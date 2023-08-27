namespace InitializersRunInOppositeOrderAsConstructorsV2;

public class Derived : Base
{
    readonly Foo derivedFoo;
    
    public Derived()
    {
        Console.WriteLine("Derived constructor");
        this.derivedFoo = new Foo("derivedFoo initializer");
    }

    public void DoIt()
    {
        this.derivedFoo.Bar();
    }
}
