using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MatchingGame2.lib.infra.Logging
{
    public class ConsoleLogger : Logger
    {
        override public void Log(string line)
        {
            Console.WriteLine(line);
        }
    }
}
