using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace MentoringProgram5
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                var container = new Container();
                container.AddAssembly(Assembly.GetExecutingAssembly());

                container.AddType(typeof(CustomerDAL), typeof(ICustomerDAL));
                container.AddType(typeof(Logger));
                container.AddType(typeof(CustomerBLL));
                container.AddType(typeof(CustomerBLL2));
            
                var customerBLL = container.CreateInstance<CustomerBLL>();
                var customerBLL2 = container.CreateInstance<CustomerBLL2>();

                var properties = customerBLL2.GetType().GetProperties();

                foreach (var prop in properties)
                {
                    var type = prop.GetValue(customerBLL2).GetType();
                    //Первый метод, остальные методы object
                    var method = type.GetMethods().First();
                    //не пойму зачемя я должен создавать снова объект.
                    var obj = Activator.CreateInstance(type);
                    method.Invoke(obj, null);  
                }
            }
            catch (MyOwnException ex)
            {
                Console.WriteLine(ex.Message);
            }

            Console.ReadKey();
        }
    }
}
