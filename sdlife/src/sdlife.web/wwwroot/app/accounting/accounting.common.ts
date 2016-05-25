/// <reference path="../../typings/tsd.d.ts" />

namespace sdlife.accounting {
    let module = angular.module(consts.moduleName);
    moment.locale("zh-cn");

    module.filter("accountingDate", () => {
        return (dateOrMoment: moment.Moment|Date|string) => {
            let time = moment(dateOrMoment);
            if (time.isSame(moment(), "day")) {
                return "今天";
            } else if (time.clone().add(1, "day").isSame(moment(), "day")) {
                return "昨天";
            } else if (time.clone().add(2, "day").isSame(moment(), "day")) {
                return "前天";
            } else {
                return time.clone().format("LL");
            }
        };
    });

    export function isSmallDevice(media: ng.material.IMedia) {
        return media("xs");
    }

    export function ensure(
        dialog: ng.material.IDialogService,
        ev: Event,
        title: string,
        textContent: string = null) {
        return dialog.show(dialog.confirm()
            .title(title)
            .textContent(textContent)
            .ok("确定")
            .cancel("取消")
            .clickOutsideToClose(true)
            .targetEvent(<MouseEvent>ev));
    }

    export function mapEntityToCalendar(entity: IAccountingEntity): IAccountingEventObject {
        return {
            title: entity.title, 
            start: entity.time, 
            allDay: false, 
            entity: entity
        };
    }

    export function addColorToEventObjects(data: IAccountingEventObject[]) {
        let max = Math.max(...data.map(x => Math.abs(x.entity.amount)));
        let min = Math.min(...data.map(x => Math.abs(x.entity.amount)));
        return data.map(x => {
            let percent = (Math.abs(x.entity.amount) - min) / max;
            let lightness = 0.7 - percent * 0.5;
            let saturation = 1;
            let hue = x.entity.isIncome ? 0.4 : 0;
            x.color = `hsl(${hue * 360}, ${saturation * 100}%, ${lightness * 100}%)`;
            return x;
        });
    }
}