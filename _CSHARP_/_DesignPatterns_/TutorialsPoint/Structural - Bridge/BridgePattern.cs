using System;

namespace BridgePattern
{
    // 1. Create bridge implementer interface
    public interface IDrawAPI
    {
        void drawCircle(int radius, int x, int y);
    }

    // 2a. Create concrete bridge implementer classes implementing the DrawAPI interface
    public class RedCircle : IDrawAPI
    {
        public void drawCircle(int radius, int x, int y)
        {
            Console.WriteLine("Drawing Circle[ color: red, radius: " + radius + ", x: " + x + ", " + y + "]");
        }
    }

    // 2b. Create concrete bridge implementer classes implementing the DrawAPI interface
    public class GreenCircle : IDrawAPI
    {
        public void drawCircle(int radius, int x, int y)
        {
            Console.WriteLine("Drawing Circle[ color: green, radius: " + radius + ", x: " + x + ", " + y + "]");
        }
    }

    // 3. Create an abstract class Shape using the DrawAPI interface
    public abstract class Shape
    {
        protected IDrawAPI drawAPI;
       
        protected Shape(IDrawAPI drawAPI)
        {
            this.drawAPI = drawAPI;
        }

        public abstract void draw();
    }

    // 4. Create concrete class implementing the Shape interface
    public class Circle : Shape
    {
        private int x, y, radius;

        public Circle(int x, int y, int radius, IDrawAPI drawAPI) : base(drawAPI)
        {
            this.x = x;  
            this.y = y;  
            this.radius = radius;
        }

        public override void draw()
        {
            drawAPI.drawCircle(radius,x,y);
        }
    }

    // 5. Use the Shape and DrawAPI classes to draw different colored circles
    public class BridgePatternDemo
    {
       public static void Main(String[] args)
        {
            Shape redCircle = new Circle(100, 100, 10, new RedCircle());
            Shape greenCircle = new Circle(100, 100, 10, new GreenCircle());

            redCircle.draw();
            greenCircle.draw();

            Console.ReadKey();
        }
    }
}

// 6. Verify the output

// Drawing Circle[ color: red, radius: 10, x: 100, 100]
// Drawing Circle[ color: green, radius: 10, x: 100, 100]