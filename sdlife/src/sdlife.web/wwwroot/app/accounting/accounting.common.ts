namespace sdlife.accounting {
    let app = angular.module(consts.moduleName);
    moment.locale("zh-cn");

    app.filter("accountingDate", () => {
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

    app.filter("accountingTime", () => {
        return (dateOrMoment: moment.Moment | Date | string) => {
            let time = moment(dateOrMoment);
            return time.format("LL");
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

    export function NN<T>(t: T | undefined) {
        if (t === null || t === undefined) {
            throw new Error("value should never be null");
        } else {
            return t;
        }
    }

    export function addColorToEventObjects(data: IAccountingEventObject[]) {
        const saturation = 1;

        let incomes = data.filter(x => x.entity.isIncome);
        let incomesSorted = _.chain(incomes.map(x => x.entity.amount)).uniq().sortBy().value();
        incomes.forEach(v => {
            let index = incomesSorted.indexOf(v.entity.amount);
            let percent = index / incomesSorted.length;
            const hue = 0.7;
            let lightness = 0.8 - percent * 0.5;
            v.color = `hsl(${hue * 360}, ${saturation * 100}%, ${lightness * 100}%)`;
        });

        let spends = data.filter(x => !x.entity.isIncome);
        let spendsSorted = _.chain(spends.map(x => x.entity.amount)).uniq().sortBy().value();
        spends.forEach(v => {
            let index = spendsSorted.indexOf(v.entity.amount);
            let percent = index / spendsSorted.length;
            const hue = 0;
            let lightness = 0.9 - percent * 0.7;
            v.color = `hsl(${hue * 360}, ${saturation * 100}%, ${lightness * 100}%)`;
        });

        return data;
    }

    export class AccountingBasePage {
        readonly $router: ng.Router;
        userId: number | null;
        canCreate = false;

        $routerOnActivate(next: ng.ComponentInstruction) {
            let userId = next.params["userId"] ||
                this.$router.parent.parent._currentInstruction.component.params["userId"];
            if (userId === "me" || !userId) {
                this.userId = null;
                this.canCreate = true;
            } else {
                this.userId = parseInt(userId);
                if (this.userId) {
                    this.api.canIModify(this.userId)
                        .then(v => this.canCreate = v);
                }
            }
        }

        static $inject = ["accounting.api"];
        constructor(public api: AccountingApi) {
        }
    }
}