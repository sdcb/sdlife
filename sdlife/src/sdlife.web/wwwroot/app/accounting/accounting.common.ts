/// <reference path="../../typings/tsd.d.ts" />

namespace sdlife.accounting {
    export var consts = {
        moduleName: "accounting", 
        version: new Date().getTime(), 
    };

    let module = angular.module(consts.moduleName, ["ngMaterial", "ui.calendar", "ngMessages"]);
    moment.locale("zh-cn");

    module.filter("accountingDate", () => {
        return (dateOrMoment: moment.Moment|Date) => {
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

    export function mapEntityToCalendar(entity: IAccountingEntity): IAccountingEventObject {
        return {
            title: entity.title, 
            start: entity.time, 
            allDay: false, 
            entity: entity
        };
    }

    function generateColor(current: number, total: number) {
        let i = current / 6;
        let j = current % 6;

        let hue = i / (total / 6) / 6 + j / 6;
        let saturation = 1;
        let lightness = 0.3 + i / (total / 6) / 3;
        return `hsl(${hue * 360}, ${saturation * 100}%, ${lightness * 100}%)`;
    }

    export function addColorToEventObjects(data: IAccountingEventObject[]) {
        let totalColorIds = 0;
        let obj = <{
            [item: string]: number
        }>{};

        for (let item of data) {
            let colorId = obj[item.title];
            if (!colorId) {
                obj[item.title] = totalColorIds;
                totalColorIds += 1;
            }
        }

        for (let item of data) {
            let colorId = obj[item.title];
            item.color = generateColor(colorId, totalColorIds);
        }

        return data;
    }
}