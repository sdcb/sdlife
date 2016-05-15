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
        dayClick: (date: Date, ev: MouseEvent) => void, 
        eventDrop: (event: IAccountingEventObject, duration: moment.Duration, rollback: () => void) => void, 
        eventResize: (event: IAccountingEventObject) => void, 
    }
}