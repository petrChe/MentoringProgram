using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentoringProgram6_storing_system.Entities
{
    public class Newspaper : PaperInformationStorage
    {
        public int Number { get; set; }
        public DateTime Date { get; set; }
        public string ISSN { get; set; }
    }
}
