# Initializers Run In The Opposite Order As Constructors (V1)

*Inspired by:* [Why Do Initializers Run In The Opposite Order As Constructors? - Part One](https://ericlippert.com/2008/02/15/why-do-initializers-run-in-the-opposite-order-as-constructors-part-one/)  
(by Eric Lippert)  

Class members initialization execution order:  

1. Instantiate 'Derived' class  

2. Initiliaze 'derivedFoo' field  

3. 'Foo' class constructor invocation  

4. 'Foo' class constructor body execution  

5. 'Derived' class constructor invocation  

6. 'Base' class baseFoo field initiliazation  

7. 'Foo' class constructor invocation  

8. 'Foo' class constructor body execution  

9. 'Base' class constructor invocation  

10. 'Base' class constructor body execution  

11. 'Derived' class constructor invocation  

12. 'Derived' class constructor body execution  


Printed output:  

```
Foo constructor: Derived initializer
Foo constructor: Base initializer
Base constructor
Derived constructor
```

------------

*References:*

Fabulous adventures in coding (Eric Lippert's blog)  

Eric Lippert's personal website:  

[Why Do Initializers Run In The Opposite Order As Constructors? Part One](https://ericlippert.com/2008/02/15/why-do-initializers-run-in-the-opposite-order-as-constructors-part-one/)  

[Why Do Initializers Run In The Opposite Order As Constructors? Part Two](https://ericlippert.com/2008/02/18/why-do-initializers-run-in-the-opposite-order-as-constructors-part-two/)  

Eric Lippert's blog on MS Learn:  

[Why Do Initializers Run In The Opposite Order As Constructors? Part One](https://learn.microsoft.com/en-us/archive/blogs/ericlippert/why-do-initializers-run-in-the-opposite-order-as-constructors-part-one)  

[Why Do Initializers Run In The Opposite Order As Constructors? Part Two](https://learn.microsoft.com/en-us/archive/blogs/ericlippert/why-do-initializers-run-in-the-opposite-order-as-constructors-part-two)  
