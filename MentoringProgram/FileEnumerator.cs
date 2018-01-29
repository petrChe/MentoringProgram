﻿using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MentoringProgram.Helpers;
using MentoringProgram.Interfaces;

namespace MentoringProgram
{
    public class FileEnumerator : IFilesEnumerator
    {
        /// <summary>
        /// Метод поиска файлов(файлы и папки) по передаваемому пути
        /// </summary>
        /// <param name="folderName"></param>
        /// <returns></returns>
        public IEnumerable<UsingFile> GetAllFilesByPath(string folderName)
        {
            List<UsingFile> files = new List<UsingFile>();

            try
            {
                if (Directory.Exists(folderName))
                {
                    DirectoryInfo directory = new DirectoryInfo(folderName);

                    GetDirectories(files, directory);

                    GetFiles(files, directory);
                }
                else // убрать такую валидацию..... 
                    throw new ArgumentException("Вы ввели неправильный путь директории");
            }
            catch(ArgumentException)
            {
                throw;
            }

            foreach (var file in files)
                yield return file;
        
        }

        /// <summary>
        /// Получаем все файлы
        /// </summary>
        /// <param name="files"></param>
        /// <param name="directory"></param>
        private void GetFiles(List<UsingFile> files, DirectoryInfo directory)
        {
            foreach (FileInfo fileInfo in directory.GetFiles())
            {
                string fileExtension = Path.GetExtension(fileInfo.Name);
                string filePath = string.Format(@"{0}\{1}", fileInfo.DirectoryName, fileInfo.Name);
                DateTime lastModifDate = File.GetLastWriteTime(filePath);
                UsingFile file = new UsingFile // создание несколько раз встречается подумаю как можно это оптимизировать
                {
                    FileName = fileInfo.Name,
                    ShortName = Path.GetFileName(fileInfo.Name),
                    LastModificationDateString = lastModifDate.ToString(HelperConsts.DataFormat),
                    Extension = fileExtension,
                    IsFolder = false
                };
                files.Add(file);
            }
        }

        /// <summary>
        /// Получаем все папки
        /// </summary>
        /// <param name="files"></param>
        /// <param name="directory"></param>
        private void GetDirectories(List<UsingFile> files, DirectoryInfo directory)
        {
            foreach (DirectoryInfo dirInfo in directory.GetDirectories())
            {
                string folderPath = string.Format(@"{0}\{1}", Path.GetDirectoryName(dirInfo.FullName), dirInfo.Name);
                DateTime lastModifDate = Directory.GetLastWriteTime(dirInfo.FullName);
                UsingFile folder = new UsingFile
                {
                    FileName = dirInfo.FullName,
                    ShortName = Path.GetFileName(dirInfo.FullName),
                    LastModificationDateString = lastModifDate.ToString(HelperConsts.DataFormat),
                    IsFolder = true
                };
                files.Add(folder);

                if (Directory.EnumerateFileSystemEntries(folderPath).Any())
                {
                    files.AddRange(GetAllFilesByPath(folderPath));
                }
            }
        }
    }
}
