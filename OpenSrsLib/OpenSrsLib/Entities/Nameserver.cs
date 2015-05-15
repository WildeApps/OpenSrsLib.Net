using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSrsLib.Entities
{
    public class Nameserver
    {
        public string ip_address { get; set; }
        public string ipv6 { get; set; }
        public string name { get; set; }
        public int sort_order { get; set; }
    }
}
