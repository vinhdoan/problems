using System;

namespace StrategyPattern
{
    // 1. Create an interface
    public interface IStrategy
    {
        int doOperation(int num1, int num2);
    }

    // 2a. Create concrete classes implementing the same interface
    public class OperationAdd : IStrategy
    {
        public int doOperation(int num1, int num2)
        {
            return num1 + num2;
        }
    }

    // 2b. Create concrete classes implementing the same interface
    public class OperationSubstract : IStrategy
    {
        public int doOperation(int num1, int num2)
        {
            return num1 - num2;
        }
    }

    // 2c. Create concrete classes implementing the same interface
    public class OperationMultiply : IStrategy
    {
        public int doOperation(int num1, int num2)
        {
            return num1 * num2;
        }
    }

    // 3. Create Context Class
    public class Context
    {
        private IStrategy strategy;

        public Context(IStrategy strategy)
        {
            this.strategy = strategy;
        }

        public int executeStrategy(int num1, int num2)
        {
            return strategy.doOperation(num1, num2);
        }
    }

    // 4. Use the Context to see change in behaviour when it changes its Strategy
    public class StrategyPatternDemo
    {
        public static void Main(String[] args)
        {
            Context context = new Context(new OperationAdd());		
            Console.WriteLine("10 + 5 = " + context.executeStrategy(10, 5));

            context = new Context(new OperationSubstract());		
            Console.WriteLine("10 - 5 = " + context.executeStrategy(10, 5));

            context = new Context(new OperationMultiply());		
            Console.WriteLine("10 * 5 = " + context.executeStrategy(10, 5));

            Console.ReadKey();
        }
    }
}

// 5. Verify the output

// 10 + 5 = 15
// 10 - 5 = 5
// 10 * 5 = 50