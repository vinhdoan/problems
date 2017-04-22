using System;
using System.Collections.Generic; // for Dictionary (generic version of Hashtable)

namespace PrototypePattern
{
    // 1. Create an abstract class implementing Clonable interface
    public abstract class Shape : ICloneable
    {
        private String id;
        protected String type;

        public abstract void draw();

        public String getType()
        {
            return type;
        }

        public String getId()
        {
            return id;
        }

        public void setId(String id)
        {
            this.id = id;
        }

        public object Clone()
        {
            object clone = null;

            try
            {
                clone = base.MemberwiseClone();
            }
            catch (Exception e)
            {
                Console.WriteLine(e.StackTrace);
            }

            return clone;
        }
    }

    // 2a. Create concrete classes extending the above class
    public class Rectangle : Shape
    {
        public Rectangle()
        {
            type = "Rectangle";
        }

        public override void draw()
        {
            Console.WriteLine("Inside Rectangle::draw() method.");
       }
    }

    // 2b. Create concrete classes extending the above class
    public class Square : Shape
    {
        public Square()
        {
            type = "Square";
        }

        public override void draw()
        {
            Console.WriteLine("Inside Square::draw() method.");
        }
    }

    // 2c. Create concrete classes extending the above class
    public class Circle : Shape
    {
        public Circle()
        {
            type = "Circle";
        }

        public override void draw()
        {
            Console.WriteLine("Inside Circle::draw() method.");
        }
    }

    // 3. Create a class to get concrete classes from database and store them in a Dictionary
    public class ShapeCache
    {
        private static Dictionary<String, Shape> shapeMap  = new Dictionary<String, Shape>();

        public static Shape getShape(String shapeId)
        {
            Shape cachedShape;
            shapeMap.TryGetValue(shapeId, out cachedShape);
            return (Shape) cachedShape.Clone();
        }

        // for each shape run database query and create shape
        // shapeMap.Add(shapeKey, shape);
        // for example, we are adding 3 shapes
        public static void loadCache()
        {
            Circle circle = new Circle();
            circle.setId("1");
            shapeMap.Add(circle.getId(), circle);

            Square square = new Square();
            square.setId("2");
            shapeMap.Add(square.getId(), square);

            Rectangle rectangle = new Rectangle();
            rectangle.setId("3");
            shapeMap.Add(rectangle.getId(), rectangle);
       }
    }

    // 4. PrototypePatternDemo uses ShapeCache class to get clones of shapes stored in a Hashtable
    public class PrototypePatternDemo
    {
        public static void Main(String[] args)
        {
            ShapeCache.loadCache();

            Shape clonedShape = (Shape) ShapeCache.getShape("1");
            Console.WriteLine("Shape : " + clonedShape.getType());		

            Shape clonedShape2 = (Shape) ShapeCache.getShape("2");
            Console.WriteLine("Shape : " + clonedShape2.getType());		

            Shape clonedShape3 = (Shape) ShapeCache.getShape("3");
            Console.WriteLine("Shape : " + clonedShape3.getType());

            Console.ReadKey();
       }
    }
}

// 5. Verify the output

// Shape : Circle
// Shape : Square
// Shape : Rectangle