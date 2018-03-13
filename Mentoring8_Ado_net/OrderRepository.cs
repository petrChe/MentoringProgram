using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Common;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentoring8_Ado_net
{
    public class OrderRepository : IOrderRepository
    {
        private readonly DbProviderFactory ProviderFactory;
        private readonly string ConnectionString;

        public OrderRepository(string connectionString, string provider)
        {
            ProviderFactory = DbProviderFactories.GetFactory(provider);
            ConnectionString = connectionString;
        }


        public IEnumerable<Order> GetOrders()
        {
            using (var connection = ProviderFactory.CreateConnection())
            {
                var resultOrders = new List<Order>();

                connection.ConnectionString = ConnectionString;
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select OrderID, CustomerId, EmployeeId, OrderDate, RequiredDate, ShippedDate," +
                                                  "ShipVia, Freight, ShipName, ShipAddress, ShipCity, ShipRegion, ShipPostalCode," +
                                                  "ShipCountry from dbo.Orders";
                    command.CommandType = System.Data.CommandType.Text;

                    using (var rd = command.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            var order = new Order();
                            order.OrderId = rd.GetInt32(0);
                            order.CustomerId = rd.IsDBNull(1) ? null : rd.GetString(1);
                            order.EmployeeId = rd.IsDBNull(2) ? null : (int?)rd.GetInt32(2);
                            order.OrderDate = rd.IsDBNull(3) ? null : (DateTime?)rd.GetDateTime(3);
                            order.RequiredDate = rd.IsDBNull(4) ? null : (DateTime?)rd.GetDateTime(4);
                            order.ShippedDate = rd.IsDBNull(5) ? null : (DateTime?)rd.GetDateTime(5);
                            order.ShipVia = rd.IsDBNull(6) ? null : (int?)rd.GetInt32(6);
                            order.Freight = rd.IsDBNull(7) ? null : (decimal?)rd.GetDecimal(7);
                            order.ShipName = rd.IsDBNull(8) ? null : rd.GetString(8);
                            order.ShipAddress = rd.IsDBNull(9) ? null : rd.GetString(9);
                            order.ShipCity = rd.IsDBNull(10) ? null : rd.GetString(10);
                            order.ShipRegion = rd.IsDBNull(11) ? null : rd.GetString(11);
                            order.ShipPostalCode = rd.IsDBNull(12) ? null : rd.GetString(12);
                            order.ShipCountry = rd.IsDBNull(13) ? null : rd.GetString(13);
                            order.Status = order.GetStatus(order);

                            resultOrders.Add(order);
                        }
                    }
                }

                return resultOrders;
            }
        }


        public Order GetOrderDetails(int orderId)
        {
            using (var connection = ProviderFactory.CreateConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "select OrderID, CustomerId, EmployeeId, OrderDate, RequiredDate, ShippedDate," +
                                                  "ShipVia, Freight, ShipName, ShipAddress, ShipCity, ShipRegion, ShipPostalCode," +
                                                  "ShipCountry from dbo.Orders " +
                                          "where OrderID = @orderId;" +
                                          "select * from dbo.[Order Details] as det " +
                                          "join dbo.Products prod on det.ProductID = prod.ProductID " +
                                          "where det.OrderID = @orderId;";
                    command.CommandType = System.Data.CommandType.Text;

                    var orderIdParam = command.CreateParameter();
                    orderIdParam.ParameterName = "@orderId";
                    orderIdParam.Value = orderId;

                    command.Parameters.Add(orderIdParam);

                    using (var rd = command.ExecuteReader())
                    {
                        if (!rd.HasRows)
                            return null;

                        rd.Read();
                        
                        var order = new Order();
                        order.OrderId = rd.GetInt32(0);
                        order.CustomerId = rd.IsDBNull(1) ? null : rd.GetString(1);
                        order.EmployeeId = rd.IsDBNull(2) ? null : (int?)rd.GetInt32(2);
                        order.OrderDate = rd.IsDBNull(3) ? null : (DateTime?)rd.GetDateTime(3);
                        order.RequiredDate = rd.IsDBNull(4) ? null : (DateTime?)rd.GetDateTime(4);
                        order.ShippedDate = rd.IsDBNull(5) ? null : (DateTime?)rd.GetDateTime(5);
                        order.ShipVia = rd.IsDBNull(6) ? null : (int?)rd.GetInt32(6);
                        order.Freight = rd.IsDBNull(7) ? null : (decimal?)rd.GetDecimal(7);
                        order.ShipName = rd.IsDBNull(8) ? null : rd.GetString(8);
                        order.ShipAddress = rd.IsDBNull(9) ? null : rd.GetString(9);
                        order.ShipCity = rd.IsDBNull(10) ? null : rd.GetString(10);
                        order.ShipRegion = rd.IsDBNull(11) ? null : rd.GetString(11);
                        order.ShipPostalCode = rd.IsDBNull(12) ? null : rd.GetString(12);
                        order.ShipCountry = rd.IsDBNull(13) ? null : rd.GetString(13);
                        order.Status = order.GetStatus(order);                     

                        rd.NextResult();

                        order.Details = new List<OrderDetails>();
                    
                        while(rd.Read())
                        {
                            var orderDetails = new OrderDetails();
                            orderDetails.Discount = (Single?)rd["Discount"];
                            orderDetails.ProductId = (int)rd["ProductID"];
                            orderDetails.ProductName = (string)rd["ProductName"];
                            orderDetails.Quantity = (short?)rd["Quantity"];
                            orderDetails.UnitPrice = (decimal)rd["UnitPrice"];
                            
                            order.Details.Add(orderDetails);
                        }

                        return order;
                    }
                }
            }
        }


        public int CreateNewOrder(int orderId, DateTime? orderDate)
        {
            using (var connection = ProviderFactory.CreateConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "set identity_insert dbo.Orders on;" +
                                          "insert into dbo.Orders (OrderID, OrderDate) values(@orderId, @orderDate);" +
                                          "set identity_insert dbo.Orders off;";
                    command.CommandType = System.Data.CommandType.Text;

                    var orderIdParam = command.CreateParameter();
                    orderIdParam.ParameterName = "@orderId";
                    orderIdParam.Value = orderId;

                    var orderDateParam = command.CreateParameter();
                    orderDateParam.ParameterName = "@orderDate";
                    if (orderDate == null)
                        orderDateParam.Value = DBNull.Value;
                    else
                        orderDateParam.Value = orderDate;

                    command.Parameters.Add(orderIdParam);
                    command.Parameters.Add(orderDateParam);

                    var result = command.ExecuteNonQuery();

                    return result;
                }
            }
        }


        public int UpdateNewRequestShipCountry(int orderId, string newShipCountry)
        {
            using (var connection = ProviderFactory.CreateConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    var order = GetOrderDetails(orderId);
                    if (order.Status != OrderStatusEnum.New)
                        return 0;

                    command.CommandText = "update dbo.Orders set ShipCountry = @shipCountry;";

                    var shipCountryParam = command.CreateParameter();
                    shipCountryParam.ParameterName = "@shipCountry";
                    if (newShipCountry == null)
                        shipCountryParam.Value = DBNull.Value;
                    else
                        shipCountryParam.Value = newShipCountry;

                    command.Parameters.Add(shipCountryParam);

                    var result = command.ExecuteNonQuery();

                    return result;
                }
            }
        }


        public int DeleteNewAndInProcessOrders()
        {
            using (var connection = ProviderFactory.CreateConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandText = "delete * from dbo.Orders where OrderDate = null or ShippedDate == null";

                    var result = command.ExecuteNonQuery();

                    return result;
                }
            }
        }


        public int SetOrderDate(int orderId, DateTime? orderDate)
        {
            using (var connection = ProviderFactory.CreateConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    var order = GetOrderDetails(orderId);
                    if (order.Status == OrderStatusEnum.InProcess)
                        return 0;

                    command.CommandText = "update dbo.Orders set OrderDate = @orderDate;";

                    var orderDateParam = command.CreateParameter();
                    orderDateParam.ParameterName = "@orderDate";
                    if (orderDate == null)
                        orderDateParam.Value = DBNull.Value;
                    else
                        orderDateParam.Value = orderDate;

                    command.Parameters.Add(orderDateParam);

                    var result = command.ExecuteNonQuery();

                    return result;
                }
            }
        }

        public int SetShippedDate(int orderId, DateTime? shippedDate)
        {
            using (var connection = ProviderFactory.CreateConnection())
            {
                connection.ConnectionString = ConnectionString;
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    var order = GetOrderDetails(orderId);
                    if (order.Status == OrderStatusEnum.Completed)
                        return 0;

                    command.CommandText = "update dbo.Orders set ShippedDate = @shippedDate;";

                    var shippedDateParam = command.CreateParameter();
                    shippedDateParam.ParameterName = "@shippedDate";
                    if (shippedDate == null)
                        shippedDateParam.Value = DBNull.Value;
                    else
                        shippedDateParam.Value = shippedDate;

                    command.Parameters.Add(shippedDateParam);

                    var result = command.ExecuteNonQuery();

                    return result;
                }
            }
        }

        public Order CustOrdersDetail(int orderId)
        {
            using (var connection = ProviderFactory.CreateConnection())
            {
                var order = new Order();
                connection.ConnectionString = ConnectionString;
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandType = System.Data.CommandType.StoredProcedure;
                    command.CommandText = "CustOrdersDetail";
                    command.Parameters.Add(new SqlParameter("@OrderID", SqlDbType.Int, 0, "OrderID"));

                    command.Parameters[0].Value = orderId;

                    using(var rd = command.ExecuteReader())
                    {
                        order.Details = new List<OrderDetails>();

                        while (rd.Read())
                        {
                            var orderDetails = new OrderDetails();
                            orderDetails.Discount = (int?)rd["Discount"];
                            orderDetails.ProductName = (string)rd["ProductName"];
                            orderDetails.Quantity = (short?)rd["Quantity"];
                            orderDetails.UnitPrice = (decimal)rd["UnitPrice"];

                            order.Details.Add(orderDetails);
                        }
                    }

                }

                return order;
            }
        }


        public List<CustOrderHistory> CustOrderHist(string customerId)
        {
            using (var connection = ProviderFactory.CreateConnection())
            {
                var resultList = new List<CustOrderHistory>();
                connection.ConnectionString = ConnectionString;
                connection.Open();

                using (var command = connection.CreateCommand())
                {
                    command.CommandType = CommandType.StoredProcedure;
                    command.CommandText = "CustOrderHist";
                    command.Parameters.Add(new SqlParameter("@CustomerID", SqlDbType.Text, 0, "CustomerID"));

                    command.Parameters[0].Value = customerId;

                    using (var rd = command.ExecuteReader())
                    {
                        while (rd.Read())
                        {
                            var custOrderHist = new CustOrderHistory();
                            custOrderHist.ProductName = (string)rd["ProductName"];
                            custOrderHist.Total = (int)rd["Total"];

                            resultList.Add(custOrderHist);
                        }
                    }
                }

                return resultList;
            }
        }
    }
}
