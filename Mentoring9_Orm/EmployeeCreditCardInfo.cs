using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Mentoring9_Orm
{
    public partial class EmployeeCreditCardInfo
    {
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Usage", "CA2214:DoNotCallOverridableMethodsInConstructors")]
        public EmployeeCreditCardInfo()
        {
        }

        [StringLength(5)]
        public string EmployeeCreditCardInfoID { get; set; }

        public DateTime? ExpiredDate { get; set; }

        [StringLength(10)]
        public string CreditCardNumber { get; set; }

        [StringLength(40)]
        public string CreditCardHolderName { get; set; }

        public int? EmployeeID { get; set; }

        public virtual Employee Employee { get; set; }
    }
}
