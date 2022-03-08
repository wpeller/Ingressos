var gulp = require('gulp');
var gutil = require('gulp-util');
var less = require('gulp-less');
var path = require('path');
var plumber = require('gulp-plumber');
var watch = require('gulp-watch');
var merge = require('merge-stream');
var rename = require('gulp-rename');
var cleanCSS = require('gulp-clean-css');

gulp.task('gulp-less', function (cb) {
    var lessCfg = require("./compilerconfig.json");

    var tasks = lessCfg.map(function (src) {
        
        return gulp.src(src.inputFile, { base: "./" })
            .pipe(watch(src.inputFile))
            .pipe(plumber())
            .pipe(less({
                    paths: [path.join(__dirname, 'less', 'includes')],
                    sourceMap: src.sourceMap ? { sourceMapFileInline: true } : false
                })
                .on('error',
                    function(e) {
                        console.log("error: " + e);
                    }))
            .pipe(rename(src.outputFile))
            .pipe(gulp.dest("."))
            .pipe(cleanCSS({ compatibility: 'ie9' }))
            .pipe(rename({ suffix: '.min' }))
            .pipe(gulp.dest("."));
    });
    
    merge(tasks);
    cb();
});