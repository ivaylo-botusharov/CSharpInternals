namespace InitializersRunInOppositeOrderAsConstructorsV1;

public class Derived : Base
{
    readonly Foo derivedFoo = new Foo("derivedFoo initializer");
    
    public Derived()
    {
        Console.WriteLine("Derived constructor");
    }
}
