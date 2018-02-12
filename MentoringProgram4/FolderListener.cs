using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading;
using System.Threading.Tasks;
using MentoringProgram4.Configuration;
using responses = MentoringProgram4.Resources.ResponsesToUser;

namespace MentoringProgram4
{
    public class FolderListener : IListener
    {
        private IRulesChecking ruleChecker;
        private FileSystemWatcher folderWatcher;
        private IList<Rule> filterRules;
        private string folderPath;
        private int count = 0;
        private CustomConfigurationSection section = (CustomConfigurationSection)ConfigurationManager.GetSection("customSection");

        public FolderListener(IRulesChecking ruleChecker)
        {
            this.ruleChecker = ruleChecker;
            folderWatcher = new FileSystemWatcher();
            CreateDirectories();
            FillFilterRules();
        }
    
        public void WatchAndListenFolder(string folderDirectory)
        {
            folderPath = folderDirectory;
            var fileName = Path.GetFullPath(folderDirectory);

            folderWatcher.Path = fileName;
            folderWatcher.NotifyFilter = NotifyFilters.FileName;
            folderWatcher.Filter = "*.*";
            folderWatcher.Created += OnCreated;
            folderWatcher.IncludeSubdirectories = false;
            folderWatcher.EnableRaisingEvents = true;

            Thread.Sleep(40 * 1000);
        }

        public void WatchAndListenFolder(IList<string> foldersDirectories)
        {
            foreach (var folder in foldersDirectories)
            {
                WatchAndListenFolder(folder);            
            }
        }

        private void FillFilterRules()
        {
            filterRules = new List<Rule>();
            
            foreach(RuleElement element in section.Rules)
            {
                var rule = new Rule
                {
                    Regex = @element.Regex ?? string.Empty,
                    RuleType = element.RuleType,
                    DestPath = @element.DestanationFolderPath,
                    AddCounter = element.AddCounter,
                    AddMovingDate = element.AddMovingDate
                };

                filterRules.Add(rule);                
            }
        }

        private void CreateDirectories()
        {
            var directoriesNames = new List<string> { @"D:\Text_files", @"D:\Begins_with_number", @"D:\Contains_Number", @"D:\Default_folder" };

            var enumerator = directoriesNames.GetEnumerator();

            while(enumerator.MoveNext())
            {
                var fullPath = Path.GetFullPath(enumerator.Current);

                if (!Directory.Exists(fullPath))
                    Directory.CreateDirectory(fullPath);
            }
        }

        private void OnCreated(object sender, FileSystemEventArgs e)
        {
            var culture = section.CultureInfo;
            var stringcurrentDate = string.Empty;

            if (!string.IsNullOrEmpty(culture))
            {
                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(culture);
                stringcurrentDate = DateTime.Now.ToString("D", Thread.CurrentThread.CurrentUICulture.DateTimeFormat);
            }

            Console.WriteLine(string.Format("{0} - {1} {2} - {3}", responses.File, e.Name, responses.CreationDate,
                stringcurrentDate));
            count++;

            foreach (var rule in filterRules)
            {
                var ruleChecked = ruleChecker.IsRuleMet(rule.RuleType, e.FullPath, rule.Regex);
                
                var fileName = Path.Combine(folderPath, e.Name);
                var finalFile = Path.Combine(rule.DestPath, e.Name);

                if (ruleChecked)
                {
                    Console.WriteLine(responses.FileMatch);
                    if (File.Exists(finalFile))
                        File.Delete(finalFile);


                    File.Copy(fileName, finalFile, true);
                    File.Delete(fileName);

                    AddEndingsToName(e, rule, finalFile);

                    Console.WriteLine(responses.FileHasMoved);
                    return;
                }
            }

            Console.WriteLine(responses.FileDoesntMatch);
        }

        private void AddEndingsToName(FileSystemEventArgs e, Rule rule, string finalFile)
        {
            if (rule.AddCounter)
            {
                var specialName = e.Name.Substring(0, e.Name.Length - 4) + count;
                var extension = e.Name.Substring(e.Name.Length - 4);

                var path = Path.Combine(rule.DestPath, specialName + extension);

                if (File.Exists(path))
                    File.Delete(path);

                File.Move(finalFile, path);
            }

            if (rule.AddMovingDate)
            {
                var specialName = e.Name.Substring(0, e.Name.Length - 4) + DateTime.Now.ToString("D", Thread.CurrentThread.CurrentUICulture.DateTimeFormat);
                var extension = e.Name.Substring(e.Name.Length - 4);
                var path = Path.Combine(rule.DestPath, specialName + extension);

                if (File.Exists(path))
                    File.Delete(path);

                Thread.CurrentThread.CurrentUICulture = new System.Globalization.CultureInfo(section.CultureInfo);
                File.Move(finalFile, path);
            }
        }
    }
}
