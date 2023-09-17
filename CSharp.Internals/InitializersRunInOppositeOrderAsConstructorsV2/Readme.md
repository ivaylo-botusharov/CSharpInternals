# Initializers Run In The Opposite Order As Constructors (V2)

*Inspired by:* [Why Do Initializers Run In The Opposite Order As Constructors? - Part Two](https://ericlippert.com/2008/02/18/why-do-initializers-run-in-the-opposite-order-as-constructors-part-two/)  
(by Eric Lippert)  

Class members initialization execution order:  

## Constructing an instance of Derived / MoreDerived 
and uncommenting in Base constructor:

```CSharp
if (this is Derived)
{
    (this as Derived).DoIt();
}
```
leads to NullReferenceException

## Constructing an instance of MoreDerived

Printed output:  

```
TODO
```

------------

*References:*

Fabulous adventures in coding (Eric Lippert's blog)  

Eric Lippert's personal website:  

[Why Do Initializers Run In The Opposite Order As Constructors? Part One](https://ericlippert.com/2008/02/15/why-do-initializers-run-in-the-opposite-order-as-constructors-part-one/)  

[Why Do Initializers Run In The Opposite Order As Constructors? Part Two](https://ericlippert.com/2008/02/18/why-do-initializers-run-in-the-opposite-order-as-constructors-part-two/)  

Eric Lippert's archived blog on MS Learn:  

[Why Do Initializers Run In The Opposite Order As Constructors? Part One](https://learn.microsoft.com/en-us/archive/blogs/ericlippert/why-do-initializers-run-in-the-opposite-order-as-constructors-part-one)  

[Why Do Initializers Run In The Opposite Order As Constructors? Part Two](https://learn.microsoft.com/en-us/archive/blogs/ericlippert/why-do-initializers-run-in-the-opposite-order-as-constructors-part-two)  
