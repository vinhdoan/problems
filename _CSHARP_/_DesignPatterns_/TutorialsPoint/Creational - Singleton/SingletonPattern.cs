using System;

namespace AbstractFactoryPattern
{
    // 1. Create a Singleton Class
    public class SingleObject
    {
        //create an object of SingleObject
        private static SingleObject instance = new SingleObject();

        //make the constructor private so that this class cannot be instantiated
        private SingleObject() {}

        //Get the only object available
        public static SingleObject getInstance()
        {
            return instance;
        }

        public void showMessage()
        {
            Console.WriteLine("Hello World!");
        }
    }

    // 2. Get the only object from the singleton class
    public class SingletonPatternDemo
    {
        public static void Main(String[] args)
        {
            //illegal construct
            //Compile Time Error: The constructor SingleObject() is not visible
            //SingleObject object = new SingleObject();

            //Get the only object available
            SingleObject object_ins = SingleObject.getInstance();

            //show the message
            object_ins.showMessage();
            
            Console.ReadKey();
        }
    }
}

// 3. Verify the output

// Hello World!