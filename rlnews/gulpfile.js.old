//Include Gulp
var gulp = require('gulp');

//Include Plugins
var less = require('gulp-less');

var SRC = "Assets/less/*.less";

gulp.task('less', function() {
	gulp.src('Assets/less/styles.less')
	.pipe(less())
	.pipe(gulp.dest('Assets/css'))
});

gulp.task('watch', function() {
	gulp.watch(SRC, ['less']);
});