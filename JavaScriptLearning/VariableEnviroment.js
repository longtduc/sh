function b() {
    var myVar
    console.log(myVar);
}
function a() {
    var myVar = 2;
    console.log(myVar);
    b();
}
var myVar = 1;
console.log(myVar);
a();

//Output
//1
//2
//undefined