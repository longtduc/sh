function myClosure() {
    var date = new Date();
    return function () {
        return date.getMilliseconds();
    }
}
var a = myClosure;
alert(a());
window.setTimeout(function () {
    alert(a());
}, 500);