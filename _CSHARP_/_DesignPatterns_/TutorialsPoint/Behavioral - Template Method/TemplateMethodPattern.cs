using System;

namespace TemplateMethodPattern
{
    // 1. Create an abstract class with a template method being sealed
    public abstract class Game
    {
        public abstract void initialize();
        public abstract void startPlay();
        public abstract void endPlay();

        //template method
        public void play()
        {
            //initialize the game
            initialize();

            //start game
            startPlay();

            //end game
            endPlay();
        }
    }

    // 2a. Create concrete classes extending the above class
    public class Cricket : Game
    {
        public override void endPlay()
        {
            Console.WriteLine("Cricket Game Finished!");
        }

        public override void initialize()
        {
            Console.WriteLine("Cricket Game Initialized! Start playing.");
        }

        public override void startPlay()
        {
            Console.WriteLine("Cricket Game Started. Enjoy the game!");
        }
    }

    // 2b. Create concrete classes extending the above class
    public class Football : Game
    {
        public override void endPlay()
        {
            Console.WriteLine("Football Game Finished!");
        }

        public override void initialize()
        {
            Console.WriteLine("Football Game Initialized! Start playing.");
        }

        public override void startPlay()
        {
            Console.WriteLine("Football Game Started. Enjoy the game!");
        }
    }

    // 3. Use the Game's template method play() to demonstrate a defined way of playing game
    public class TemplatePatternDemo
    {
        public static void Main(String[] args)
        {
            Game game = new Cricket();
            game.play();
            Console.WriteLine();
            game = new Football();
            game.play();
            
            Console.ReadKey();
        }
    }
}

// 4. Verify the output

// Cricket Game Initialized! Start playing.
// Cricket Game Started. Enjoy the game!
// Cricket Game Finished!

// Football Game Initialized! Start playing.
// Football Game Started. Enjoy the game!
// Football Game Finished!