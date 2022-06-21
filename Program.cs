// A simple data structure created to replicate garage parking problem on our tiny memory. this is memory efficient but not CPU efficient. Big O (in turkish we call him The OÇ) will be pissed sometimes.
// you can inspect this structure to crack your western style nonsense interviews.
// in east, we crack rocks with our heads.
// Use it, you'll understand
// you can park bicycle by entering b{number} eg: b1 or b2 or b99
// similarly you can park cars with c{number} or trucks with t{number}
// if you want to pickup specific vehicle by its key, simply input -{key}. eg if you want to pickup vehicle t4 from garage and free allocated space, type "-t4"
// input sth else to quit.
// 
using DS;

Console.WriteLine("Hello, World!");
var garage = new Garage();

while (true)
{
    garage.PrintGarage();
    var input = Console.ReadLine();
    if (string.IsNullOrEmpty(input))
        continue;
    if (input.StartsWith("b"))
        garage.Park(new Bicycle(input));

    else if (input.StartsWith("c"))
        garage.Park(new Car(input));

    else if (input.StartsWith("t"))
        garage.Park(new Truck(4, input));
    
    else if (input.StartsWith("-"))
        garage.Pickup(input[1..]);
    
    else break;
}