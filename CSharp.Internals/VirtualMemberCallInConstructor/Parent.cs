namespace VirtualMemberCallInConstructor;

public class Parent
{
    private readonly string fred = "FRED";

    public Parent()
    {
        this.DoSomething();
    }

    public string Waldo { get; } = "WALDO";

    protected virtual void DoSomething()
    {
        Console.WriteLine(this.fred.ToLower());
        Console.WriteLine("Parent.DoSomething");
    }
}
