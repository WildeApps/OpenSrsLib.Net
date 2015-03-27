namespace OpenSrsLib.Commands
{
    public class OpsObjectCollection
    {
        internal OPS_envelope OpsRequest { get; set; }

        internal dt_assoc MainBodyArray { get; set; }

        internal dt_assoc AttributesArray { get; set; }
    }
}
