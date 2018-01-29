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

        //Словарь ошибок
        private Dictionary<string, string> Errors;

        public FileSystemVisitor(IFilesEnumerator filesEnumerator, Func<UsingFile, string, bool> filter, string path)
        {
            this.filesEnumerator = filesEnumerator;
            this.filter = filter;
            this.path = path;
            Errors = new Dictionary<string, string>();
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
        public void StartWork(string type, string parametr, string directoryPath) 
        {
            OnStart(new SearchingProcessArgs());

            // зачем свич если все 3 блока одинаковые....
            switch (type)
            { 
                case "ex":
                    FindedFiles = AllFindedFilesFromDirectory(directoryPath);
                    AllFilteredFilesFromDirectory(Filter, FindedFiles, parametr);  // зачем передавать фильтр если это глобальная переменная
                    break;
                case "beggins":
                    FindedFiles = AllFindedFilesFromDirectory(directoryPath);
                    AllFilteredFilesFromDirectory(Filter, FindedFiles, parametr); 
                    break;
                case "contains":
                    FindedFiles = AllFindedFilesFromDirectory(directoryPath);
                    AllFilteredFilesFromDirectory(Filter, FindedFiles, parametr); 
                    break;
                default:
                    break;
            }

            ShowErrors();

            OnFinish(new SearchingProcessArgs());
        }

        public void ShowErrors()
        {
            if (Errors.Count > 0)
            {
                // Ходят легенды что это быстрее он сам умеет многопоточность Errors.ToList().ForEach
                foreach (var error in Errors)
                    Console.WriteLine(error.Value);
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

            try
            {
                var files = filesEnumerator.GetAllFilesByPath(folderName);

                if (files.Count() == 0)// Валидация не дожна заказчиваться выкидыванием исключения - переделать
                    throw new IndexOutOfRangeException("Исключение");

                foreach (var file in files)
                {
                    SearchingProcessArgs args = new SearchingProcessArgs { FileName = file.FileName, LastModificationDate = file.LastModificationDateString }; // зачем миллион раз создавать переменную, внутри цикла только операции, переделать

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
            }
            catch (ArgumentException ex)
            {
                Errors.Add(ex.TargetSite.ToString(), ex.Message);
            }
            catch (Exception ex)
            {
                Errors.Add(ex.TargetSite.ToString(), HelperConsts.CommonError);     
            }

            return findedFiles;
        }

        public void AllFilteredFilesFromDirectory(Func<UsingFile, string, bool> Filter, IEnumerable<UsingFile> files, string filterDetail)
        {
            try
            {
                foreach (var file in files)
                {
                    //проверка
                    if (!validator.IsValid(filterDetail)) // Валидация не дожна заказчиваться выкидыванием исключения - переделать
                        throw new ArgumentException("Вы ввели неправильное расширение файла");

                    SearchingProcessArgs args = new SearchingProcessArgs { FileName = file.FileName, LastModificationDate = file.LastModificationDateString };

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
            catch (ArgumentException ex)
            {
                //Запись ошибки в журнал ошибок
                Errors.Add(ex.TargetSite.ToString(), ex.Message);
            }
            catch (Exception ex)
            {
                //Запись ошибки в журнал ошибок
                Errors.Add(ex.TargetSite.ToString(), HelperConsts.CommonError);
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
