using System;
using System.Collections.Generic;
using static System.Console;
using Users;

// var p1 = new Person(name: "John Doe", age: 23);
var p1 = new Person
{
    Name = "John Doe",
    Age = 23
};

p1.Walk();

WriteLine($"p1.ToString(): {p1}");
WriteLine($"p1.Name: {p1.Name}");
WriteLine($"p1.Age: {p1.Age}");

WriteLine();

var p2 = new Person
{
    Name = "John Doe",
    Age = 23
};

WriteLine($"p1 == p2: {p1 == p2}");
WriteLine($"p1.Equals(p2): {p1.Equals(p2)}");
WriteLine($"EqualityComparer<Person>.Default.Equals(p1, p2): {EqualityComparer<Person>.Default.Equals(p1, p2)}");
WriteLine($"Object.Equals(p1, p2): {Object.Equals(p1, p2)}");
WriteLine($"Object.ReferenceEquals(p1, p2): {Object.ReferenceEquals(p1, p2)}");

namespace Users
{
    public record Person
    {
        public string Name { get; init; }
        
        public int Age { get; init;  }
        
        public void Walk()
        {
            Console.WriteLine($"{Name} started walking...");
        }
    }
}