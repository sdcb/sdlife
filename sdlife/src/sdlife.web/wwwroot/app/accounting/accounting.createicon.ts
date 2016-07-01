namespace sdlife.accounting {
    class CreateIcon {
        userId: number;

        open(ev: MouseEvent) {
            showAccountingCreateDialog(
                moment().format(),
                this.userId, 
                this.dialog,
                this.media,
                ev).then(() => {
                    this.onCreated();
                });
        }

        onCreated: () => void;

        static $inject = ["$mdMedia", "$mdDialog"];
        constructor(
            public media: ng.material.IMedia,
            public dialog: ng.material.IDialogService) {
        }
    }
    
    let module = angular.module(consts.moduleName);
    module.component("createIcon", {
        controller: CreateIcon, 
        controllerAs: "vm", 
        bindings: {
            onCreated: "&", 
            userId: "<", 
        }, 
        templateUrl: "/app/accounting/accounting.createicon.html", 
    });
}