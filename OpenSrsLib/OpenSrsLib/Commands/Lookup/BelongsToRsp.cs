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

        public BelongsToRspResponse(OPS_envelope response)
        {
            //TODO - set the mapping from Ops_Envelope to command response in the command response class
        }
    }
}
