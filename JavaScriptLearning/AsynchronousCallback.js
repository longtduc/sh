function waitThreeSecond() {
    var ms = 3000 + new Date().getTime();
    while (new Date() < ms)
    {
        console.log('c');
    } 
}

function clickHandler() {
    console.log('click event');
}
document.addEventListener('click', clickHandler, false);

waitThreeSecond();
console.log('finished execution');
//output
//finished function
//finished function
//...
//finished execution
//click event
