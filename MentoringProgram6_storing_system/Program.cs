using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MentoringProgram6_storing_system.Entities;

namespace MentoringProgram6_storing_system
{
    class Program
    {
        static void Main(string[] args)
        {
            var preparator = new Preparator();

            var obj = preparator.CreateXml<Book>(preparator.Books);
            Console.ReadKey();
        }
    }
}
