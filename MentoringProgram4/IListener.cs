using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentoringProgram4
{
    public interface IListener
    {
        void WatchAndListenFolder(string folderDirectory);

        void WatchAndListenFolder(IList<string> foldersDirectories);
    }
}
