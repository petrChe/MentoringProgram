using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Mentoring8_Ado_net
{
    public class OrderDetails
    {
        public decimal UnitPrice { get; set; }
        public short? Quantity { get; set; }
        public Single? Discount { get; set; }
        public int ProductId { get; set; }
        public string ProductName { get; set; }
    }
}
