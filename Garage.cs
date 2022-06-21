namespace DS;

public class Garage
{

    private ParkingLot? ParkingLot { get; set; } = null!;

    public void Park(Vehicle vehicle)
    {

        if (ParkingLot == null)
        {
            ParkingLot = new ParkingLot(vehicle, 0);
            return;
        }

        if (ParkingLot.StartsAt > vehicle.Length)
        {
            var parkingLot = new ParkingLot(vehicle, 0);
            parkingLot.Next = ParkingLot;
            ParkingLot = parkingLot;
            return;
        }

        var currentLot = ParkingLot;

        while (currentLot.Next != null)
        {
            if (currentLot.SpaceAvailable >= vehicle.Length)
            {
                var inserting = new ParkingLot(vehicle, currentLot.EndsAt);
                inserting.Next = currentLot.Next;
                currentLot.Next = inserting;
                return;
            }
            currentLot = currentLot.Next;
        }
        var startsAt = currentLot.StartsAt + (currentLot.Vehicle.Length);

        currentLot.Next = new ParkingLot(vehicle, startsAt);
    }

    public Vehicle? Pickup(string key)
    {
        if (ParkingLot == null)
        {
            return null;
        }

        var currentLot = ParkingLot;

        if (currentLot.HasVehicle(key))
        {
            var vehicle = ParkingLot.Vehicle;
            ParkingLot = currentLot.Next;
            return vehicle;
        }

        while (currentLot.Next is not null)
        {
            if (currentLot.Next.HasVehicle(key))
            {
                return currentLot.ReleaseNext();
            }
            currentLot = currentLot.Next;
        }

        return null;
    }


    public void PrintGarage()
    {
        var currentLot = ParkingLot;
        for (int i = 0; i < (currentLot?.StartsAt ?? 0); i++)
            Console.Write($"{"___":10}|");

        while (currentLot != null)
        {
            currentLot.Print();
            currentLot = currentLot.Next;
        }

        Console.WriteLine("\n\n");
    }
}

internal class ParkingLot
{
    public ParkingLot(Vehicle vehicle, int startsAt)
    {
        Vehicle = vehicle;
        StartsAt = startsAt;
    }

    public Vehicle Vehicle { get; set; }
    public int StartsAt { get; set; }
    public int EndsAt => StartsAt + (Vehicle?.Length ?? 1);
    public int SpaceAvailable => Next == null ? 0 : Next.StartsAt - EndsAt;
    public ParkingLot? Next { get; set; }

    internal bool HasVehicle(string key)
    {
        return key == Vehicle.Key;
    }

    internal void Print()
    {
        for (int i = 0; i < Vehicle?.Length; i++)
            Console.Write($"{Vehicle.Key.PadLeft(3, '_')}|");

        if (SpaceAvailable <= 0)
            return;

        for (int i = 0; i < SpaceAvailable; i++)
            Console.Write($"{"___":10}|");


    }

    internal Vehicle? ReleaseNext()
    {
        var nextVehicle = Next?.Vehicle;
        var newNext = Next?.Next;
        this.Next = newNext;
        return nextVehicle;
    }
}

public abstract class Vehicle
{
    public int Length { get; set; }
    public char Identifier { get; set; }
    public string Key { get; set; }
    protected Vehicle(int length, char identifier, string key)
    {
        Identifier = identifier;
        Length = length;
        Key = key;
    }
}

public class Bicycle : Vehicle
{
    public Bicycle(string key) : base(1, 'b', key)
    {
    }
}

public class Car : Vehicle
{
    public Car(string key) : base(2, 'c', key)
    {
    }
}

public class Truck : Vehicle
{
    public Truck(int length, string key) : base(length, 't', key)
    {
    }
}