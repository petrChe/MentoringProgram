using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentoringProgram
{
    public class FileSystemVisitor : IFileSearching
    {
        private Func<UsingFile, string, bool> Filter;
        
        private string path;

        public string Path { get { return path; } set { this.path = value; } }

        public FileSystemVisitor(Func<UsingFile, string, bool> Filter, string path)
        {
            this.Filter = Filter;
            this.path = path;
        }

        #region events
        public event EventHandler<SearchingProcessArgs> StartProcess;
        public event EventHandler<SearchingProcessArgs> FinishProcess;
        public event EventHandler<SearchingProcessArgs> FileFinded;
        public event EventHandler<SearchingProcessArgs> FilteredFileFinded;
        public event EventHandler<SearchingProcessArgs> FolderFinded;
        public event EventHandler<SearchingProcessArgs> FilteredFolderFinded;


        protected virtual void OnStart(SearchingProcessArgs args)
        {
            var tmp = StartProcess;
            if (tmp != null)
                tmp(this, args);
        }

        protected virtual void OnFinish(SearchingProcessArgs args)
        {
            var tmp = FinishProcess;
            // повторяющийся код
            if (tmp != null)
                tmp(this, args);
        }

        protected virtual void OnFileFinded(SearchingProcessArgs args)
        {
            var tmp = FileFinded;
            if (tmp != null)
                tmp(this, args);
        }

        protected virtual void OnFilteredFileFinded(SearchingProcessArgs args)
        {
            var tmp = FilteredFileFinded;
            if (tmp != null)
                tmp(this, args);
        }

        protected virtual void OnFolderFinded(SearchingProcessArgs args)
        {
            var tmp = FolderFinded;
            if (tmp != null)
                tmp(this, args);
        }

        protected virtual void OnFilteredFolderFinded(SearchingProcessArgs args)
        {
            var tmp = FilteredFolderFinded;
            if (tmp != null)
                tmp(this, args);
        }

        #endregion

        public void StartWork(string type, string parametr, string directoryPath)
        {
            OnStart(new SearchingProcessArgs());
            
            switch (type)
            { 
                case "ex":
                    AllFilteredFilesFromDirectory(FileSystemVisitor.Filtrator.FilterByFileType, parametr, directoryPath);
                    break;
                case "beggins":
                    AllFilteredFilesFromDirectory(FileSystemVisitor.Filtrator.FilterByBeginsWith, parametr, directoryPath);
                    break;
                case "contains":
                    AllFilteredFilesFromDirectory(FileSystemVisitor.Filtrator.FilterByContains, parametr, directoryPath);
                    break;
                default:
                    break;
            }

            OnFinish(new SearchingProcessArgs());
        }

        public void AllFilteredFilesFromDirectory(Func<UsingFile, string, bool> Filter, string filterDetail, string folderName)
        {
            var files = FileEnumerator.GetAllFilesByPath(folderName);
            foreach (var file in files)
            {
                if (file.IsFolder)
                {
                    SearchingProcessArgs args = new SearchingProcessArgs { FileName = file.FileName, LastModificationDate = file.LastModificationDateString };
                    OnFolderFinded(args);

                    if (args.SkipFlag)
                        continue;
                }
                else
                {
                    SearchingProcessArgs args = new SearchingProcessArgs { FileName = file.FileName, LastModificationDate = file.LastModificationDateString };
                    OnFileFinded(args);

                    if (args.SkipFlag)
                        continue;
                }

                if (Filter(file, filterDetail))
                {
                    if (file.IsFolder)
                    {
                        SearchingProcessArgs args = new SearchingProcessArgs { FileName = file.FileName, LastModificationDate = file.LastModificationDateString };
                        OnFilteredFolderFinded(args);
                        
                        //я это уже видел....
                        if (args.StopFlag)
                            break;
                    }
                    else
                    {
                        SearchingProcessArgs args = new SearchingProcessArgs { FileName = file.FileName, LastModificationDate = file.LastModificationDateString };
                        OnFilteredFileFinded(args);
                        
                        if (args.StopFlag)
                            break;
                    }
                }     
            }
        }

        public class SearchingProcessArgs
        {
            public string FileName { get; set; }

            public string LastModificationDate { get; set; }

            public bool StopFlag { get; set; }

            public bool SkipFlag { get; set; }
        }

        public class Filtrator
        {
            public static bool FilterByFileType(UsingFile file, string extension)
            {
                return file.Extension == extension;                 
            }

            public static bool FilterByBeginsWith(UsingFile file, string beggining)
            {
                var lettersCount = beggining.Length;

                return file.ShortName.Substring(0, lettersCount).ToLower() == beggining.ToLower();
            }

            public static bool FilterByContains(UsingFile file, string partOfName)
            {
                return file.FileName.Contains(partOfName);            
            }
        }
    }
}
