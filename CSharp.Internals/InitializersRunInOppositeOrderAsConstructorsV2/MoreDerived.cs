namespace InitializersRunInOppositeOrderAsConstructorsV2;

public class MoreDerived : Derived
{
    public MoreDerived()
    {        
    }
    
    public override void Blah()
    {
        this.DoIt();
    }
}