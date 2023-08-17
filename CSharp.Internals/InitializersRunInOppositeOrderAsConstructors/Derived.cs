namespace InitializersRunInOppositeOrderAsConstructors;

public class Derived : Base
{
    readonly Foo derivedFoo = new Foo("Derived initializer");
    
    public Derived()
    {
        Console.WriteLine("Derived constructor");
    }
}
