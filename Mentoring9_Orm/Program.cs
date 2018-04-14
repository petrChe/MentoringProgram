using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentoring9_Orm
{
    class Program
    {
        static void Main(string[] args)
        {
            PrintOrders();
            Console.ReadKey();
        }

        private static void PrintOrders()
        {
            SampleClass.GetOrdersByCategory("Seafood");

            //foreach (var ord in orders)
            //{
            //    Console.WriteLine(string.Format("OrderID {0}, CompanyName {1}",
            //            ord.OrderID, ord.Customer.CompanyName));
            //    Console.WriteLine();

            //    foreach (var det in ord.Order_Details)
            //    {
            //        Console.WriteLine(string.Format("Product name {0}, Category name {1}",
            //            det.Product.ProductName, det.Product.Category.CategoryName));
            //    }
            //    Console.WriteLine("++++++++++++++++++++++++++++++++++++++++++++++++++++");
            //}
        }
    }
}
