/// <reference path="../../typings/tsd.d.ts" />

namespace sdlife.accounting {
    let module = angular.module(consts.moduleName);

    export class AccountingPage {
        userId: number;
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
            this.userId = next.params.userId;
        }

        calendarView: FullCalendar.ViewObject;
        loading: ng.IPromise<any>;
        loadDataInViewRange() {
            let from = this.calendarView.start.format();
            let to = this.calendarView.end.format();
            return this.loading = this.api.loadInRange(from, to, this.userId).then(dto => {
                let events = addColorToEventObjects(dto.map(x => mapEntityToCalendar(x)));
                return this.timeout(() => this.eventSources[0] = events, 0);
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
            showAccountingCreateDialog(date.format(), this.dialog, this.media, ev).then((...args) => {
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

    module.component("accountingPage", {
        controller: AccountingPage,
        controllerAs: "vm",
        templateUrl: "/app/accounting/accounting-page.html",
    });
}