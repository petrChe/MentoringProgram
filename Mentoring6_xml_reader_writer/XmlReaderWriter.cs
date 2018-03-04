using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;
using Mentoring6_storing_system;

namespace Mentoring6_xml_reader_writer
{
    public class XmlReaderWriter
    {
        private IValidate validator;

        public XmlReaderWriter()
        {
            validator = new Validator();
        }

        #region Запись
        public XmlElement CreateBaseRoot(Type baseType, out XmlDocument doc)
        {
            var xmlDocument = new XmlDocument();
            var xmlRootElement = xmlDocument.CreateElement("rootElement");
            xmlRootElement.SetAttribute("LoadingDate", DateTime.Now.ToShortDateString());
            xmlRootElement.SetAttribute("LoadingTime", DateTime.Now.ToShortTimeString());
            xmlRootElement.SetAttribute("LibraryName", Assembly.GetAssembly(baseType).GetName().Name);

            doc = xmlDocument;
            return xmlRootElement;
        }

        public XmlDocument CreateXml<T>(List<IEnumerable<T>> collectionList) where T : PaperInformationStorage//, XmlElement rootElement, XmlDocument xmlDocument)
        {
            var xmlDocument = new XmlDocument();
            var xmlRootElement = xmlDocument.CreateElement("rootElement");
            xmlRootElement.SetAttribute("LoadingDate", DateTime.Now.ToShortDateString());
            xmlRootElement.SetAttribute("LoadingTime", DateTime.Now.ToShortTimeString());
            xmlRootElement.SetAttribute("LibraryName", Assembly.GetAssembly(typeof(T)).GetName().Name);

            foreach (var collection in collectionList)
            {
                var xmlElements = xmlDocument.CreateElement(collection.First().GetType().Name + "Elements");

                foreach (var el in collection)
                {
                    var xmlElement = xmlDocument.CreateElement(el.GetType().Name);

                    foreach (var prop in el.GetType().GetProperties(BindingFlags.Public | BindingFlags.Instance))
                    {
                        var propXmlElement = xmlDocument.CreateElement(prop.Name);
                        if (el.GetType().GetProperty(prop.Name).GetValue(el) != null)
                        {
                            propXmlElement.InnerText = el.GetType().GetProperty(prop.Name).GetValue(el).ToString();
                            xmlElement.AppendChild(propXmlElement);
                        }
                    }

                    xmlElements.AppendChild(xmlElement);
                }
                xmlRootElement.AppendChild(xmlElements);
            }
            xmlDocument.AppendChild(xmlRootElement);

            return xmlDocument;
        }

        public void CreateFinalDocument(XmlDocument xmlDocument)
        {
            //Сохранение xml
            xmlDocument.Save(@"D:\document.xml");
        }
        #endregion

        #region Чтение

        public PaperInformationStorage XmlRead(string fileUri)
        {
            if (File.Exists(fileUri))
            {
                string fileExtension = Path.GetExtension(fileUri);

                if (fileExtension != ".xml")
                    throw new IOException("Неверный формат файла");

                var paperInfoStorage = new PaperInformationStorage();
                paperInfoStorage.Books = new List<Book>();
                paperInfoStorage.Newspapers = new List<Newspaper>();
                paperInfoStorage.Patents = new List<Patent>();

                using (XmlReader reader = XmlReader.Create(fileUri))
                {
                    var elementType = string.Empty;
                    while (reader.Read())
                    {

                        if (reader.IsStartElement() && reader.Name.Contains("Elements"))
                        {
                            elementType = reader.Name.Substring(0, reader.Name.Length - 8);
                            continue;
                        }

                        if (reader.IsStartElement())
                        {

                            switch (elementType)
                            {
                                case "Book":
                                    var bookEl = XElement.ReadFrom(reader) as XElement;
                                    var bookElements = bookEl.Elements();

                                    var pageCount = 0;
                                    var publishingYear = 0;

                                    if ((bookEl.Element("PageCount") != null &&!Int32.TryParse(bookEl.Element("PageCount").Value, out pageCount)) ||
                                        (bookEl.Element("PublishingYear") != null && !Int32.TryParse(bookEl.Element("PublishingYear").Value, out publishingYear)))
                                        continue;

                                    var book = new Book
                                    {
                                        ISBN = bookEl.Element("ISBN").Value,
                                        AuthorOrInventor = bookEl.Element("AuthorOrInventor").Value,
                                        PlaceOfPublication = bookEl.Element("PlaceOfPublication").Value,
                                        PageCount = pageCount,
                                        Note = bookEl.Element("Note").Value,
                                        PublishingHouseName = bookEl.Element("PublishingHouseName").Value,
                                        PublishingYear = publishingYear
                                    };

                                    paperInfoStorage.Books.Add(book);
                                    break;
                                case "Newspaper":
                                    var newspaperEl = XElement.ReadFrom(reader) as XElement;

                                    var number = 0;
                                    var date = new DateTime();
                                    var newspaperPageCount = 0;
                                    var newspaperPublishingYear = 0;
                                    
                                    if (!Int32.TryParse(newspaperEl.Element("Number").Value, out number) ||
                                        !DateTime.TryParse(newspaperEl.Element("Date").Value, out date) ||
                                        !Int32.TryParse(newspaperEl.Element("PageCount").Value, out newspaperPageCount) ||
                                        !Int32.TryParse(newspaperEl.Element("PublishingYear").Value, out newspaperPublishingYear))
                                        continue;

                                    var newspaper = new Newspaper
                                    {
                                        Number = number,
                                        Date = date,
                                        ISSN = newspaperEl.Element("ISSN").Value,
                                        Name = newspaperEl.Element("Name").Value,
                                        PlaceOfPublication = newspaperEl.Element("PlaceOfPublication").Value,
                                        PageCount = newspaperPageCount,
                                        Note = newspaperEl.Element("Note").Value,
                                        PublishingHouseName = newspaperEl.Element("PublishingHouseName").Value,
                                        PublishingYear = newspaperPublishingYear
                                    };

                                    paperInfoStorage.Newspapers.Add(newspaper);
                                    break;
                                case "Patent":
                                    var patentEl = XElement.ReadFrom(reader) as XElement;

                                    var requestDate = new DateTime();
                                    var publishingDate = new DateTime();
                                    var patentPageCount = 0;
                                    var patentPublishingYear = 0;

                                    if (!DateTime.TryParse(patentEl.Element("RequestDate").Value, out requestDate) ||
                                        !DateTime.TryParse(patentEl.Element("PublishingDate").Value, out publishingDate) ||
                                        !Int32.TryParse(patentEl.Element("PageCount").Value, out patentPageCount) ||
                                        !Int32.TryParse(patentEl.Element("PublishingYear").Value, out patentPublishingYear))
                                        continue;

                                    var patent = new Patent
                                    {
                                        RegistrationNumber = patentEl.Element("RegistrationNumber").Value,
                                        RequestDate = requestDate,
                                        PublishingDate = publishingDate,
                                        Country = patentEl.Element("Country").Value,
                                        Name = patentEl.Element("Name").Value,
                                        PageCount = patentPageCount,
                                        Note = patentEl.Element("Note").Value,
                                        AuthorOrInventor = patentEl.Element("AuthorOrInventor").Value,
                                        PublishingYear = patentPublishingYear
                                    };

                                    paperInfoStorage.Patents.Add(patent);
                                    break;
                            };
                        }

                    }
                }

                return paperInfoStorage;
            }
            else
                throw new IOException("Такого файла не существует");
        }
        #endregion
    }
}
