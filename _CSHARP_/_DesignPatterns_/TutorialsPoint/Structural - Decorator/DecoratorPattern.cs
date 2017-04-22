using System;

namespace DecoratorPattern
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
            Console.WriteLine("Shape: Rectangle");
        }
    }

    // 2b. Create concrete classes implementing the same interface
    public class Circle : IShape
    {
        public void draw()
        {
            Console.WriteLine("Shape: Circle");
        }
    }

    // 3. Create abstract decorator class implementing the Shape interface
    public abstract class ShapeDecorator : IShape
    {
        protected IShape decoratedShape;

        public ShapeDecorator(IShape decoratedShape)
        {
            this.decoratedShape = decoratedShape;
        }

        public virtual void draw()
        {
            decoratedShape.draw();
        }
    }

    // 4. Create concrete decorator class extending the ShapeDecorator class
    public class RedShapeDecorator : ShapeDecorator
    {
        public RedShapeDecorator(IShape decoratedShape) : base(decoratedShape) {}

        public override void draw()
        {
            decoratedShape.draw();
            setRedBorder(decoratedShape);
        }

        private void setRedBorder(IShape decoratedShape)
        {
            Console.WriteLine("Border Color: Red");
        }
    }

    // 5. Use the RedShapeDecorator to decorate Shape objects
    public class DecoratorPatternDemo
    {
        public static void Main(String[] args)
        {
            IShape circle = new Circle();

            IShape redCircle = new RedShapeDecorator(new Circle());

            IShape redRectangle = new RedShapeDecorator(new Rectangle());
            Console.WriteLine("Circle with normal border");
            circle.draw();

            Console.WriteLine("\nCircle of red border");
            redCircle.draw();

            Console.WriteLine("\nRectangle of red border");
            redRectangle.draw();
            
            Console.ReadKey();
        }
    }
}

// 6. Verify the output

// Circle with normal border
// Shape: Circle

// Circle of red border
// Shape: Circle
// Border Color: Red

// Rectangle of red border
// Shape: Rectangle
// Border Color: Red