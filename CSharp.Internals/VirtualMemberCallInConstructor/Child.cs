namespace VirtualMemberCallInConstructor;

public class Child : Parent
{
    private readonly string foo;

    private readonly string bar = "WORLD";

    public Child()
    {
        this.foo = "HELLO";
    }

    public string Baz { get; } = "BAZ";

    protected override void DoSomething()
    {
        Console.WriteLine(this.bar.ToLower()); // this line executes fine
        Console.WriteLine(this.foo.ToLower()); // this line throws a NullReferenceException, since foo is null
    }
}
