using System;
using System.Collections.Generic;
using System.Linq;
using System.Runtime.Serialization;
using System.Text;
using System.Threading.Tasks;

namespace Mentoring11_Tools.Entities
{
    [DataContract]
    public class Worker
    {
        public int Id { get; set; }
        public string Name { get; set; }
    }
}
