using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MentoringProgram5.Attributes;

namespace MentoringProgram5
{
    [Export]
    public class Logger
    {
        public Logger() { }

        public void LogToConsole()
        {
            Console.WriteLine("Log to console");
        }
    }
}
