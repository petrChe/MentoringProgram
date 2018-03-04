using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using Mentoring6_storing_system;
using Mentoring6_xml_reader_writer;

namespace Mentoring6
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var preparator = new Preparator();
                var xmlReaderWriter = new XmlReaderWriter();

                var bigCollection = new List<IEnumerable<PaperInformationStorage>>
                {
                    preparator.Books,
                    preparator.Newspapers,
                    preparator.Patents
                };

                var doc = xmlReaderWriter.CreateXml(bigCollection);
                xmlReaderWriter.CreateFinalDocument(doc);


                var paperInfoStorage = xmlReaderWriter.XmlRead(@"D:\document.xml");
            }
            catch (IOException ex)
            {
                Console.WriteLine(ex.Message);
            }
            Console.ReadKey();
        }
    }
}
