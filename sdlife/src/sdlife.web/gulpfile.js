/// <binding Clean='clean' />
"use strict";

var gulp = require("gulp"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify"), 
    fs = require("fs");    

var paths = {
    webroot: "./wwwroot/"
};

paths.js = paths.webroot + "app/**/*.js";
paths.minJs = paths.webroot + "app/**/*.min.js";
paths.concatJsDest = paths.webroot + "app/app.min.js";

paths.css = paths.webroot + "css/**/*.css";
paths.minCss = paths.webroot + "css/**/*.min.css";
paths.concatCssDest = paths.webroot + "css/site.min.css";

paths.libJs = [
    "jquery/dist/jquery.min.js",
    "angular/angular.min.js",
    "angular-component-router/angular_1_router.js",
    "jquery.ui.touch/jquery.ui.touch.js",
    "angular-aria/angular-aria.min.js",
    "angular-messages/angular-messages.min.js",
    "angular-animate/angular-animate.min.js",
    "angular-material/angular-material.min.js",
    "moment/min/moment.min.js",
    "moment/locale/zh-cn.js",
    "fullcalendar/dist/fullcalendar.min.js",
    "fullcalendar/dist/lang/zh-cn.js",
    "angular-ui-calendar/src/calendar.js"
].map(function (x) { return paths.webroot + "lib/" + x; });
paths.concatLibJsDest = paths.webroot + "lib/lib.min.js";

paths.libCss = [
    "angular/angular-csp.css",
    "angular-material/angular-material.min.css",
    "fullcalendar/dist/fullcalendar.min.css"
].map(function (x) { return paths.webroot + "lib/" + x; });
paths.concatLibCssDest = paths.webroot + "lib/lib.min.css";

gulp.task("clean:js", function (cb) {
    rimraf(paths.concatJsDest, cb);
});

gulp.task("clean:css", function (cb) {
    rimraf(paths.concatCssDest, cb);
});

gulp.task("clean", ["clean:js", "clean:css"]);

gulp.task("min:js", function () {
    var cshtml = fs.readFileSync("Views/Home/Index.cshtml", "utf8");
    var regex = /<script src=\"~\/app\/(.+).js"><\/script>/g;
    var scripts = cshtml.match(regex).map(function (tag) {
        var jsFile = tag.replace(regex, "$1");
        return "./wwwroot/app/" + jsFile + ".js";
    });
    return gulp.src(scripts, { base: "." })
        .pipe(concat(paths.concatJsDest))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("min:libJs", function () {
    return gulp.src(paths.libJs, { base: "." })
        .pipe(concat(paths.concatLibJsDest))
        //.pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("min:css", function () {
    return gulp.src([paths.css, "!" + paths.minCss])
        .pipe(concat(paths.concatCssDest))
        .pipe(cssmin())
        .pipe(gulp.dest("."));
});

gulp.task("min:libCss", function () {
    return gulp.src(paths.libCss)
        .pipe(concat(paths.concatLibCssDest))
        .pipe(cssmin())
        .pipe(gulp.dest("."));
});

gulp.task("min", ["min:js", "min:libJs", "min:css", "min:libCss"]);
