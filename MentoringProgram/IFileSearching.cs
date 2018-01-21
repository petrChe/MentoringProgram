using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentoringProgram
{
    interface IFileSearching
    {
        void AllFilteredFilesFromDirectory(Func<UsingFile, string, bool> Filter, string filterDetail, string folderName);
    }
}
