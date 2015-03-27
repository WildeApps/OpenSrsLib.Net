using System;

namespace OpenSrsLib.Commands.Lookup
{
    public class BelongsToRspRequest
    {
        public string Domain { get; set; }
    }

    public class BelongsToRspResponse : ResponseBase
    {
        public bool BelongsToRsp { get; set; }
        public DateTime? DomainExpiryDate { get; set; }
    }
}
