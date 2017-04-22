using System;
using System.Collections.Generic; // for Dictionary (generic version of Hashtable)

namespace FlyweightPattern
{
    // 1. Create an interface
    public interface IShape {
        void draw();
    }

    // 2. Create concrete class implementing the same interface
    public class Circle : IShape {
        private String color;
        private int x;
        private int y;
        private int radius;

        public Circle(String color)
        {
            this.color = color;
        }

        public void setX(int x)
        {
            this.x = x;
        }

        public void setY(int y)
        {
            this.y = y;
        }

        public void setRadius(int radius)
        {
            this.radius = radius;
        }

        public void draw()
        {
            Console.WriteLine("Circle: Draw() [Color : " + color + ", x : " + x + ", y :" + y + ", radius :" + radius);
        }
    }

    // 3. Create a factory to generate object of concrete class based on given information
    public class ShapeFactory
    {
        private static readonly Dictionary<String, IShape> circleMap = new Dictionary<String, IShape>();

        public static IShape getCircle(String color)
        {
            // Circle circle = (Circle)circleMap.get(color);
            IShape circleIShape;
            circleMap.TryGetValue(color, out circleIShape);
            Circle circle = (Circle)circleIShape;

            if(circle == null)
            {
                circle = new Circle(color);
                circleMap.Add(color, circle);
                Console.WriteLine("Creating circle of color : " + color);
            }
            return circle;
        }
    }
    
    // 4. Use the factory to get object of concrete class by passing an information such as color
    public class FlyweightPatternDemo
    {
        private static readonly String[] colors = { "Red", "Green", "Blue", "White", "Black" };
        private static Random rnd = new Random();
        public static void Main(String[] args)
        {
            for(int i=0; i < 20; ++i)
            {
                Circle circle = (Circle)ShapeFactory.getCircle(getRandomColor());
                circle.setX(getRandomX());
                circle.setY(getRandomY());
                circle.setRadius(100);
                circle.draw();
            }

            Console.ReadKey();
        }

        private static String getRandomColor()
        {
            return colors[rnd.Next(0, colors.Length)];
        }
        private static int getRandomX()
        {
            return rnd.Next(0, 100);
        }
        private static int getRandomY()
        {
            return rnd.Next(0, 100);
        }
    }
}

// 5. Verify the output (only an example from random generators)

// Creating circle of color : Black
// Circle: Draw() [Color : Black, x : 36, y :71, radius :100
// Creating circle of color : Green
// Circle: Draw() [Color : Green, x : 27, y :27, radius :100
// Creating circle of color : White
// Circle: Draw() [Color : White, x : 64, y :10, radius :100
// Creating circle of color : Red
// Circle: Draw() [Color : Red, x : 15, y :44, radius :100
// Circle: Draw() [Color : Green, x : 19, y :10, radius :100
// Circle: Draw() [Color : Green, x : 94, y :32, radius :100
// Circle: Draw() [Color : White, x : 69, y :98, radius :100
// Creating circle of color : Blue
// Circle: Draw() [Color : Blue, x : 13, y :4, radius :100
// Circle: Draw() [Color : Green, x : 21, y :21, radius :100
// Circle: Draw() [Color : Blue, x : 55, y :86, radius :100
// Circle: Draw() [Color : White, x : 90, y :70, radius :100
// Circle: Draw() [Color : Green, x : 78, y :3, radius :100
// Circle: Draw() [Color : Green, x : 64, y :89, radius :100
// Circle: Draw() [Color : Blue, x : 3, y :91, radius :100
// Circle: Draw() [Color : Blue, x : 62, y :82, radius :100
// Circle: Draw() [Color : Green, x : 97, y :61, radius :100
// Circle: Draw() [Color : Green, x : 86, y :12, radius :100
// Circle: Draw() [Color : Green, x : 38, y :93, radius :100
// Circle: Draw() [Color : Red, x : 76, y :82, radius :100
// Circle: Draw() [Color : Blue, x : 95, y :82, radius :100