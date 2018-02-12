using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;

namespace MentoringProgram4
{
    public class RuleChecker : IRulesChecking
    {
        private const string textFileExtension = ".txt";

        public bool IsRuleMet(HelperEnums.RuleTypes ruleType, string fileName, string regex)
        {
            var result = false;

            switch (ruleType)
            {
                case HelperEnums.RuleTypes.TxtFile:
                    result = IsTextFile(fileName);
                    break;
                case HelperEnums.RuleTypes.NameBeginsWithNumber:
                    result = CheckNameFirstSymbol(fileName, regex);
                    break;
                case HelperEnums.RuleTypes.NameContainsNumber:
                    result = CheckNameForSymbols(fileName, regex);
                    break;
                case HelperEnums.RuleTypes.WithoutRule:
                    result = true;
                    break;
            };

            return result;
        }

        private bool CheckNameForSymbols(string fileName, string pattern)
        {
            foreach (var c in fileName)
            {
                if (Regex.IsMatch(c.ToString(), pattern))
                    return true;
            }

            return false;
        }

        private bool CheckNameFirstSymbol(string fileName, string pattern)
        {
            var shortName = Path.GetFileName(fileName);

            if (Regex.IsMatch(shortName.Substring(0,1), pattern))
                return true;

            return false;
        }

        private bool IsTextFile(string fileName)
        {
            var extension = Path.GetExtension(fileName);
            //!!!
            if (extension == textFileExtension)
                return true;

            return false;
        }


    }
}
