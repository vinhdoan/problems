using System;
using System.Collections.Generic;   // for List<T>

namespace CommandPattern
{
    // 1. Create a command interface
    public interface IOrder
    {
        void execute();
    }

    // 2. Create a request class
    public class Stock
    {
        private String name = "ABC";
        private int quantity = 10;

        public void buy()
        {
            Console.WriteLine("Stock [ Name: " + name + ", Quantity: " + quantity + " ] bought");
        }

        public void sell()
        {
            Console.WriteLine("Stock [ Name: "+name+", Quantity: " + quantity +" ] sold");
        }
    }

    // 3. Create concrete classes implementing the Order interface
    public class BuyStock : IOrder
    {
        private Stock abcStock;

        public BuyStock(Stock abcStock)
        {
            this.abcStock = abcStock;
        }

        public void execute()
        {
            abcStock.buy();
        }
    }

    public class SellStock : IOrder
    {
        private Stock abcStock;

        public SellStock(Stock abcStock)
        {
            this.abcStock = abcStock;
        }

        public void execute() {
            abcStock.sell();
        }
    }

    // 4. Create command invoker class
    public class Broker
    {
        private List<IOrder> orderList = new List<IOrder>();

        public void takeOrder(IOrder order)
        {
            orderList.Add(order);
        }

        public void placeOrders()
        {
            foreach (IOrder order in orderList)
            {
                order.execute();
            }
            orderList.Clear();
        }
    }

    // 5. Use the Broker class to take and execute commands
    public class CommandPatternDemo
    {
        public static void Main(String[] args)
        {
            Stock abcStock = new Stock();

            BuyStock buyStockOrder = new BuyStock(abcStock);
            SellStock sellStockOrder = new SellStock(abcStock);

            Broker broker = new Broker();
            broker.takeOrder(buyStockOrder);
            broker.takeOrder(sellStockOrder);

            broker.placeOrders();
            
            Console.ReadKey();
        }
    }
}

// 6. Verify the output

// Stock [ Name: ABC, Quantity: 10 ] bought
// Stock [ Name: ABC, Quantity: 10 ] sold