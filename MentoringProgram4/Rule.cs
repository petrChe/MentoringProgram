using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentoringProgram4
{
    public class Rule
    {
        public string Regex { get; set; }
        public MentoringProgram4.HelperEnums.RuleTypes RuleType { get; set; }
        public string DestPath { get; set; }
        public bool AddCounter { get; set; }
        public bool AddMovingDate { get; set; }
    }
}
