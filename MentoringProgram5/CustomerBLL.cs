using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MentoringProgram5.Attributes;

namespace MentoringProgram5
{
    [ImportConstructor]
    public class CustomerBLL
    {
        public CustomerBLL(ICustomerDAL dal, Logger logger)
        {

        }

        public CustomerBLL(Logger logger1, Logger logger2)
        {

        }

        public CustomerBLL()
        { }
    }
}