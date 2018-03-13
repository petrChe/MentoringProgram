using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentoring8_Ado_net
{
    public interface IOrderRepository
    {
        IEnumerable<Order> GetOrders();

        Order GetOrderDetails(int orderId);

        Order CustOrdersDetail(int orderId);
        List<CustOrderHistory> CustOrderHist(string customerId);

        int CreateNewOrder(int orderId, DateTime? orderDate);

        int UpdateNewRequestShipCountry(int orderId, string newShipCountry);

        int DeleteNewAndInProcessOrders();

        int SetOrderDate(int orderId, DateTime? orderDate);

        int SetShippedDate(int orderId, DateTime? shippedDate);
    }
}
