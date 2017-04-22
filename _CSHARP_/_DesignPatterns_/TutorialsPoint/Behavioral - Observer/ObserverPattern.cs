using System;
using System.Collections.Generic;   // for List<T>

namespace ObserverPattern
{
    // 1. Create Subject class
    public class Subject
    {
        private List<Observer> observers = new List<Observer>();
        private int state;

        public int getState()
        {
            return state;
        }

        public void setState(int state)
        {
            this.state = state;
            notifyAllObservers();
        }

        public void attach(Observer observer)
        {
            observers.Add(observer);
        }

        public void notifyAllObservers()
        {
            foreach (Observer observer in observers)
            {
                observer.update();
            }
        }
    }

    // 2. Create Observer class
    public abstract class Observer
    {
        protected Subject subject;
        public abstract void update();
    }

    // 3a. Create concrete observer classes
    public class BinaryObserver : Observer
    {
        public BinaryObserver(Subject subject)
        {
            this.subject = subject;
            this.subject.attach(this);
        }

        public override void update()
        {
            Console.WriteLine("Binary String: " + Convert.ToString(subject.getState(), 2)); 
        }
    }

    // 3b. Create concrete observer classes
    public class OctalObserver : Observer
    {
        public OctalObserver(Subject subject)
        {
            this.subject = subject;
            this.subject.attach(this);
        }

        public override void update()
        {
            Console.WriteLine("Octal String: " + Convert.ToString(subject.getState(), 8)); 
        }
    }

    // 3c. Create concrete observer classes
    public class HexaObserver : Observer
    {
        public HexaObserver(Subject subject)
        {
            this.subject = subject;
            this.subject.attach(this);
        }

        public override void update()
        {
            Console.WriteLine( "Hex String: " + Convert.ToString(subject.getState(), 16)); 
        }
    }

    // 4. Use Subject and concrete observer objects
    public class ObserverPatternDemo
    {
        public static void Main(String[] args)
        {
            Subject subject = new Subject();

            new HexaObserver(subject);
            new OctalObserver(subject);
            new BinaryObserver(subject);

            Console.WriteLine("First state change: 15");	
            subject.setState(15);
            Console.WriteLine("Second state change: 10");	
            subject.setState(10);
            
            Console.ReadKey();
        }
    }
}

// 5. Verify the output

// First state change: 15
// Hex String: F
// Octal String: 17
// Binary String: 1111
// Second state change: 10
// Hex String: A
// Octal String: 12
// Binary String: 1010