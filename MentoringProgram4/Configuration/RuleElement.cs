using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentoringProgram4.Configuration
{
    public class RuleElement : ConfigurationElement
    {
        [ConfigurationProperty("regex")]
        public string Regex
        {
            get { return (string)base["regex"]; }
        }

        [ConfigurationProperty("destFolderPath", IsKey=true)]
        public string DestanationFolderPath
        {
            get { return (string)base["destFolderPath"]; }            
        }

        [ConfigurationProperty("addCounter")]
        public bool AddCounter
        {
            get { return (bool)base["addCounter"]; }
        }

        [ConfigurationProperty("addMovingDate")]
        public bool AddMovingDate
        {
            get { return (bool)base["addMovingDate"]; }
        }

        [ConfigurationProperty("ruleType")]
        public MentoringProgram4.HelperEnums.RuleTypes RuleType
        {
            get { return (MentoringProgram4.HelperEnums.RuleTypes)base["ruleType"]; }
        }
    }
}