using System;

namespace FacadePattern
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
            Console.WriteLine("Rectangle::draw()");
        }
    }

    // 2b. Create concrete classes implementing the same interface
    public class Square : IShape
    {
        public void draw()
        {
            Console.WriteLine("Square::draw()");
        }
    }

    // 2c. Create concrete classes implementing the same interface
    public class Circle : IShape
    {
        public void draw()
        {
            Console.WriteLine("Circle::draw()");
        }
    }

    // 3. Create a facade class
    public class ShapeMaker
    {
        private IShape circle;
        private IShape rectangle;
        private IShape square;

        public ShapeMaker()
        {
            circle = new Circle();
            rectangle = new Rectangle();
            square = new Square();
        }

        public void drawCircle()
        {
            circle.draw();
        }
        public void drawRectangle()
        {
            rectangle.draw();
        }
        public void drawSquare()
        {
            square.draw();
        }
    }

    // 4. Use the facade to draw various types of shapes
    public class FacadePatternDemo
    {
        public static void Main(String[] args)
        {
            ShapeMaker shapeMaker = new ShapeMaker();

            shapeMaker.drawCircle();
            shapeMaker.drawRectangle();
            shapeMaker.drawSquare();

            Console.ReadKey();
        }
    }
}

// 5. Verify the output

// Circle::draw()
// Rectangle::draw()
// Square::draw()