using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Xml.Linq;

namespace Mentoring6_xml_reader_writer
{
    public interface IValidate
    {
        bool IsValid(IEnumerable<XElement> elements, string elementType);
    }
}
