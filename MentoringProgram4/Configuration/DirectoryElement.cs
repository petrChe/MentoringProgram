using System;
using System.Collections;
using System.Text;
using System.Configuration;
using System.Xml;
namespace MentoringProgram4.Configuration
{
    public class DirectoryElement : ConfigurationElement
    {
        [ConfigurationProperty("name", IsKey=true)]
        public string DirectoryName
        {
            get { return (string)base["name"]; }
        }
    }
}
