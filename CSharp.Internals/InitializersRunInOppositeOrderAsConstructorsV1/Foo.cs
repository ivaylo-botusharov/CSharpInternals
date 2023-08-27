namespace InitializersRunInOppositeOrderAsConstructorsV1;

public class Foo
{
    public Foo(string invokedBy)
    {
        Console.WriteLine($"Foo constructor (invoked by): {invokedBy}");
    }
    
    public void Bar()
    {
        Console.WriteLine($"Foo.Bar()");
    }
}
