/// <reference path="../../typings/tsd.d.ts" />
var sdlife;
(function (sdlife) {
    var accounting;
    (function (accounting) {
        accounting.consts = {
            moduleName: "accounting"
        };
        angular.module(accounting.consts.moduleName, ["ngMaterial", "ui.calendar"]);
        function mapEntityToCalendar(entity) {
            return {
                title: entity.title,
                start: entity.time,
                allDay: false,
                entity: entity
            };
        }
        accounting.mapEntityToCalendar = mapEntityToCalendar;
        function generateColor(current, total) {
            var i = current / 6;
            var j = current % 6;
            var hue = i / (total / 6) / 6 + j / 6;
            var saturation = 1;
            var lightness = 0.3 + i / (total / 6) / 3;
            return "hsl(" + hue * 360 + ", " + saturation * 100 + "%, " + lightness * 100 + "%)";
        }
        function addColorToEventObjects(data) {
            var totalColorIds = 0;
            var obj = {};
            for (var _i = 0, data_1 = data; _i < data_1.length; _i++) {
                var item = data_1[_i];
                var colorId = obj[item.title];
                if (!colorId) {
                    obj[item.title] = totalColorIds;
                    totalColorIds += 1;
                }
            }
            for (var _a = 0, data_2 = data; _a < data_2.length; _a++) {
                var item = data_2[_a];
                var colorId = obj[item.title];
                item.color = generateColor(colorId, totalColorIds);
            }
            return data;
        }
        accounting.addColorToEventObjects = addColorToEventObjects;
    })(accounting = sdlife.accounting || (sdlife.accounting = {}));
})(sdlife || (sdlife = {}));
//# sourceMappingURL=accounting.common.js.map