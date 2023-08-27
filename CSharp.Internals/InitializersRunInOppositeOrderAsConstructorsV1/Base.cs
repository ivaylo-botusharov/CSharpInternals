namespace InitializersRunInOppositeOrderAsConstructorsV1;

public class Base
{
    readonly Foo baseFoo = new Foo("baseFoo initializer");
    
    public Base()
    {
        Console.WriteLine("Base constructor");
    }
}
