namespace sdlife.accounting {
    class CreateIcon {
        open(ev: MouseEvent) {
            showAccountingCreateDialog(
                moment().format(),
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
            onCreated: "&"
        }, 
        templateUrl: `/app/accounting/accounting.createicon.html?${consts.version}`, 
    });
}