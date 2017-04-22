using System;

namespace InterpreterPattern
{
    // 1. Create an expression interface
    public interface IExpression
    {
        bool interpret(String context);
    }

    // 2a. Create concrete classes implementing the above interface
    public class TerminalExpression : IExpression
    {
        private String data;

        public TerminalExpression(String data)
        {
            this.data = data; 
        }

        public bool interpret(String context)
        {
            if(context.Contains(data))
            {
                return true;
            }
            return false;
        }
    }

    // 2b. Create concrete classes implementing the above interface
    public class OrExpression : IExpression
    {
        private IExpression expr1 = null;
        private IExpression expr2 = null;

        public OrExpression(IExpression expr1, IExpression expr2)
        { 
            this.expr1 = expr1;
            this.expr2 = expr2;
        }

        public bool interpret(String context)
        {
            return expr1.interpret(context) || expr2.interpret(context);
        }
    }

    // 2c. Create concrete classes implementing the above interface
    public class AndExpression : IExpression
    {
        private IExpression expr1 = null;
        private IExpression expr2 = null;

        public AndExpression(IExpression expr1, IExpression expr2)
        { 
            this.expr1 = expr1;
            this.expr2 = expr2;
        }

        public bool interpret(String context)
        {
            return expr1.interpret(context) && expr2.interpret(context);
        }
    }

    // 3. InterpreterPatternDemo uses Expression class to create rules and then parse them
    public class InterpreterPatternDemo
    {
        //Rule: Robert and John are male
        public static IExpression getMaleExpression()
        {
            IExpression robert = new TerminalExpression("Robert");
            IExpression john = new TerminalExpression("John");
            return new OrExpression(robert, john);
        }

        //Rule: Julie is a married women
        public static IExpression getMarriedWomanExpression()
        {
            IExpression julie = new TerminalExpression("Julie");
            IExpression married = new TerminalExpression("Married");
            return new AndExpression(julie, married);
        }

        public static void Main(String[] args)
        {
            IExpression isMale = getMaleExpression();
            IExpression isMarriedWoman = getMarriedWomanExpression();

            Console.WriteLine("John is male? " + isMale.interpret("John"));
            Console.WriteLine("Julie is a married women? " + isMarriedWoman.interpret("Married Julie"));
            
            Console.ReadKey();
        }
    }
}

// 4. Verify the output

// John is male? true
// Julie is a married women? true