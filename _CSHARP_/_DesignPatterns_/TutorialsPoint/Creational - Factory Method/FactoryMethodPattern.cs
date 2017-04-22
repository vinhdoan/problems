using System;

namespace FactoryMethodPattern
{
    // 1. Create an interface
    public interface IShape
    {
        void draw();
    }

    // 2a. Create concrete classes implementing the same interface
    public class Rectangle : IShape
    {
        public void draw()
        {
            Console.WriteLine("Inside Rectangle::draw() method.");
        }
    }

    // 2b. Create concrete classes implementing the same interface
    public class Square : IShape
    {
        public void draw()
        {
            Console.WriteLine("Inside Square::draw() method.");
        }
    }

    // 2c. Create concrete classes implementing the same interface
    public class Circle : IShape
    {
        public void draw() {
            Console.WriteLine("Inside Circle::draw() method.");
        }
    }

    // 3. Create a Factory to generate object of concrete class based on given information
    public class ShapeFactory
    {
        //use getShape method to get object of type shape 
        public IShape getShape(String shapeType)
        {
            if(shapeType == null)
            {
                return null;
            }

            if(String.Equals(shapeType, "CIRCLE", StringComparison.OrdinalIgnoreCase))
            {
                return new Circle();
            }
            else if(String.Equals(shapeType, "RECTANGLE", StringComparison.OrdinalIgnoreCase))
            {
                return new Rectangle();
            }
            else if(String.Equals(shapeType, "SQUARE", StringComparison.OrdinalIgnoreCase))
            {
                return new Square();
            }
            return null;
        }
    }

    // 4. Use the Factory to get object of concrete class by passing an information such as type
    public class FactoryPatternDemo
    {
        public static void Main(String[] args)
        {
            ShapeFactory shapeFactory = new ShapeFactory();

            //get an object of Circle and call its draw method.
            IShape shape1 = shapeFactory.getShape("CIRCLE");

            //call draw method of Circle
            shape1.draw();

            //get an object of Rectangle and call its draw method.
            IShape shape2 = shapeFactory.getShape("RECTANGLE");

            //call draw method of Rectangle
            shape2.draw();

            //get an object of Square and call its draw method.
            IShape shape3 = shapeFactory.getShape("SQUARE");

            //call draw method of circle
            shape3.draw();
            
            Console.ReadKey();
        }
    }
}

// 5. Verify the output

// Inside Circle::draw() method.
// Inside Rectangle::draw() method.
// Inside Square::draw() method.