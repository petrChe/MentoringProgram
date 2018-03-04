using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MentoringProgram6_storing_system.Entities
{
    public class Patent : PaperInformationStorage
    {
        public string RegistrationNumber { get; set; }
        public DateTime RequestDate { get; set; }
        public DateTime PublishingDate { get; set; }
        public string Country { get; set; }
    }
}
