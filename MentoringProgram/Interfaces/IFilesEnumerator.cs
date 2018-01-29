using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentoringProgram.Interfaces
{
    public interface IFilesEnumerator
    {
        IEnumerable<UsingFile> GetAllFilesByPath(string folderName);
    }
}
