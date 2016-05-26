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
        let incomes = data.filter(x => x.entity.isIncome);
        let spends = data.filter(x => !x.entity.isIncome);
        let incomeMax = Math.max(...incomes.map(x => Math.abs(x.entity.amount)));
        let incomeMin = Math.min(...incomes.map(x => Math.abs(x.entity.amount)));
        let spendMax = Math.max(...spends.map(x => Math.abs(x.entity.amount)));
        let spendMin = Math.min(...spends.map(x => Math.abs(x.entity.amount)));
        return data.map(x => {
            let hue: number;
            let lightness: number;

            if (x.entity.isIncome) {
                let percent = (Math.abs(x.entity.amount) - incomeMin) / incomeMax;
                hue = 0.7;
                lightness = 0.8 - percent * 0.5;
            } else {
                let percent = (Math.abs(x.entity.amount) - spendMin) / spendMax;
                hue = 0;
                lightness = 0.8 - percent * 0.6;
            }
            let saturation = 1;
            
            x.color = `hsl(${hue * 360}, ${saturation * 100}%, ${lightness * 100}%)`;
            return x;
        });
    }
}