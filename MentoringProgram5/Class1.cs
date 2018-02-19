using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MentoringProgram5.Attributes;

namespace MentoringProgram5
{
    public class CustomerBLL2
    {
        [Import]
        public ICustomerDAL CustomerDAL { get; set; }

        [Import]
        public Logger Logger { get; set; }
    }
}
