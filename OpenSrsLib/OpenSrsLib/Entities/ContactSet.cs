using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSrsLib.Entities
{
    public class ContactSet
    {
        public Contact Owner { get; set; }
        public Contact Admin { get; set; }
        public Contact Billing { get; set; }
        public Contact Tech { get; set; }
    }
}
