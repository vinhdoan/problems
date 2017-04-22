using System;
using System.Collections.Generic;   // for List<T>

namespace BuilderPattern
{
    // 1a. Create an interface Item representing food item and packing
    public interface IItem
    {
        String name();
        IPacking packing();
        float price();
    }

    // 1b. Create an interface Item representing food item and packing
    public interface IPacking
    {
        String pack();
    }

    // 2a. Create concrete classes implementing the Packing interface
    public class Wrapper : IPacking
    {
        public String pack()
        {
            return "Wrapper";
        }
    }

    // 2b. Create concrete classes implementing the Packing interface
    public class Bottle : IPacking
    {
        public String pack()
        {
            return "Bottle";
        }
    }

    // 3a. Create abstract classes implementing the Item interface providing default functionalities
    public abstract class Burger : IItem
    {
        public abstract String name();

        public IPacking packing()
        {
            return new Wrapper();
        }

        public abstract float price();
    }

    // 3b. Create abstract classes implementing the Item interface providing default functionalities
    public abstract class ColdDrink : IItem
    {
        public abstract String name();

        public IPacking packing()
        {
           return new Bottle();
        }

        public abstract float price();
    }

    // 4a. Create concrete classes extending Burger and ColdDrink classes
    public class VegBurger : Burger
    {
        public override float price()
        {
            return 25.0f;
        }

        public override String name()
        {
            return "Veg Burger";
        }
    }

    // 4b. Create concrete classes extending Burger and ColdDrink classes
    public class ChickenBurger : Burger
    {
        public override float price()
        {
            return 50.5f;
        }

        public override String name()
        {
            return "Chicken Burger";
        }
    }

    // 4c. Create concrete classes extending Burger and ColdDrink classes
    public class Coke : ColdDrink
    {
        public override float price()
        {
            return 30.0f;
        }

        public override String name()
        {
            return "Coke";
        }
    }

    // 4d. Create concrete classes extending Burger and ColdDrink classes
    public class Pepsi : ColdDrink 
    {
        public override float price()
        {
            return 35.0f;
        }

        public override String name()
        {
            return "Pepsi";
        }
    }

    // 5. Create a Meal class having Item objects defined above
    public class Meal
    {
        private List<IItem> items = new List<IItem>();

        public void addItem(IItem item)
        {
            items.Add(item);
        }

        public float getCost()
        {
            float cost = 0.0f;

            foreach (IItem item in items)
            {
                cost += item.price();
            }
            return cost;
        }

        public void showItems()
        {
            foreach (IItem item in items)
            {
                Console.Write("Item : " + item.name());
                Console.Write(", Packing : " + item.packing().pack());
                Console.WriteLine(", Price : " + item.price());
            }
        }
    }
    
    // 6. Create a MealBuilder class, the actual builder class responsible to create Meal objects
    public class MealBuilder
    {
        public Meal prepareVegMeal()
        {
            Meal meal = new Meal();
            meal.addItem(new VegBurger());
            meal.addItem(new Coke());
            return meal;
        }   

        public Meal prepareNonVegMeal()
        {
            Meal meal = new Meal();
            meal.addItem(new ChickenBurger());
            meal.addItem(new Pepsi());
            return meal;
        }
    }

    // 7. BuiderPatternDemo uses MealBuider to demonstrate builder pattern
    public class BuilderPatternDemo
    {
        public static void Main(String[] args)
        {
            MealBuilder mealBuilder = new MealBuilder();

            Meal vegMeal = mealBuilder.prepareVegMeal();
            Console.WriteLine("Veg Meal");
            vegMeal.showItems();
            Console.WriteLine("Total Cost: " + vegMeal.getCost());

            Meal nonVegMeal = mealBuilder.prepareNonVegMeal();
            Console.WriteLine("\n\nNon-Veg Meal");
            nonVegMeal.showItems();
            Console.WriteLine("Total Cost: " + nonVegMeal.getCost());
            
            Console.ReadKey();
        }
    }
}

// 8. Verify the output

// Veg Meal
// Item : Veg Burger, Packing : Wrapper, Price : 25.0
// Item : Coke, Packing : Bottle, Price : 30.0
// Total Cost: 55.0


// Non-Veg Meal
// Item : Chicken Burger, Packing : Wrapper, Price : 50.5
// Item : Pepsi, Packing : Bottle, Price : 35.0
// Total Cost: 85.5