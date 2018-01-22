using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MentoringProgram;

namespace MentoringProgram1
{
    class Program
    {
        static void Main(string[] args)
        {
            Func<UsingFile, string, bool> Filter = Ex;

            Console.WriteLine("Введите путь:");
            var directoryPath = Console.ReadLine();
            FileSystemVisitor fileSystemVisitor = new FileSystemVisitor(Filter, directoryPath);
            PrepareForSearching(fileSystemVisitor);

            //Фильтрация по методу расширения
            Console.WriteLine("Введите расширение файла:");
            var extension = Console.ReadLine();
            fileSystemVisitor.StartWork("ex", extension, fileSystemVisitor.Path);

            //Фильтрация по началу имени файла/папки
            Console.WriteLine("Введите символ(ы) с которого(ых) должно начинаться имя файла/папки:");
            var begging = Console.ReadLine();
            fileSystemVisitor.StartWork("beggins", begging, fileSystemVisitor.Path);

            //Фильтрация по части имени файла
            Console.WriteLine("Введите символ(ы) который(ые) присутствуют в имени файла/папки:");
            var contains  = Console.ReadLine();
            fileSystemVisitor.StartWork("contains", contains, fileSystemVisitor.Path);

            Console.ReadKey();
        }

        public static void PrepareForSearching(FileSystemVisitor visitor)
        {
            visitor.StartProcess += Searching_Start;
            visitor.FinishProcess += Searching_End;
            visitor.FileFinded += File_Finded;
            visitor.FilteredFileFinded += FilteredFile_Finded;
            visitor.FolderFinded += Directory_Finded;
            visitor.FilteredFolderFinded += FilteredDirectory_Finded;
        }

        private static void Searching_Start(object sender, FileSystemVisitor.SearchingProcessArgs args)
        {
            Console.WriteLine("Начало работы поиска");
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

            if (answer.ToLower() == "y")
                args.StopFlag = true;
        }

        private static void File_Finded(object sender, FileSystemVisitor.SearchingProcessArgs args)
        {
            Console.WriteLine("Файл найден:");
            Console.WriteLine("Имя файла {0}", args.FileName);
            Console.WriteLine("Исключить файл из конечного списка? y - да, n - нет");
            var answer = Console.ReadLine();

            // что если я введу что либо не удослетворющее обоим условиям
            if (answer.ToLower() == "y")
                args.SkipFlag = true;
            else if (answer.ToLower() == "n")
                args.SkipFlag = false;
        }

        private static void Directory_Finded(object sender, FileSystemVisitor.SearchingProcessArgs args)
        {
            Console.WriteLine("Папка найдена:");
            Console.WriteLine("Имя папки {0}", args.FileName);
            Console.WriteLine("Исключить папку из конечного списка? y - да, n - нет");
            var answer = Console.ReadLine();

            // одно и то же 1000 раз, рефакторить
            if (answer.ToLower() == "y")
                args.SkipFlag = true;
            else if (answer.ToLower() == "n")
                args.SkipFlag = false;
        }

        protected static void FilteredDirectory_Finded(object sender, FileSystemVisitor.SearchingProcessArgs args)
        {
            Console.WriteLine("Папка соответствует поиску!");
            Console.WriteLine("Имя папки {0} Дата последнего изменения папки {1}", args.FileName, args.LastModificationDate);
            Console.WriteLine("Прервать поиск? y - да, n - нет");
            var answer = Console.ReadLine();

            if (answer.ToLower() == "y")
                args.StopFlag = true;
            else if (answer.ToLower() == "n")
                args.StopFlag = false;
        }

        public static bool Ex(UsingFile file, string str)
        {
            return false;
        }
    }
}
