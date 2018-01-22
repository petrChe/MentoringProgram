using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentoringProgram
{
    public class FileEnumerator
    {
        // рефакторить
        //Большой метод, рефакторить
        public static IEnumerable<UsingFile> GetAllFilesByPath(string folderName)
        {
            List<UsingFile> files = new List<UsingFile>();

            try
            {
                if (Directory.Exists(folderName))
                {
                    DirectoryInfo directory = new DirectoryInfo(folderName);

                    foreach (DirectoryInfo dirInfo in directory.GetDirectories())
                    {
                        string folderPath = string.Format(@"{0}\{1}", Path.GetDirectoryName(dirInfo.FullName), dirInfo.Name);
                        DateTime lastModifDate = Directory.GetLastWriteTime(dirInfo.FullName);
                        UsingFile folder = new UsingFile
                        {
                            FileName = dirInfo.FullName,
                            ShortName = Path.GetFileName(dirInfo.FullName),
                            LastModificationDateString = lastModifDate.ToString("dd.MM.yyyy"),
                            IsFolder = true
                        };
                        files.Add(folder); // миллион мест добавления файла, что если ты решишь их помещать в новую коллекцию...

                        if (Directory.EnumerateFileSystemEntries(folderPath).Any())
                        {
                            files.AddRange(GetAllFilesByPath(folderPath));
                        }
                    }

                    foreach (FileInfo fileInfo in directory.GetFiles())
                    {
                        string fileExtension = Path.GetExtension(fileInfo.Name);
                        string filePath = string.Format(@"{0}\{1}", fileInfo.DirectoryName, fileInfo.Name);
                        DateTime lastModifDate = File.GetLastWriteTime(filePath);
                        UsingFile file = new UsingFile
                        {
                            FileName = fileInfo.Name,
                            ShortName = Path.GetFileName(fileInfo.Name),
                            LastModificationDateString = lastModifDate.ToString("dd.MM.yyyy"), //что если формат захочу еще где то юзать
                            Extension = fileExtension,
                            IsFolder = false
                        };
                        files.Add(file);
                    }
                }
                else
                    throw new ArgumentException("Вы ввели неправильный путь"); // убрать все выводы подобного типа, используй словари
            }
            catch(ArgumentException ex)
            {
                Console.WriteLine(ex.Message);
            }

            foreach (var file in files)
                yield return file;
        
        }
    }
}
