using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using MentoringProgram.Interfaces;

namespace MentoringProgram.Helpers
{
    public class HelperValidation : IValidation
    {

        public bool IsValid(string parametr)
        {
            var result = false;

            if (parametr.First() != '.')
                parametr = "." + parametr;

            string pattern = @".\w{3}";

            if (!Regex.IsMatch(parametr, pattern))
                return false;

            if (HelperConsts.filesExtensions.Contains(parametr.Substring(1)))
                result = true;

            return result;
        }
    }
}
