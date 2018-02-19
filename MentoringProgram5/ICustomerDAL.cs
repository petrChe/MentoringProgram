using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using MentoringProgram5.Attributes;

namespace MentoringProgram5
{
    public interface ICustomerDAL
    {
        void PrintCustomer();
    }

    [Export(typeof(ICustomerDAL))]
    public class CustomerDAL : ICustomerDAL
    {
        public void PrintCustomer()
        {
            Console.WriteLine("This is a dal customer");
        }
    }
}
