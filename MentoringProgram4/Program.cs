using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MentoringProgram4.Configuration;

namespace MentoringProgram4
{
    class Program
    {
        static void Main(string[] args)
        {
            while (true)
            {
                var folders = GetFoldersForListening();
                RuleChecker ruleChecker = new RuleChecker();
                FolderListener listener = new FolderListener(ruleChecker);
                listener.WatchAndListenFolder(folders);
            }
        }

        public static List<string> GetFoldersForListening()
        {
            var section = (CustomConfigurationSection)ConfigurationManager.GetSection("customSection");
            var resultList = new List<string>();

            foreach (DirectoryElement dirInfo in section.Directories)
            {
                resultList.Add(@dirInfo.DirectoryName);                            
            }

            return resultList;
        }
    }
}
