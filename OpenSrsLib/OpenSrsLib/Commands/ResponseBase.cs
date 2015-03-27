using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpenSrsLib.Commands
{
    public abstract class ResponseBase
    {
        public string Protocol { get; set; }
        public string Action { get; set; }
        public long ResponseCode { get; set; }
        public string ResponseText { get; set; }
        public string Xml { get; set; }
    }
}
