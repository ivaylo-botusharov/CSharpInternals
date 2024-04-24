Vehicle carVehicle = new Car();
carVehicle.GetDetails();
carVehicle.SetDetails();

Console.WriteLine("\nPress Enter Key to Exit..");
Console.ReadLine();

public class Vehicle
{
    public virtual void GetDetails()
    {
        Console.WriteLine("Get Base Vehicle Details");
    }
    public virtual void SetDetails()
    {
        Console.WriteLine("Set Base Vehicle Details");
    }
}

public class Car : Vehicle
{
    public override void GetDetails()
    {
        Console.WriteLine("Get Child Car Details");
    }

    public new void SetDetails()
    {
        Console.WriteLine("Set Child Car Details");
    }
}