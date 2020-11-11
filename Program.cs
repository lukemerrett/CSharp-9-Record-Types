using System;
using System.Collections.Generic;

namespace CSharp_9_Record_Types
{
    class Program
    {
        public enum Colour { Black, Blue, Red, Green }
        public enum PenState { Up, Down }

        // Creates a Record type using the longhand syntax
        // The longhand still requires you to add a constructor and the underlying properties
        // Gives you more control over the constructor
        // However carries the same Record type benefits of equality checks
        public record Animal
        {
            public string Name { get; }

            public Animal(string name) => Name = name;
        }

        // Creates a Record type using the Positional Record syntax
        public record Position(int X, int Y);

        // Creates a Record type using the Positional Record syntax that inherits from another record type
        public record Turtle(string Name, Position Position, int Angle, PenState PenState, Colour Colour) : Animal(Name);

        static void Main(string[] _)
        {
            // Initialising a new record type like any other class
            var position = new Position(10, 20);
            
            // For Positional Records, you must include all properties in the order declared, as a constructor is created for you
            var turtle = new Turtle("Mr Scruff", position, 90, PenState.Down, Colour.Green);

            // Benefit 1 of Record Types: standardised & clean string output of the type
            Console.WriteLine(turtle); // Outputs: "Turtle { Name = Mr Scruff, position = Position { X = 10, Y = 20 }, Angle = 90, PenState = Down, Colour = Green }"

            /* Benefit 2 of Record Types: they are immutable, all properties are read-only and assigned through the constructor only
             * Encourages the use of immutability in your application, simplifying behaviour and testability.
            
                turtle.Name = "Jim";  // Will fail as the property is read-only
            */

            // Create an identical turtle to that above, but as a new instance with no shared reference types
            var otherTurtle = new Turtle("Mr Scruff", position, 90, PenState.Down, Colour.Green);

            // Benefit 3 of Record Types: equality is value based, it checks the equality of all properties inside the record plus the type name, rather than the references.
            // If these were normal classes, this would output false
            // However because all properties are the same, it outputs true
            Console.WriteLine(turtle == otherTurtle);

            // Benefit 4 of Record Types: you can use the WITH keyword to deep copy the record and change only a sub-set of values
            // The below copies all properties over, but updates the Colour property to a new value
            var copiedTurtle = turtle with { Colour = Colour.Blue };

            // Benefit 5 of Record Types: they can be deconstructed like tuples
            // Even with nested deconstruction as seen below with the position type
            var(_, _, angle, _, _) = copiedTurtle;
            var (name, (xPosition, yPosition), _, _, _) = copiedTurtle;
            Console.WriteLine($"{name} is at position {xPosition},{yPosition}");
        }
    }
}
