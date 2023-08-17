namespace InitializersRunInOppositeOrderAsConstructors;

public class Foo
{
    public Foo(string s)
    {
        Console.WriteLine("Foo constructor: {0}", s);
    }
    
    public void Bar() {}
}
