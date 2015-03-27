using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSrsLib.Entities
{
    public class Domain
    {
        public bool AutoRenew { get; set; }
        public ContactSet ContactSet { get; set; }
        public bool CustomNameservers { get; set; }
        public bool CustomTransferNameservers { get; set; }
        public bool CustomTechContact { get; set; }
        public string DomainName { get; set; }
        public bool LockDomain { get; set; }
        public bool WhoIsPrivacy { get; set; }
        public string Handle { get; set; }
    }
}
