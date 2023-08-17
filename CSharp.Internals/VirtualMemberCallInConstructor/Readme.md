# Virtual member call in Constructor

*Question:* Is it ok to call C# class virtual member within a constructor?

**Conclusion:** It is not recommended to invoke virtual member in a constructor, since it can cause unexpected behavior, in our case: NullReferenceException. This is due to the specifics of class members initialization execution order and the execution of the Derived class overriden DoSomething() method before the Derived class constructor body.

Our scenario: we have a parent class "Parent" and within it's constructor the virtual method "DoSomething" is invoked.

We have class "Derived" which inherits from "Parent" and overrides the "DoSomething" method. In it's constructor *Derived()* a field "foo" is initialized. The same field is supposed to be printed in the overriden "DoSomething()" method, but there is a NullReferenceException, since Derived() constructor is not invoked ahead of the overriden "DoSomething()" method in *Derived* class.

## Execution order:

1. Child - 'bar' field initialization
2. Child - 'Baz' property initialization
3. Child - default constructor invocation
4. Parent - 'fred' field initialization
5. Parent - 'Waldo' property initialization
6. Parent - constructor invocation
7. Parent - constructor body execution, calls Child's overriden DoSomething() method
8. Child - overriden DoSomething() method execution:  
8.1. Makes bar's value to lowercase and prints it.  
8.2. Tries to lowercase foo's value and print it, but gets NullReferenceException since foo is null  

*Note:* Child - DoSomething() - if we comment ```Console.WriteLine(this.foo.ToLower());``` we will notice that Child's default constructor body is executed after the Child's DoSmething() method and foo field is initialized.  

-------------

*References:*

[Virtual member call in a constructor](https://stackoverflow.com/questions/119506/virtual-member-call-in-a-constructor)  
