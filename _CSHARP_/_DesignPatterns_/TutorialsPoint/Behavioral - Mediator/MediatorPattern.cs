using System;

namespace MediatorPattern
{
    // 1. Create mediator class
    public class ChatRoom
    {
        public static void showMessage(User user, String message)
        {
            Console.WriteLine(DateTime.Today + " [" + user.getName() + "] : " + message);
        }
    }

    // 2. Create user class
    public class User
    {
        private String name;

        public String getName()
        {
            return name;
        }

        public void setName(String name)
        {
            this.name = name;
        }

        public User(String name)
        {
            this.name  = name;
        }

        public void sendMessage(String message)
        {
            ChatRoom.showMessage(this, message);
        }
    }

    // 3. Use the User object to show communications between them
    public class MediatorPatternDemo
    {
        public static void Main(String[] args)
        {
            User robert = new User("Robert");
            User john = new User("John");

            robert.sendMessage("Hi! John!");
            john.sendMessage("Hello! Robert!");
            
            Console.ReadKey();
        }
    }
}

// 4. Verify the output

// Thu Jan 31 16:05:46 IST 2013 [Robert] : Hi! John!
// Thu Jan 31 16:05:46 IST 2013 [John] : Hello! Robert!