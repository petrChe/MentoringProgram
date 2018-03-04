using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentoring6_xml_reader_writer
{
    public class Validator : IValidate
    {
        //public bool IsValid(IEnumerable<System.Xml.Linq.XElement> elements, string elementType)
        //{
        //    foreach(var el in elements)
        //    {
        //       var t =  el.NodeType;
        //    }



        //    switch (elementType)
        //    {
        //        case "Book":
                    
        //                ISBN = bookEl.Element("ISBN").Value,
        //                AuthorOrInventor = bookEl.Element("AuthorOrInventor").Value,
        //                PlaceOfPublication = bookEl.Element("PlaceOfPublication").Value,
        //                PageCount = Convert.ToInt32(bookEl.Element("PageCount").Value),
        //                Note = bookEl.Element("Note").Value,
        //                PublishingHouseName = bookEl.Element("PublishingHouseName").Value,
        //                PublishingYear = Convert.ToInt32(bookEl.Element("PublishingYear").Value)
        //            };

        //            paperInfoStorage.Books.Add(book);
        //            break;
        //        case "Newspaper":
        //            var newspaperEl = XElement.ReadFrom(reader) as XElement;
        //            var newspaper = new Newspaper
        //            {
        //                Number = Convert.ToInt32(newspaperEl.Element("Number").Value),
        //                Date = Convert.ToDateTime(newspaperEl.Element("Date").Value),
        //                ISSN = newspaperEl.Element("ISSN").Value,
        //                Name = newspaperEl.Element("Name").Value,
        //                PlaceOfPublication = newspaperEl.Element("PlaceOfPublication").Value,
        //                PageCount = Convert.ToInt32(newspaperEl.Element("PageCount").Value),
        //                Note = newspaperEl.Element("Note").Value,
        //                PublishingHouseName = newspaperEl.Element("PublishingHouseName").Value,
        //                PublishingYear = Convert.ToInt32(newspaperEl.Element("PublishingYear").Value)
        //            };

        //            paperInfoStorage.Newspapers.Add(newspaper);
        //            break;
        //        case "Patent":
        //            var patentEl = XElement.ReadFrom(reader) as XElement;
        //            var patent = new Patent
        //            {
        //                RegistrationNumber = patentEl.Element("RegistrationNumber").Value,
        //                RequestDate = Convert.ToDateTime(patentEl.Element("RequestDate").Value),
        //                PublishingDate = Convert.ToDateTime(patentEl.Element("PublishingDate").Value),
        //                Country = patentEl.Element("Country").Value,
        //                Name = patentEl.Element("Name").Value,
        //                PageCount = Convert.ToInt32(patentEl.Element("PageCount").Value),
        //                Note = patentEl.Element("Note").Value,
        //                AuthorOrInventor = patentEl.Element("AuthorOrInventor").Value,
        //                PublishingYear = Convert.ToInt32(patentEl.Element("PublishingYear").Value)
        //            };

        //            paperInfoStorage.Patents.Add(patent);
        //            break;
        //    };
        //}
        public bool IsValid(IEnumerable<System.Xml.Linq.XElement> elements, string elementType)
        {
            throw new NotImplementedException();
        }
    }
}
