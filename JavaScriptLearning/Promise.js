
var p = new Promise(function (resolve, reject) {
    console.log('Promise Started (Async code)');
    var randomNum;
    setTimeout(function () {
        randomNum = Math.random() * 2000 + 1000;
        console.log('SetTimeOut Running (Async code)');
        resolve(randomNum);      
    }, 1000);   

});
p.then(function (val) {
    console.log('Promise fulfilled (Async code)');
    console.log(val);
}).catch(function (reason) {
    console.log('Handle rejected promise (' + reason + ') here.');
    console.log(reason);
});
console.log('Ending');

//-----------------------------------------
//var p = Promise.resolve().then(function () {
//    console.log('Promise resolve callback');  // Prints Promise resolve callback
//}, function () {
//    console.log('Promise reject callback');
//});

//-----------------------------------------
//var p = Promise.reject().then(function () {
//    console.log('Promise resolve callback');
//}, function () {
//    console.log('Promise reject callback'); // Prints Promise reject callback
//});

//-----------------------------------------
//var p = Promise.resolve(1).then(function (value) {
//    console.log(value); // Prints 1
//    return 2;
//}).then(function (value) {
//          console.log(value); // Prints 2
//      });

//-----------------------------------------
//var p = Promise.reject().then(function () {
//    console.log('Promise resolve callback');
//}, function () {
//    console.log('Promise reject callback'); // Prints Promise reject callback
//});

//-----------------------------------------
//var p = Promise.resolve().then(function (value) {
//    console.log(value); // Prints undefined			
//    return 1;
//})
//	.then(function (value) {
//	    console.log(value); // Prints 1			
//	});

//-----------------------------------------
//var p = Promise.resolve().then(function (value) {
//    return 1;
//})
//	.then(function (value) {
//	    console.log('One', value); // Prints One, 1			
//	})
//	.then(function (value) {
//	    console.log('Two', value); // Prints Two, undefined			
//	});

//-----------------------------------------
//var p = Promise.resolve(1).then(function (value) {
//    console.log(value); // Prints 1			
//    return Promise.resolve(2);
//}).then(function (value) {
//    console.log(value); // Prints 2			
//});

//-----------------------------------------
//var p = Promise.reject().
//    then(function () {
//            console.log('resolve1');	// This does not get invoked		
//        }, function () {
//    console.error('reject1');  // Prints reject1			
//    }).catch(function () {
//      	    console.error('catch');
//      	});

//var p = Promise.reject()
//    .then(function () {
//        console.log('resolve1');  // This gets skipped due to rejection		
//    })
//	.then(function () {
//	    console.log('resolve2');  // This also gets skipped
//	    }, function () {
//	    console.error('reject2'); // Prints reject2			
//	})
//	.catch(function () {
//	    // This gets skipped because a previous error handler caught the failed promise
//	    console.error('catch');
//	});