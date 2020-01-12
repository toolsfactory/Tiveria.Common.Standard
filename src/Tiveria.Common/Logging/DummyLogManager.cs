using System;

namespace Tiveria.Common.Logging
{
    public class DummyLogManager : ILogManager
    {
        public ILogger GetLogger(string name)
        {
            return new DummyLogger();
        }

        public ILogger GetLogger(Type type)
        {
            return new DummyLogger();
        }
    }
}
