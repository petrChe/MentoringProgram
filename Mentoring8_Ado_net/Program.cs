using System;
using System.Collections.Generic;
using System.Configuration;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentoring8_Ado_net
{
    class Program
    {
        static void Main(string[] args)
        {
            var connectionStr = ConfigurationManager.ConnectionStrings["Test"].ConnectionString;
            var providerName = ConfigurationManager.ConnectionStrings["Test"].ProviderName;
            OrderRepository or = new OrderRepository(connectionStr, providerName);
            var orders = or.GetOrders();
            var order = or.GetOrderDetails(10250);
            //Identity insert on. Решил так попробовать. Id надо выбрать другой.
            var insrt = or.CreateNewOrder(55556, null);
            var updt = or.UpdateNewRequestShipCountry(55556, "Neverland"); 
            var orderProc = or.CustOrdersDetail(10250);
            var custOrderHistProc = or.CustOrderHist("ALFKI");
            Console.ReadKey();
        }
    }
}
