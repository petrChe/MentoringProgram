using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MentoringProgram.Helpers;
using MentoringProgram.Interfaces;

namespace MentoringProgram
{
    public class FileSystemVisitor
    {
        private readonly IFilesEnumerator filesEnumerator;
        private readonly IValidation validator = new HelperValidation();

        private Func<UsingFile, string, bool> filter;
        public Func<UsingFile, string, bool> Filter { get { return filter; } set { this.filter = value; } }
        
        private string path;
        public string Path { get { return path; } set { this.path = value; } }

        public IEnumerable<UsingFile> FindedFiles{ get; private set;}

        //Коллекция ошибок
        private List<string> errors;

        public FileSystemVisitor(IFilesEnumerator filesEnumerator, Func<UsingFile, string, bool> filter, string path)
        {
            this.filesEnumerator = filesEnumerator;
            this.filter = filter;
            this.path = path;
            errors = new List<string>();
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
            RunEvent(tmp, args);
        }

        protected virtual void OnFinish(SearchingProcessArgs args)
        {
            var tmp = FinishProcess;
            RunEvent(tmp, args);
        }

        protected virtual void OnFileFinded(SearchingProcessArgs args)
        {
            var tmp = FileFinded;
            RunEvent(tmp, args);
        }

        protected virtual void OnFilteredFileFinded(SearchingProcessArgs args)
        {
            var tmp = FilteredFileFinded;
            RunEvent(tmp, args);
        }

        protected virtual void OnFolderFinded(SearchingProcessArgs args)
        {
            var tmp = FolderFinded;
            RunEvent(tmp, args);
        }

        protected virtual void OnFilteredFolderFinded(SearchingProcessArgs args)
        {
            var tmp = FilteredFolderFinded;
            RunEvent(tmp, args);
        }

        private void RunEvent(EventHandler<SearchingProcessArgs> tmp, SearchingProcessArgs args)
        {
            if (tmp != null)
                tmp(this, args);
        }

        #endregion

        /// <summary>
        /// Метод начало работы
        /// </summary>
        /// <param name="type"></param>
        /// <param name="parametr"></param>
        /// <param name="directoryPath"></param>
        public void StartWork(string parametr, string directoryPath)
        {
            OnStart(new SearchingProcessArgs());

            // зачем свич если все 3 блока одинаковые....
            FindedFiles = AllFindedFilesFromDirectory(directoryPath);
            AllFilteredFilesFromDirectory(FindedFiles, parametr);

            ShowErrors();

            OnFinish(new SearchingProcessArgs());
        }

        public void ShowErrors()
        {
            if (errors.Count > 0)
            {
                foreach (var error in errors)
                    Console.WriteLine(error);
            }
        }

        /// <summary>
        /// С помощью класса enumerator(а) получаем объекты(файлы и папки) и взаимодействуем с ними
        /// </summary>
        /// <param name="folderName"></param>
        /// <returns></returns>
        public IEnumerable<UsingFile> AllFindedFilesFromDirectory(string folderName)
        {
            var findedFiles = new List<UsingFile>();

            var files = filesEnumerator.GetAllFilesByPath(folderName);
            
            if (files.Count() == 0)
                errors.Add("Найдено 0 файлов/папок");

            foreach (var file in files)
            {
                SearchingProcessArgs args = new SearchingProcessArgs { FileName = file.FileName, LastModificationDate = file.LastModificationDateString };

                if (file.IsFolder)
                {
                    OnFolderFinded(args);
                    
                    if (args.SkipFlag)
                        continue;
                    else
                        findedFiles.Add(file);
                }
                else
                {
                    OnFileFinded(args);

                    if (args.SkipFlag)
                        continue;
                    else
                        findedFiles.Add(file);
                }
            }

            return findedFiles;
        }

        public void AllFilteredFilesFromDirectory(IEnumerable<UsingFile> files, string filterDetail)
        {
            SearchingProcessArgs args = new SearchingProcessArgs();

            foreach (var file in files)
            {
                //проверка
                if (!validator.IsValid(filterDetail))
                    errors.Add("Вы ввели неправильное расширение файла");

                args = new SearchingProcessArgs { FileName = file.FileName, LastModificationDate = file.LastModificationDateString };

                if (Filter(file, filterDetail))
                {
                    if (file.IsFolder)
                    {
                        OnFilteredFolderFinded(args);

                        if (args.StopFlag)
                            break;
                    }
                    else
                    {
                        OnFilteredFileFinded(args);

                        if (args.StopFlag)
                            break;
                    }
                }
             }

        }

        /// <summary>
        /// Класс аргументов для использования с событиями
        /// </summary>
        public class SearchingProcessArgs
        {
            public string FileName { get; set; }

            public string LastModificationDate { get; set; }

            public bool StopFlag { get; set; }

            public bool SkipFlag { get; set; }          
        }
    }
}
