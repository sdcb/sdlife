/// <binding Clean='clean' />
"use strict";

var gulp = require("gulp"),
    gulpif = require("gulp-if"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify"),
    htmlmin = require("gulp-htmlmin"),
    templateCache = require("gulp-angular-templatecache"),
    fs = require("fs");

var webroot = "./wwwroot/";

gulp.task("min:templateCache", function () {
    return gulp.src("wwwroot/app/**/*.html")
        .pipe(htmlmin({
            collapseWhitespace: true,
            collapseBooleanAttributes: true,
            removeComments: true,
        }))
        .pipe(templateCache({
            module: "sdlife",
            root: "/app/"
        }))
        .pipe(concat("wwwroot/min/app.tpl.min.js"))
        .pipe(gulp.dest("."));
});

gulp.task("min:appJs", function () {
    var cshtml = fs.readFileSync("Views/Home/Index.cshtml", "utf8");
    var regex = /<script src=\"~\/app\/(.+).js"><\/script>/g;
    var scripts = cshtml.match(regex).map(function (tag) {
        var jsFile = tag.replace(regex, "$1");
        return "./wwwroot/app/" + jsFile + ".js";
    });
    return gulp.src(scripts, { base: "." })
        .pipe(concat("wwwroot/min/app.min.js"))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("min:libJs", function () {
    var libJs = [
        "wwwroot/lib/jquery/dist/jquery.min.js",
        "wwwroot/lib/angular/angular.min.js",
        "wwwroot/lib/angular-component-router/angular_1_router.js",
        "wwwroot/lib/jquery.ui.touch/jquery.ui.touch.js",
        "wwwroot/lib/angular-aria/angular-aria.min.js",
        "wwwroot/lib/angular-messages/angular-messages.min.js",
        "wwwroot/lib/angular-animate/angular-animate.min.js",
        "wwwroot/lib/angular-material/angular-material.min.js",
        "wwwroot/lib/angular-material-data-table/dist/md-data-table.min.js", 
        "wwwroot/lib/moment/min/moment.min.js",
        "wwwroot/lib/fullcalendar/dist/fullcalendar.min.js",
        "wwwroot/lib/fullcalendar/dist/lang/zh-cn.js",
        "wwwroot/lib/angular-ui-calendar/src/calendar.js",
        "wwwroot/lib/lodash/dist/lodash.min.js"
    ];
    return gulp.src(libJs, { base: "." })
        .pipe(gulpif(function (file) {
            return !/\.min\.js/.test(file.path);
        }, uglify()))
        .pipe(concat("wwwroot/min/lib.min.js"))
        .pipe(gulp.dest("."));
});

gulp.task("min", ["min:templateCache", "min:appJs", "min:libJs"]);
