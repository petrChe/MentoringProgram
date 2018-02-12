using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentoringProgram4.Configuration
{
    public class CustomConfigurationSection : ConfigurationSection
    {
        [ConfigurationProperty("directories")]
        public DirectoryElementCollection Directories
        {
            get { return (DirectoryElementCollection)this["directories"]; }
        }

        [ConfigurationProperty("rules")]
        public RulesElementsCollection Rules
        {
            get { return (RulesElementsCollection)this["rules"]; }
        }

        [ConfigurationProperty("culture")]
        public string CultureInfo
        {
            get { return (string)this["culture"]; }
        }
    }
}