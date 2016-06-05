/// <reference path="../../typings/tsd.d.ts" />

namespace sdlife.accounting {
    let module = angular.module(consts.moduleName);
    moment.locale("zh-cn");

    module.filter("accountingDate", () => {
        return (dateOrMoment: moment.Moment | Date | string) => {
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
        const saturation = 1;

        function unique<T>(arr: Array<T>) {
            return [...new Set(arr)];
        }

        let incomes = data.filter(x => x.entity.isIncome);
        let incomesSorted = unique(incomes.map(x => x.entity.amount)).sort((a, b) => a - b);
        incomes.forEach(v => {
            let index = incomesSorted.indexOf(v.entity.amount);
            let percent = index / incomesSorted.length;
            const hue = 0.7;
            let lightness = 0.8 - percent * 0.5;
            v.color = `hsl(${hue * 360}, ${saturation * 100}%, ${lightness * 100}%)`;
        });
        
        let spends = data.filter(x => !x.entity.isIncome);
        let spendsSorted = unique(spends.map(x => x.entity.amount)).sort((a, b) => a - b);
        spends.forEach(v => {
            let index = spendsSorted.indexOf(v.entity.amount);
            let percent = index / spendsSorted.length;
            const hue = 0;
            let lightness = 0.8 - percent * 0.6;
            v.color = `hsl(${hue * 360}, ${saturation * 100}%, ${lightness * 100}%)`;
        });

        return data;
    }
}