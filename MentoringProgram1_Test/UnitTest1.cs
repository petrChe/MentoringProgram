using System;
using System.Collections.Generic;
using MentoringProgram;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace MentoringProgram1_Test
{
    [TestClass]
    public class UnitTest1
    {
        //public Func<UsingFile, string, bool> Filter = Ex;
        //public const string Path = "D:\\файлы";

        [TestMethod]
        public void TestMethod1()
        {
            //FileSystemVisitor fileSystemVisitor = new FileSystemVisitor(Filter, Path);
            //PrepareForSearching(fileSystemVisitor);

            //var filteredByExtensionCount = fileSystemVisitor.AllFilteredFilesFromDirectory(FileSystemVisitor.Filtrator.FilterByFileType, ".txt", fileSystemVisitor.Path, GetAllFilesFromSelectedDirectory());
            //Assert.Equals(filteredByExtensionCount, 4);
        }


        //public IEnumerable<UsingFile> GetAllFilesFromSelectedDirectory()
        //{
        //    return FileEnumerator.GetAllFilesByPath(Path);
        //}

        //public static bool Ex(UsingFile file, string str)
        //{
        //    return false;
        //}

        //public static void PrepareForSearching(FileSystemVisitor visitor)
        //{
        //    visitor.StartProcess += Searching_Start;
        //    visitor.FinishProcess += Searching_End;
        //    visitor.FileFinded += File_Finded;
        //    visitor.FilteredFileFinded += FilteredFile_Finded;
        //    visitor.FolderFinded += Directory_Finded;
        //    visitor.FilteredFolderFinded += FilteredDirectory_Finded;
        //}

        //private static void Searching_Start(object sender, FileSystemVisitor.SearchingProcessArgs args)
        //{
        //    Console.WriteLine("Начало работы поиска");
        //}

        //private static void Searching_End(object sender, FileSystemVisitor.SearchingProcessArgs args)
        //{
        //    Console.WriteLine("Конец работы поиска");
        //}

        //private static void FilteredFile_Finded(object sender, FileSystemVisitor.SearchingProcessArgs args)
        //{
        //    Console.WriteLine("Файл соответствует поиску!");
        //    Console.WriteLine("Имя файла {0} Дата последнего изменения файла {1}", args.FileName, args.LastModificationDate);
        //    Console.WriteLine("Прервать поиск? y - да, n - нет");
        //    var answer = Console.ReadLine();

        //    if (answer.ToLower() == "y")
        //        args.StopFlag = true;
        //}

        //private static void File_Finded(object sender, FileSystemVisitor.SearchingProcessArgs args)
        //{
        //    Console.WriteLine("Файл найден:");
        //    Console.WriteLine("Имя файла {0}", args.FileName);
        //    Console.WriteLine("Исключить файл из конечного списка? y - да, n - нет");
        //    var answer = Console.ReadLine();

        //    if (answer.ToLower() == "y")
        //        args.SkipFlag = true;
        //    else if (answer.ToLower() == "n")
        //        args.SkipFlag = false;
        //}

        //private static void Directory_Finded(object sender, FileSystemVisitor.SearchingProcessArgs args)
        //{
        //    Console.WriteLine("Папка найдена:");
        //    Console.WriteLine("Имя папки {0}", args.FileName);
        //    Console.WriteLine("Исключить папку из конечного списка? y - да, n - нет");
        //    var answer = Console.ReadLine();

        //    if (answer.ToLower() == "y")
        //        args.SkipFlag = true;
        //    else if (answer.ToLower() == "n")
        //        args.SkipFlag = false;
        //}

        //protected static void FilteredDirectory_Finded(object sender, FileSystemVisitor.SearchingProcessArgs args)
        //{
        //    Console.WriteLine("Папка соответствует поиску!");
        //    Console.WriteLine("Имя папки {0} Дата последнего изменения папки {1}", args.FileName, args.LastModificationDate);
        //    Console.WriteLine("Прервать поиск? y - да, n - нет");
        //    var answer = Console.ReadLine();

        //    if (answer.ToLower() == "y")
        //        args.StopFlag = true;
        //    else if (answer.ToLower() == "n")
        //        args.StopFlag = false;
        //}
    }
}
