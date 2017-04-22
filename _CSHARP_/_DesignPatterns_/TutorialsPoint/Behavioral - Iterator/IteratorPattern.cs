using System;

namespace IteratorPattern
{
    // 1a. Create interfaces
    public interface IIterator
    {
        bool hasNext();
        object next();
    }
    
    // 1b. Create interfaces
    public interface IContainer
    {
        IIterator getIterator();
    }

    // 2a. Create concrete class implementing the Container interface
    public class NameRepository : IContainer
    {
        public static String[] names = {"Robert" , "John" ,"Julie" , "Lora"};

        public IIterator getIterator()
        {
            return new NameIterator();
        }

        // 2b. This class has inner class NameIterator implementing the Iterator interface
        private class NameIterator : IIterator
        {
            int index;

            public bool hasNext()
            {
                if(index < names.Length)
                {
                    return true;
                }
                return false;
            }

            public object next()
            {
                if(this.hasNext())
                {
                    return names[index++];
                }
                return null;
            }
        }
    }

    // 3. Use the NameRepository to get iterator and print names
    public class IteratorPatternDemo
    {
        public static void Main(String[] args)
        {
            NameRepository namesRepository = new NameRepository();

            for(IIterator iter = namesRepository.getIterator(); iter.hasNext();)
            {
                String name = (String)iter.next();
                Console.WriteLine("Name : " + name);
            }
            
            Console.ReadKey();
        }
    }
}

// 4. Verify the output

// Name : Robert
// Name : John
// Name : Julie
// Name : Lora