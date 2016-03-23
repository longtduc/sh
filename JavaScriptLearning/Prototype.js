function Car() { }

// Create properties on the prototype
Car.prototype.wheels = 4;
Car.prototype.steeringWheel = 1;
Car.prototype.color = 'metallic';

// Create objects
var car1 = new Car();
var car2 = new Car();

// Car is now the constructor function for 'car1' and 'car2'
// We didn't define any properties on car1 and car2
console.log(car1.stereo); // Prints undefined
console.log(car2.color); // Prints 'undefined'

// In spite of that, it seems that both car1 and car2 can 'see' the properties defined on its constructor's prototype (See point 4 about the prototype chain)
console.log(car1.wheels); // Prints 4
console.log(car2.wheels); // Prints 4

// Remember, they can only 'see' the parent property. If they try to overwrite it, a property of the same name is created on the object itself which shadows the property on the prototype
car1.wheels = 3; // Thats a strange car indeed!
console.log(car1.wheels); // Prints 3

// To prove that the prototype still has the property intact
console.log(car2.wheels); // Still prints 4
console.log(Car.prototype.wheels); // Checking directly at the Constructor. Prints 4

//To change the value of the prototype property, you gotta change it directly at the constructor.
Car.prototype.wheels = 5;

console.log(car2.wheels); // Prints 5
console.log(car1.wheels); // Prints 3 since it has its own 'wheels' property