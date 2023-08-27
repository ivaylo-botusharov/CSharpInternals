namespace InitializersRunInOppositeOrderAsConstructorsV2;

public class Base
{
    readonly Foo baseFoo = new Foo("baseFoo initializer");
    
    public Base()
    {
        Console.WriteLine("Base constructor");

        // would deref null if in Program.cs we are constructing an instance of Derived / MoreDerived
        // if (this is Derived)
        // {
        //     (this as Derived).DoIt();
        // }

        // would deref null if in Program.cs we are constructing an instance of MoreDerived
        // this.Blah();
    }
    
    public virtual void Blah()
    {
        Console.WriteLine("Base.Blah()");
    }
}
