using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using System.Xml;

namespace XmlReaderWriter
{
    public class XmlReaderWriter
    {
        public XmlDocument CreateXml<T>(IEnumerable<T> parametrElements)
        {
            var xmlDocument = new XmlDocument();
            var xmlRootElement = xmlDocument.CreateElement("rootElement");
            xmlRootElement.SetAttribute("LibraryName", Assembly.GetExecutingAssembly().GetName().Name);

            var xmlElements = xmlDocument.CreateElement(typeof(T).Name + "Elements");
            foreach (var el in parametrElements)
            {
                var xmlElement = xmlDocument.CreateElement(el.GetType().Name);

                foreach (var prop in typeof(T).GetProperties(BindingFlags.Public | BindingFlags.Instance))
                {
                    var propXmlElement = xmlDocument.CreateElement(prop.Name);
                    propXmlElement.InnerText = el.GetType().GetProperty(prop.Name).GetValue(el).ToString();
                    xmlElement.AppendChild(propXmlElement);
                }

                xmlElements.AppendChild(xmlElement);
            }
            xmlRootElement.AppendChild(xmlElements);
            xmlDocument.AppendChild(xmlRootElement);

            return xmlDocument;
        }
    }
}
