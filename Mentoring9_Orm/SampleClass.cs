using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Data.Entity;

namespace Mentoring9_Orm
{
    public class SampleClass
    {
        public static void GetOrdersByCategory(string categoryName)
        {
            var resultList = new List<Order>();

            using (var db = new NorthwindDb())
            {
                db.Configuration.LazyLoadingEnabled = false;

                //foreach (var ord in db.Orders)
                //{
                //    foreach (var det in ord.Order_Details)
                //    {
                //        if (det.Product.Category.CategoryName == categoryName)
                //        {
                //            Console.WriteLine(string.Format("Order Id {0}, Order date {1}, Customer name {2}, Product name {3}, Category name {4}",
                //                ord.OrderID, ord.OrderDate, ord.Customer.CompanyName, det.Product.ProductName, det.Product.Category.CategoryName));
                //        }
                //    }
                //}    

                var orders = db.Orders
                    .Include(o => o.Order_Details.Select(det => det.Product.Category))
                    .Include(o => o.Customer);

                foreach(var ord in orders)
                {
                    ord.Order_Details
                        .Where(det => det.Product.Category.CategoryName == categoryName)
                        .Select(det =>
                            string.Concat(ord.OrderID, ", ",
                                          ord.RequiredDate, ", ",                                            
                                          det.Product.ProductName, ", ",
                                          det.Product.Category.CategoryName, ", "
                                          )).ToList().ForEach(Console.WriteLine);
                }
            }       
        }
    }
}