using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Tiveria.Common.Logging
{
    public class ConsoleLogger : ILogger
    {
        #region static configuration
        public static bool UseErrorOutputStream = true;
        #endregion

        #region public properties
        public bool IsDebugEnabled => true;
        public bool IsInfoEnabled => true;
        public bool IsWarnEnabled => true;
        public bool IsErrorEnabled => true;
        public bool IsFatalEnabled => true;
        #endregion

        public void Debug(object message)
        {
            WriteLine("Debug: " + message, ConsoleColor.Blue);
        }

        public void Debug(object message, Exception exception)
        {
            WriteLine("Debug: " + message, exception, ConsoleColor.Blue);
        }

        public void Info(object message)
        {
            WriteLine("Info: " + message, ConsoleColor.White);
        }

        public void Info(object message, Exception exception)
        {
            WriteLine("Info: " + message, exception, ConsoleColor.White);
        }

        public void Warn(object message)
        {
            WriteLine("Warn: " + message, ConsoleColor.Yellow);
        }

        public void Warn(object message, Exception exception)
        {
            WriteLine("Warn: " + message, exception, ConsoleColor.Yellow);
        }

        public void Error(object message)
        {
            WriteLine("Error: " + message, ConsoleColor.Red);
        }

        public void Error(object message, Exception exception)
        {
            WriteLine("Error: " + message,  exception, ConsoleColor.Red);
        }

        public void Fatal(object message)
        {
            WriteLine("Fatal: " + message, ConsoleColor.Magenta);
        }

        public void Fatal(object message, Exception exception)
        {
            WriteLine("Fatal: " + message, exception, ConsoleColor.Magenta);
        }

        private void WriteLine(string text, ConsoleColor color)
        {
            var previous = Console.ForegroundColor;
            if (UseErrorOutputStream)
                Console.Error.WriteLine(text);
            else
                Console.WriteLine(text);
            Console.ForegroundColor = previous;
        }

        private void WriteLine(string text, Exception ex, ConsoleColor color)
        {
            var previous = Console.ForegroundColor;
            if (UseErrorOutputStream)
            {
                Console.Error.WriteLine(text);
                Console.Error.WriteLine("  Exception: " + ex);
            }
            else
            {
                Console.WriteLine(text);
                Console.WriteLine("  Exception: " + ex);
            }
            Console.ForegroundColor = previous;
        }
    }
}
