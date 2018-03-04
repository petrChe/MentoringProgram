using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentoringProgram6_storing_system.Entities
{
    public class PaperInformationStorage
    {
        public string Name { get; set; }
        public string PlaceOfPublication { get; set; }
        public int PageCount { get; set; }
        public string Note { get; set; }
        public string AuthorOrInventor { get; set; }
        public string PublishingHouseName { get; set; }
        public int PublishingYear { get; set; }
    }
}
