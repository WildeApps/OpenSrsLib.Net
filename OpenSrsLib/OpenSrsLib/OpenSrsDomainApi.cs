using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OpenSrsLib.Commands;
using OpenSrsLib.Commands.Lookup;

namespace OpenSrsLib
{
    public class OpenSrsDomainApi
    {
        private string _version;
        private readonly string _registrantIp;
        private OpsObjectHelper _opsObjectHelper;

        public OpenSrsDomainApi(string version, string registrantIp, string username, string privateKey, string openSrsUrl)
        {
            _version = version;
            _registrantIp = registrantIp;
            _opsObjectHelper = new OpsObjectHelper(username, privateKey, openSrsUrl);
        }

        public BelongsToRspResponse BelongsToRsp(BelongsToRspRequest request)
        {
            OpsObjectCollection opsObjects = _opsObjectHelper.BuildOpsEnvelope(_version, "SW_REGISTER", "DOMAIN", _registrantIp);

            opsObjects.AttributesArray.Items.Add(new item("domain", request.Domain));

            var srsResponse = _opsObjectHelper.SendRequest(opsObjects.OpsRequest);

            //TODO - set the mapping from Ops_Envelope to command response in the command response class
        }
    }
}
