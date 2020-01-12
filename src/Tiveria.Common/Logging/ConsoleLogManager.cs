using System;

namespace Tiveria.Common.Logging
{
    public class ConsoleLogManager : ILogManager
    {
        public ILogger GetLogger(string name)
        {
            return new ConsoleLogger(name);
        }

        public ILogger GetLogger(Type type)
        {
            return new ConsoleLogger(type.Name);
        }
    }
}
