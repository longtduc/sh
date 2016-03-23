function calculator(first, second, callback) {
    callback(first, second);
}

function add(first, second) {
    console.log('Added Result '+(first + second).toString());
}

function substract(first, second) {
    console.log("Substracted Result " + (first - second).toString());
}

calculator(10, 20, add);
calculator(20, 10, substract);