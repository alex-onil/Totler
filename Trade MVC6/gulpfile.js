/// <binding BeforeBuild='min:angularJs' Clean='clean' />
"use strict";

var gulp = require("gulp"),
    rimraf = require("rimraf"),
    concat = require("gulp-concat"),
    cssmin = require("gulp-cssmin"),
    uglify = require("gulp-uglify"),
    ngmin = require("gulp-ngmin"),
    ngAnnotate = require("gulp-ng-annotate"),
    angularFilesort = require("gulp-angular-filesort"),
    inject = require('gulp-inject');


var paths = {
    webroot: "./wwwroot/"
};

paths.js = paths.webroot + "js/**/*.js";
paths.minJs = paths.webroot + "js/**/*.min.js";
paths.css = paths.webroot + "css/**/*.css";
paths.minCss = paths.webroot + "css/**/*.min.css";
paths.concatJsDest = paths.webroot + "js/site.min.js";
paths.concatCssDest = paths.webroot + "css/site.min.css";

// Angular App Paths

paths.angularApp = "./App/**/*.js";
paths.angularJsDest = paths.webroot + "js/angular-app.min.js";


gulp.task("clean:js", function (cb) {
    rimraf(paths.concatJsDest, cb);
});

gulp.task("clean:css", function (cb) {
    rimraf(paths.concatCssDest, cb);
});

gulp.task("clean", ["clean:js", "clean:css"]);

gulp.task("min:js", function () {
    return gulp.src([paths.js, "!" + paths.minJs], { base: "." })
        .pipe(concat(paths.concatJsDest))
        .pipe(uglify())
        .pipe(gulp.dest("."));
});

gulp.task("min:angularJs", function() {
    return gulp.src(paths.angularApp)
        .pipe(inject(gulp.src(paths.angularApp).pipe(angularFilesort())))
        .pipe(concat(paths.angularJsDest))
        .pipe(ngAnnotate())
        //.pipe(ngmin()) // { dynamic: true }
        //.pipe(uglify({ mangle: false }))
        .pipe(gulp.dest("."));

});

gulp.task("min:css", function () {
    return gulp.src([paths.css, "!" + paths.minCss])
        .pipe(concat(paths.concatCssDest))
        .pipe(cssmin())
        .pipe(gulp.dest("."));
});

gulp.task("min", ["min:js", "min:css"]);
