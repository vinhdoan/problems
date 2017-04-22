using System;

namespace ProxyPattern
{
    // 1. Create an interface
    public interface IImage
    {
        void display();
    }
    
    // 2a. Create concrete classes implementing the same interface
    public class RealImage : IImage
    {
        private String fileName;

        public RealImage(String fileName)
        {
            this.fileName = fileName;
            loadFromDisk(fileName);
        }

        public void display()
        {
            Console.WriteLine("Displaying " + fileName);
        }

        private void loadFromDisk(String fileName)
        {
            Console.WriteLine("Loading " + fileName);
        }
    }

    // 2b. Create concrete classes implementing the same interface
    public class ProxyImage : IImage
    {
        private RealImage realImage;
        private String fileName;

        public ProxyImage(String fileName)
        {
            this.fileName = fileName;
        }

        public void display()
        {
            if(realImage == null)
            {
                realImage = new RealImage(fileName);
            }
            realImage.display();
        }
    }

    // 3. Use the ProxyImage to get object of RealImage class when required
    public class ProxyPatternDemo
    {
        public static void Main(String[] args)
        {
            IImage image = new ProxyImage("test_10mb.jpg");

            //image will be loaded from disk
            image.display(); 
            Console.WriteLine("");

            //image will not be loaded from disk
            image.display();
            
            Console.ReadKey();
        }
    }
}

// 4. Verify the output

// Loading test_10mb.jpg
// Displaying test_10mb.jpg

// Displaying test_10mb.jpg