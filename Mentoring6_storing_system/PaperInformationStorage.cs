using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Xml.Serialization;

namespace Mentoring6_storing_system
{
    public class PaperInformationStorage
    {
        public List<Book> Books { get; set; }
        public List<Newspaper> Newspapers { get; set; }
        public List<Patent> Patents { get; set; }

        public string Name { get; set; }

        [XmlElement(IsNullable = true)]
        public string PlaceOfPublication { get; set; }
        
        public int PageCount { get; set; }
        public string Note { get; set; }
        public string AuthorOrInventor { get; set; }

        [XmlElement(IsNullable = true)]
        public string PublishingHouseName { get; set; }

        [XmlIgnore]
        public int PublishingYear { get; set; }
    }
}
