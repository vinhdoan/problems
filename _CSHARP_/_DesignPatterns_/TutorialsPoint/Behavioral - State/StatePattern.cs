using System;

namespace StatePattern
{
    // 1. Create an interface
    public interface IState
    {
        void doAction(Context context);
    }

    // 2a. Create concrete classes implementing the same interface
    public class StartState : IState
    {
        public void doAction(Context context)
        {
            Console.WriteLine("Player is in start state");
            context.setState(this);	
        }

        public override String ToString()
        {
            return "Start State";
        }
    }

    // 2b. Create concrete classes implementing the same interface
    public class StopState : IState
    {
        public void doAction(Context context)
        {
            Console.WriteLine("Player is in stop state");
            context.setState(this);	
        }

        public override String ToString()
        {
            return "Stop State";
        }
    }

    // 3. Create Context Class
    public class Context
    {
        private IState state;

        public Context()
        {
            state = null;
        }

        public void setState(IState state)
        {
            this.state = state;		
        }

        public IState getState()
        {
            return state;
        }
    }

    // 4. Use the Context to see change in behaviour when State changes
    public class StatePatternDemo
    {
        public static void Main(String[] args)
        {
            Context context = new Context();

            StartState startState = new StartState();
            startState.doAction(context);

            Console.WriteLine(context.getState().ToString());

            StopState stopState = new StopState();
            stopState.doAction(context);

            Console.WriteLine(context.getState().ToString());

            Console.ReadKey();
        }
    }
}

// 5. Verify the output

// Player is in start state
// Start State
// Player is in stop state
// Stop State