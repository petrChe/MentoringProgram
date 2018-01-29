using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MentoringProgram;
using MentoringProgram.Helpers;
using MentoringProgram.Interfaces;

namespace MentoringProgram1
{
    class Program
    {
        // в целом со сценарием проблема
        static void Main(string[] args)
        {
            //Настройка кодировки
            Console.OutputEncoding = Encoding.UTF8;

            Func<UsingFile, string, bool> filter = (UsingFile file, string fileExtension) => file.Extension == fileExtension;
            IFilesEnumerator filesEnumerator = new FileEnumerator();

            Console.WriteLine("Введите путь:");
            var directoryPath = Console.ReadLine();
            FileSystemVisitor fileSystemVisitor = new FileSystemVisitor(filesEnumerator, filter, directoryPath);
            PrepareForSearching(fileSystemVisitor);

            // Где шаблоны......
            //Фильтрация по методу расширения
            Console.WriteLine("Введите расширение файла:"); // не хочу указывать хочу найти сразу и все без лишних вопросов 
            var extension = Console.ReadLine();
            
            fileSystemVisitor.StartWork("ex", extension, fileSystemVisitor.Path); //что за магические символы

            //Фильтрация по началу имени файла/папки          
            fileSystemVisitor.Filter = (UsingFile file, string beggining) => 
            {
                var lettersCount = beggining.Length;
                return file.ShortName.Substring(0, lettersCount).ToLower() == beggining.ToLower();
            };

            Console.WriteLine("Введите символ(ы) с которого(ых) должно начинаться имя файла/папки:"); // не хочу я ничего фильтровать
            var begging = Console.ReadLine();

            fileSystemVisitor.StartWork("beggins", begging, fileSystemVisitor.Path);

            //Фильтрация по части имени файла
            fileSystemVisitor.Filter = (UsingFile file, string partOfName) => file.FileName.Contains(partOfName);

            Console.WriteLine("Введите символ(ы) который(ые) присутствуют в имени файла/папки:"); // аналогично
            var contains = Console.ReadLine();
            
            fileSystemVisitor.StartWork("contains", contains, fileSystemVisitor.Path);

            Console.ReadKey();
        }

        /// <summary>
        /// Подписка на события
        /// </summary>
        /// <param name="visitor"></param>
        public static void PrepareForSearching(FileSystemVisitor visitor)
        {
            visitor.StartProcess += Searching_Start;
            visitor.FinishProcess += Searching_End;
            visitor.FileFinded += File_Finded;
            visitor.FilteredFileFinded += FilteredFile_Finded;
            visitor.FolderFinded += Directory_Finded;
            visitor.FilteredFolderFinded += FilteredDirectory_Finded;
        }

        #region Обработчики событий
        private static void Searching_Start(object sender, FileSystemVisitor.SearchingProcessArgs args)
        {
            Console.WriteLine("Начало работы поиска"); // TODO TODO TODO я писал убрать все эти магические буквы, используй файл расширения resx
        }

        private static void Searching_End(object sender, FileSystemVisitor.SearchingProcessArgs args)
        {
            Console.WriteLine("Конец работы поиска");
        }

        private static void FilteredFile_Finded(object sender, FileSystemVisitor.SearchingProcessArgs args)
        {
            Console.WriteLine("Файл соответствует поиску!");
            Console.WriteLine("Имя файла {0} Дата последнего изменения файла {1}", args.FileName, args.LastModificationDate);
            Console.WriteLine("Прервать поиск? y - да, n - нет");
            var answer = Console.ReadLine();

            EnableFlags(HelperEnums.FlagTypes.StopFlag, answer, args);
        }

        private static void File_Finded(object sender, FileSystemVisitor.SearchingProcessArgs args)
        {
            Console.WriteLine("Файл найден:");
            Console.WriteLine("Имя файла {0}", args.FileName);
            Console.WriteLine("Исключить файл из конечного списка? y - да, n - нет");
            var answer = Console.ReadLine();

            EnableFlags(HelperEnums.FlagTypes.SkipFlag, answer, args);
        }

        private static void Directory_Finded(object sender, FileSystemVisitor.SearchingProcessArgs args)
        {
            Console.WriteLine("Папка найдена:");
            Console.WriteLine("Имя папки {0}", args.FileName);
            Console.WriteLine("Исключить папку из конечного списка? y - да, n - нет");
            var answer = Console.ReadLine();

            EnableFlags(HelperEnums.FlagTypes.SkipFlag, answer, args);
        }

        protected static void FilteredDirectory_Finded(object sender, FileSystemVisitor.SearchingProcessArgs args)
        {
            Console.WriteLine("Папка соответствует поиску!");
            Console.WriteLine("Имя папки {0} Дата последнего изменения папки {1}", args.FileName, args.LastModificationDate);
            Console.WriteLine("Прервать поиск? y - да, n - нет");
            var answer = Console.ReadLine();

            EnableFlags(HelperEnums.FlagTypes.StopFlag, answer, args);
        }
        #endregion

        /// <summary>
        /// Диалог с пользователем. Задание значений для флагов в аргументах
        /// </summary>
        /// <param name="type"></param>
        /// <param name="answer"></param>
        /// <param name="args"></param>
        public static void EnableFlags(HelperEnums.FlagTypes type, string answer, FileSystemVisitor.SearchingProcessArgs args)
        {
            if(answer.ToLower() != "y" && answer.ToLower() != "n")// опять магические символы, завтра ты захочишь писатьна русском и полностью да нет, что делать будешь
            {
                Console.WriteLine("Ошибка. Вы ввели неправильную информацию. Попробуйте снова");
                EnableFlags(type, Console.ReadLine(), args);
            }

            switch(type)
            {
                case HelperEnums.FlagTypes.SkipFlag:
                    if (answer.ToLower() == "y")
                        args.SkipFlag = true;
                    else
                        args.SkipFlag = false;
                    break;
                case HelperEnums.FlagTypes.StopFlag:
                    if (answer.ToLower() == "y")
                        args.StopFlag = true;
                    else
                        args.StopFlag = false;
                    break;
                default:
                    break;
            }
        }
    }
}