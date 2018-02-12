using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentoringProgram4
{
    public interface IRulesChecking
    {
        bool IsRuleMet(MentoringProgram4.HelperEnums.RuleTypes ruleType, string fileName, string regex);
    }
}
