//Include Gulp
var gulp = require('gulp');

//Include Plugins
var less = require('gulp-less');
var concat = require('gulp-concat');
var uglify = require('gulp-uglify');
var minifycss = require('gulp-minify-css');
var rename = require('gulp-rename');
var path = require('path');
var gutil = require('gulp-util');  
var watch = require('gulp-watch');

//Css Task
gulp.task('css', function () {  
  return gulp.src('Assets/src/less/styles.less')
    .pipe(less())
    .pipe(minifycss())
    .pipe(rename('styles.min.css'))
    .pipe(gulp.dest('Assets/css'))
    .on('error', gutil.log);
});

//JS Task
gulp.task('js', function() {  
  return gulp.src('Assets/src/js/*.js')
    .pipe(concat('app.js'))
    .pipe(uglify())
    .pipe(rename('app.min.js'))
    .pipe(gulp.dest('Assets/js'))
    .on('error', gutil.log)
});

// Watch task
gulp.task('watch', ['css', 'js'], function() {

        // Watch .less files
        gulp.watch('Assets/src/less/**/*.less', ['css']);      

        // Watch .js files
        gulp.watch('Assets/src/js/**/*.js', ['js']);

});
