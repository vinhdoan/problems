using System;

namespace VisitorPattern
{
    // 1. Define an interface to represent element
    public interface IComputerPart
    {
        void accept(IComputerPartVisitor computerPartVisitor);
    }

    // 2a. Create concrete classes extending the above class
    public class Keyboard : IComputerPart
    {
        public void accept(IComputerPartVisitor computerPartVisitor)
        {
            computerPartVisitor.visit(this);
        }
    }

    // 2b. Create concrete classes extending the above class
    public class Monitor : IComputerPart
    {
        public void accept(IComputerPartVisitor computerPartVisitor)
        {
            computerPartVisitor.visit(this);
        }
    }

    // 2c. Create concrete classes extending the above class
    public class Mouse : IComputerPart
    {
        public void accept(IComputerPartVisitor computerPartVisitor)
        {
            computerPartVisitor.visit(this);
        }
    }

    // 2d. Create concrete classes extending the above class
    public class Computer : IComputerPart
    {
        IComputerPart[] parts;

        public Computer()
        {
            parts = new IComputerPart[] {new Mouse(), new Keyboard(), new Monitor()};
        } 

        public void accept(IComputerPartVisitor computerPartVisitor)
        {
            for (int i = 0; i < parts.Length; i++)
            {
                parts[i].accept(computerPartVisitor);
            }
            computerPartVisitor.visit(this);
        }
    }

    // 3. Define an interface to represent visitor
    public interface IComputerPartVisitor
    {
        void visit(Computer computer);
        void visit(Mouse mouse);
        void visit(Keyboard keyboard);
        void visit(Monitor monitor);
    }

    // 4. Create concrete visitor implementing the above class
    public class ComputerPartDisplayVisitor : IComputerPartVisitor
    {
        public void visit(Computer computer)
        {
            Console.WriteLine("Displaying Computer.");
        }

        public void visit(Mouse mouse)
        {
            Console.WriteLine("Displaying Mouse.");
        }

        public void visit(Keyboard keyboard)
        {
            Console.WriteLine("Displaying Keyboard.");
        }

        public void visit(Monitor monitor)
        {
            Console.WriteLine("Displaying Monitor.");
        }
    }

    // 5. Use the ComputerPartDisplayVisitor to display parts of Computer
    public class VisitorPatternDemo
    {
        public static void Main(String[] args)
        {
            IComputerPart computer = new Computer();
            computer.accept(new ComputerPartDisplayVisitor());
            
            Console.ReadKey();
        }
    }
}

// 6. Verify the output

// Displaying Mouse.
// Displaying Keyboard.
// Displaying Monitor.
// Displaying Computer.