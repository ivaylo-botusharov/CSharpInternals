namespace InitializersRunInOppositeOrderAsConstructors;

public class Base
{
    readonly Foo baseFoo = new Foo("Base initializer");
    
    public Base()
    {
        Console.WriteLine("Base constructor");
    }
}
