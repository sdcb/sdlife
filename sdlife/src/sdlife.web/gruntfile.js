"use strict";

module.exports = function (grunt) {
    grunt.initConfig({
        pkg: grunt.file.readJSON("package.json"), 
        ngtemplates: {
            myapp: {
                options: {
                    module: 'sdlife',
                },
                src: 'wwwroot/app/*/*.html',
                dest: 'wwwroot/app/app.tpl.min.js'
            }
        }
    });

    grunt.loadNpmTasks("grunt-angular-templates");
    grunt.registerTask("default", ["ngtemplates"])
}