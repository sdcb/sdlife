/// <reference path="../../typings/tsd.d.ts" />

namespace sdlife.accounting {
    let module = angular.module(consts.moduleName);

    class AccountingPage {
        eventSources = [<IAccountingEventObject[]>[]];
        calendarConfig = <IAccountingCalendarConfig>{
            height: "auto",
            editable: true,
            lang: "zh-cn", 
            header: {
                left: "month",
                center: "title",
                right: "today prev,next"
            },
            dayClick: (day, ev) => this.dayClick(day, ev),
            eventDrop: (event, duration, rollback) => this.eventDrop(event, duration, rollback),
            eventResize: (_1, _2, revert) => revert(), 
            viewRender: (view, el) => this.loadData(view.calendar.getDate()), 
            eventRender: (event, element) => this.eventRender(event, element), 
            eventClick: (event, jsEvent, view) => this.eventClick(event, jsEvent, view)
        }

        loading: ng.IPromise<any>;

        static $inject = ["$compile", "$scope", "accounting.api", "$mdDialog", "$mdMedia"];
        constructor(
            public compile: ng.ICompileService, 
            public scope: ng.IScope, 
            public api: AccountingApi,
            public dialog: ng.material.IDialogService, 
            public media: ng.material.IMedia
        ) {
        }

        currentMonth: moment.Moment;
        loadData(date: moment.Moment = null) {
            this.currentMonth = date || this.currentMonth;
            return this.loading = this.api.loadInMonth(this.currentMonth).then(dto => {
                let events = addColorToEventObjects(dto.map(x => mapEntityToCalendar(x)));
                this.eventSources[0] = events;
            });
        }

        onCreated() {
            return this.loadData();
        }

        childChangedRequestReloadData() {
            return this.loadData();
        }
        
        dayClick(date: moment.Moment, ev: MouseEvent) {
            showAccountingCreateDialog(date.format(), this.dialog, this.media, ev).then((...args) => {
                return this.loadData();
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
                    }).then(() => this.loadData());
                    ev.stopPropagation();
                }));
            }

            element.find(".fc-time").remove();            
            if (this.isSmallDevice()) {
                element.find(".fc-title").html("")
                    .append($("<span></span>").text(event.entity.title))
                    .append("<br/>")
                    .append($("<span></span>").text(`¥${event.entity.amount.toFixed(1)}`));
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

        eventDragStop(event: IAccountingEventObject, ev: MouseEvent, ui: any, view: FullCalendar.ViewObject) {
        }
    }

    module.component("accountingPage", {
        controller: AccountingPage,
        controllerAs: "vm",
        templateUrl: "/app/accounting/accounting-page.html",
    });
}