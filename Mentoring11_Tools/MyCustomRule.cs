using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;
using StyleCop;
using StyleCop.CSharp;

namespace Mentoring11_Tools
{
    [SourceAnalyzer(typeof(CsParser))]
    public class MyCustomRule : SourceAnalyzer
    {
        const string rule = "MyCustomRule";

        public override void AnalyzeDocument(CodeDocument document)
        {
            CsDocument doc = (CsDocument)document;
            doc.WalkDocument(new CodeWalkerElementVisitor<object>(this.VisitElement));
        }

        private bool VisitElement(CsElement el, CsElement parent, object context)
        {
            var result = true;

            var classElement = el as Class;

            if (classElement != null)
            {
                var attrs = classElement.GetType().GetCustomAttributes(false);

                var isDataContract = attrs.Any(delegate(object attr)
                {
                    var dataContextAttr = attr as DataContractAttribute;
                    return attr != null;
                });

                if (!isDataContract)
                    this.AddViolation(el, rule);

                if (!classElement.GetType().IsPublic)
                {
                    this.AddViolation(el, rule);
                }

                result = false;
            }

            return result;
        }

    }
}

