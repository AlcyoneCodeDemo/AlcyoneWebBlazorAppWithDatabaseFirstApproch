const gulp = require('gulp');
const concat = require('gulp-concat');
const uglify = require('gulp-uglify');
const cssnano = require('gulp-cssnano');
const sourcemaps = require('gulp-sourcemaps');

const jsFiles = 'wwwroot/js/**/*.js'; // Modify this path to match your JS files
const cssFiles = 'wwwroot/css/**/*.css'; // Modify this path to match your CSS files

gulp.task('scripts', function () {
    return gulp.src(jsFiles)
        .pipe(sourcemaps.init())
        .pipe(concat('all.min.js'))
        .pipe(uglify())
        .pipe(sourcemaps.write('.'))
        .pipe(gulp.dest('wwwroot/dist/js'));
});

gulp.task('styles', function () {
    return gulp.src(cssFiles)
        .pipe(sourcemaps.init())
        .pipe(concat('all.min.css'))
        .pipe(cssnano())
        .pipe(sourcemaps.write('.'))
        .pipe(gulp.dest('wwwroot/dist/css'));
});

gulp.task('default', gulp.series('scripts', 'styles'));

