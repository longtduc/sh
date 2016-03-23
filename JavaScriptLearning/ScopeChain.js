function b() {
    console.log(myVar);
}
function a() {
    var myVar = 2;
    b();
}

var myVar = 1;
a();

//Output: 1, not 2 because outer lexical eviroment is global execution context

