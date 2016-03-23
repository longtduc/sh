function a() {
    
    function b() {
        console.log(myVar);
    }
    var myVar = 2;
    b();
}

var myVar = 1;
a();

//Output: 2 because outer lexical enviroment is execution context of function a
