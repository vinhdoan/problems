using System;

namespace AbstractFactoryPattern
{
    // 1. Create an interface for Shapes
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
        public void draw()
        {
            Console.WriteLine("Inside Circle::draw() method.");
        }
    }
    
    // 3. Create an interface for Colors
    public interface IColor
    {
        void fill();
    }
    
    // 4a. Create concrete classes implementing the same interface
    public class Red : IColor
    {
        public void fill()
        {
            Console.WriteLine("Inside Red::fill() method.");
        }
    }

    // 4b. Create concrete classes implementing the same interface
    public class Green : IColor
    {
        public void fill()
        {
            Console.WriteLine("Inside Green::fill() method.");
        }
    }

    // 4c. Create concrete classes implementing the same interface
    public class Blue : IColor
    {
        public void fill()
        {
            Console.WriteLine("Inside Blue::fill() method.");
        }
    }
    
    // 5. Create an Abstract class to get factories for Color and Shape Objects
    public abstract class AbstractFactory
    {
        public abstract IColor getColor(String color);
        public abstract IShape getShape(String shape);
    }
    
    // 6a. Create Factory classes extending AbstractFactory to generate object of concrete class based on given information
    public class ShapeFactory : AbstractFactory
    {
        public override IShape getShape(String shapeType)
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

        public override IColor getColor(String color)
        {
            return null;
        }
    }

    // 6b. Create Factory classes extending AbstractFactory to generate object of concrete class based on given information
    public class ColorFactory : AbstractFactory
    {
        public override IShape getShape(String shapeType)
        {
            return null;
        }
   
        public override IColor getColor(String color)
        {
            if(color == null)
            {
                return null;
            }

            if(String.Equals(color, "RED", StringComparison.OrdinalIgnoreCase))
            {
                return new Red();
            }
            else if(String.Equals(color, "GREEN", StringComparison.OrdinalIgnoreCase))
            {
                return new Green();
            }
            else if(String.Equals(color, "BLUE", StringComparison.OrdinalIgnoreCase))
            {
                return new Blue();
            }
            return null;
        }
    }
    
    // 7. Create a Factory generator/producer class to get factories by passing an information such as Shape or Color
    public class FactoryProducer
    {
        public static AbstractFactory getFactory(String choice)
        {
            if(String.Equals(choice, "SHAPE", StringComparison.OrdinalIgnoreCase))
            {
                return new ShapeFactory();
            }
            else if(String.Equals(choice, "COLOR", StringComparison.OrdinalIgnoreCase))
            {
                return new ColorFactory();
            }
            return null;
        }
    }

    // 8. Use the FactoryProducer to get AbstractFactory in order to get factories of concrete classes by passing an information such as type
    public class AbstractFactoryPatternDemo
    {
        public static void Main(String[] args)
        {
            //get shape factory
            AbstractFactory shapeFactory = FactoryProducer.getFactory("SHAPE");

            //get an object of Shape Circle
            IShape shape1 = shapeFactory.getShape("CIRCLE");

            //call draw method of Shape Circle
            shape1.draw();

            //get an object of Shape Rectangle
            IShape shape2 = shapeFactory.getShape("RECTANGLE");

            //call draw method of Shape Rectangle
            shape2.draw();

            //get an object of Shape Square 
            IShape shape3 = shapeFactory.getShape("SQUARE");

            //call draw method of Shape Square
            shape3.draw();

            //get color factory
            AbstractFactory colorFactory = FactoryProducer.getFactory("COLOR");

            //get an object of Color Red
            IColor color1 = colorFactory.getColor("RED");

            //call fill method of Red
            color1.fill();

            //get an object of Color Green
            IColor color2 = colorFactory.getColor("Green");

            //call fill method of Green
            color2.fill();

            //get an object of Color Blue
            IColor color3 = colorFactory.getColor("BLUE");

            //call fill method of Color Blue
            color3.fill();
            
            Console.ReadKey();
        }
    }
}

// 9. Verify the output
// Inside Circle::draw() method.
// Inside Rectangle::draw() method.
// Inside Square::draw() method.
// Inside Red::fill() method.
// Inside Green::fill() method.
// Inside Blue::fill() method.