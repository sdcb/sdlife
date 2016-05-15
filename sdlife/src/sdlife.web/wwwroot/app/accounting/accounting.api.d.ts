declare namespace sdlife.accounting {
    export interface IAccountingEntity extends IAccountingDto {
        id: number;
    }

    export interface IAccountingDto {
        title: string;
        amount: number;
        time: string;
        comment?: string;
    }

    export interface IAccountingEventObject {
        title: string, 
        start: string|moment.Moment, 
        end?: string|moment.Moment, 
        allDay?: boolean, 
        color?: string, 

        entity: IAccountingEntity
    }

    export interface IAccountingCalendarConfig {
        height: number, 
        editable: boolean, 
        header: {
            left: string, 
            center: string, 
            right: string
        }, 
        lang?: "zh-cn", 
        dayClick: (date: moment.Moment, ev: MouseEvent) => void, 
        eventDrop: (event: IAccountingEventObject, duration: moment.Duration, rollback: () => void) => void, 
        eventResize: (event: IAccountingEventObject, delta: moment.Duration, revertFunc: () => void, jsEvent: Event, ui: any, view: FullCalendar.ViewObject) => void, 
        viewRender: (view: FullCalendar.ViewObject, element: JQuery) => void;
        eventRender: (event: IAccountingEventObject, element: JQuery, view: FullCalendar.ViewObject) => void;
        eventDragStop?: (event: IAccountingEventObject, jsEvent: MouseEvent, ui: any, view: FullCalendar.ViewObject) => void;
        eventClick?: (event: IAccountingEventObject, jsEvent: MouseEvent, view: FullCalendar.ViewObject) => any; // return type boolean or void
    }
}

declare namespace FullCalendar {
    interface Calendar {
        getDate(): moment.Moment;
    }

    interface ViewObject {
        calendar: Calendar;
    }
}