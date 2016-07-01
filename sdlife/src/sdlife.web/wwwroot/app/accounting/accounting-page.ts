/// <reference path="../../typings/tsd.d.ts" />

namespace sdlife.accounting {
    let module = angular.module(consts.moduleName);

    export class AccountingPage {
        userId: number;
        canCreate = true;
        eventSources = [<IAccountingEventObject[]>[]];
        calendarConfig = <IAccountingCalendarConfig>{
            height: "auto",
            editable: true,
            lang: "zh-cn",
            header: {
                left: "month, basicWeek, basicDay",
                center: "title",
                right: "today prev,next"
            },
            dayClick: (day, ev) => this.dayClick(day, ev),
            eventDrop: (event, duration, rollback) => this.eventDrop(event, duration, rollback),
            eventResize: (_1, _2, revert) => revert(),
            viewRender: (view, el) => this.viewRender(view, el),
            eventRender: (event, element) => this.eventRender(event, element),
            eventClick: (event, jsEvent, view) => this.eventClick(event, jsEvent, view)
        }

        $routerOnActivate(next) {
            this.userId = parseInt(next.params.userId);
            if (this.userId) {
                this.api.canIModify(this.userId)
                    .then(v => this.canCreate = v);
            }
        }

        calendarView: FullCalendar.ViewObject;
        loading: ng.IPromise<any>;
        loadDataInViewRange() {
            let from = this.calendarView.start.format();
            let to = this.calendarView.end.format();
            return this.loading = this.api.loadInRange(from, to, this.userId).then(dto => {
                let events = addColorToEventObjects(dto.map(x => mapEntityToCalendar(x)));
                return this.timeout(0).then(() => {
                    this.eventSources[0] = events;
                    this.caculateTotalAmount();
                });
            });
        }

        onCreated() {
            return this.loadDataInViewRange();
        }

        childChangedRequestReloadData() {
            return this.loadDataInViewRange();
        }

        viewRender(view: FullCalendar.ViewObject, el: JQuery) {
            this.calendarView = view;
            return this.loadDataInViewRange();
        }

        dayClick(date: moment.Moment, ev: MouseEvent) {
            showAccountingCreateDialog(date.format(), this.userId, this.dialog, this.media, ev).then((...args) => {
                return this.loadDataInViewRange();
            });
        }

        eventClick(event: IAccountingEventObject, jsEvent: MouseEvent, view: FullCalendar.ViewObject) {
            showAccountingEditDialog(event.entity, this.dialog, jsEvent, this.media, () => this.childChangedRequestReloadData());
        }

        eventDrop(event: IAccountingEventObject, duration: moment.Duration, rollback: () => void) {
            this.loading = this.api.updateTime(event.entity.id, event.start)
                .catch(() => rollback());
        }

        eventRender(event: IAccountingEventObject, element: JQuery) {
            $(element).addTouch();

            if (event.entity.comment) {
                element.append($(`<md-tooltip>${event.entity.comment.replace("\n", "<br/>")}</md-tooltip>`));
            }

            if (!isSmallDevice(this.media)) {
                element.append($(`<span class="top-right calendar-delete-button">✕&nbsp;</span>`).click(ev => {
                    ensure(this.dialog, ev, "确定要删除此条目吗？").then(() => {
                        return this.loading = this.api.delete(event.entity.id);
                    }).then(() => this.loadDataInViewRange());
                    ev.stopPropagation();
                }));
            }

            element.find(".fc-time").remove();
            if (this.isSmallDevice()) {
                element.find(".fc-title").html("")
                    .append($("<span></span>").text(event.entity.title))
                    .append("<br/>")
                    .append($("<span></span>").text(`${event.entity.amount.toFixed(1)}`));
            } else {
                element.find(".fc-title")
                    .html("")
                    .append($("<span></span>")
                        .text(`${event.entity.title}: ¥${event.entity.amount.toFixed(1)}`));
            }


            this.compile(element)(this.scope);
        }

        isSmallDevice() {
            return isSmallDevice(this.media);
        }

        totalInternal(isIncome: boolean) {
            function inRange(value: string, range: { start: moment.Moment, end: moment.Moment }) {
                return (
                    range.start.isSameOrBefore(value) &&
                    range.end.isAfter(value));
            }

            function totalAmountByFilter(books: IAccountingEventObject[], filter: (dto: IAccountingEventObject) => boolean) {
                return _.chain(books)
                    .filter(x => filter(x))
                    .sumBy(x => x.entity.amount);
            }

            let range = currentViewRange(this.calendarView.calendar.getDate(), this.calendarView.name);
            let filter = (dto: IAccountingEventObject) =>
                inRange(dto.entity.time, range) && dto.entity.isIncome === isIncome;
            return totalAmountByFilter(this.eventSources[0], filter).value();
        }

        totalIncome = 0;
        totalSpend = 0;
        caculateTotalAmount() {
            this.totalIncome = this.totalInternal(true);
            this.totalSpend = this.totalInternal(false);
        }

        period() {
            let range = currentViewRange(this.calendarView.calendar.getDate(), this.calendarView.name);
            return `${range.start.format("l")} - ${range.end.format("l")}`;
        }

        static $inject = ["$compile", "$scope", "accounting.api", "$mdDialog", "$mdMedia", "$timeout"];
        constructor(
            public compile: ng.ICompileService,
            public scope: ng.IScope,
            public api: AccountingApi,
            public dialog: ng.material.IDialogService,
            public media: ng.material.IMedia,
            public timeout: ng.ITimeoutService
        ) {
            scope.$watch(() => this.isSmallDevice(), () => {
                let v = this.eventSources[0];
                this.eventSources[0] = [];
                timeout(() => {
                    this.eventSources[0] = v;
                }, 0);
            });
        }
    }

    function currentViewRange(currentTime: moment.Moment, viewName: string) {
        const durations = ["month", "week", "day"];
        viewName = viewName.toLowerCase();
        for (let duration of durations) {
            if (_.includes(viewName, duration)) {
                return {
                    start: currentTime.clone().startOf(duration),
                    end: currentTime.clone().startOf(duration).add(1, duration)
                }
            }
        }
        throw new Error("UNKNOWN viewName: " + viewName);
    }

    class AccountingPageForFriend extends AccountingPage {
        canCreate = false;

        dayClick(date: moment.Moment, ev: MouseEvent) {
            if (this.canCreate) {
                super.dayClick(date, ev);
            }
        }

        eventClick(event: IAccountingEventObject, jsEvent: MouseEvent, view: FullCalendar.ViewObject) {
            if (this.canCreate) {
                super.eventClick(event, jsEvent, view);
            }
        }

        eventDrop(event: IAccountingEventObject, duration: moment.Duration, rollback: () => void) {
            if (this.canCreate) {
                super.eventDrop(event, duration, rollback);
            } else {
                rollback();
            }
        }
    }

    module.component("accountingPage", {
        controller: AccountingPage,
        controllerAs: "vm",
        templateUrl: "/app/accounting/accounting-page.html",
    });

    module.component("accountingPageForFriend", {
        controller: AccountingPageForFriend,
        controllerAs: "vm",
        templateUrl: "/app/accounting/accounting-page.html",
    });
}