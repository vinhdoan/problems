using System;

namespace ChainPattern
{
    // 1. Create an abstract logger class
    public abstract class AbstractLogger
    {
        public static int INFO = 1;
        public static int DEBUG = 2;
        public static int ERROR = 3;

        protected int level;

        //next element in chain or responsibility
        protected AbstractLogger nextLogger;

        public void setNextLogger(AbstractLogger nextLogger)
        {
            this.nextLogger = nextLogger;
        }

        public void logMessage(int level, String message)
        {
            if(this.level <= level)
            {
                write(message);
            }
            if(nextLogger != null)
            {
                nextLogger.logMessage(level, message);
            }
        }

        abstract protected void write(String message);
        
    }

    // 2a. Create concrete classes extending the logger
    public class ConsoleLogger : AbstractLogger
    {
        public ConsoleLogger(int level)
        {
            this.level = level;
        }

        protected override void write(String message)
        {
            Console.WriteLine("Standard Console::Logger: " + message);
        }
    }

    // 2b. Create concrete classes extending the logger
    public class ErrorLogger : AbstractLogger
    {
        public ErrorLogger(int level)
        {
            this.level = level;
        }

        protected override void write(String message)
        {
            Console.WriteLine("Error Console::Logger: " + message);
        }
    }
    
    // 2c. Create concrete classes extending the logger
    public class FileLogger : AbstractLogger
    {
        public FileLogger(int level){
            this.level = level;
        }

        protected override void write(String message)
        {
            Console.WriteLine("File::Logger: " + message);
        }
    }

    // 3. Create different types of loggers
    //    Assign them error levels and set next logger in each logger
    //    Next logger in each logger represents the part of the chain
    public class ChainPatternDemo
    {
        private static AbstractLogger getChainOfLoggers()
        {
            AbstractLogger errorLogger = new ErrorLogger(AbstractLogger.ERROR);
            AbstractLogger fileLogger = new FileLogger(AbstractLogger.DEBUG);
            AbstractLogger consoleLogger = new ConsoleLogger(AbstractLogger.INFO);

            errorLogger.setNextLogger(fileLogger);
            fileLogger.setNextLogger(consoleLogger);

            return errorLogger;	
        }

        public static void Main(String[] args) {
            AbstractLogger loggerChain = getChainOfLoggers();

            loggerChain.logMessage(AbstractLogger.INFO, "This is an information.");

            loggerChain.logMessage(AbstractLogger.DEBUG, "This is an debug level information.");

            loggerChain.logMessage(AbstractLogger.ERROR, "This is an error information.");
            
            Console.ReadKey();
        }
    }
}

// 4. Verify the output

// Standard Console::Logger: This is an information.
// File::Logger: This is an debug level information.
// Standard Console::Logger: This is an debug level information.
// Error Console::Logger: This is an error information.
// File::Logger: This is an error information.
// Standard Console::Logger: This is an error information.