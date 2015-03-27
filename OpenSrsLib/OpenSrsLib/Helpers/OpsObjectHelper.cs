using System.Linq;
using OpenSrsLib.Commands;

namespace OpenSrsLib.Helpers
{
    public static class OpsObjectHelper
    {
        /// <summary>
        /// Sets up an initial OPS_envelope object with the common objects that are needed and returns an object that provides easy access to
        /// the important composite objects such as the main body items array and the attributes array
        /// </summary>
        public static OpsObjectCollection BuildOpsEnvelope(string version, string action, string obj = null, string registrantIp = null)
        {
            var objects = new OpsObjectCollection();

            // main body array
            objects.MainBodyArray = new dt_assoc();

            // protocol/action/object
            objects.MainBodyArray.Items.Add(new item("protocol", "XCP"));
            objects.MainBodyArray.Items.Add(new item("action", action));

            if (obj != null)
            {
                objects.MainBodyArray.Items.Add(new item("object", obj));
            }

            if (registrantIp != null)
            {
                objects.MainBodyArray.Items.Add(new item("registrant_ip", registrantIp));
            }

            // attributes
            var attributes = new item("attributes");
            objects.AttributesArray = new dt_assoc();

            attributes.Item = objects.AttributesArray;

            objects.MainBodyArray.Items.Add(attributes);

            // create ops reuest object
            objects.OpsRequest = new OPS_envelope
            {
                header = BuildHeader(version),
                body = new body
                {
                    data_block = new data_block
                    {
                        Item = objects.MainBodyArray
                    }
                }
            };

            return objects;
        }

        /// <summary>
        /// Retrieves an item object from a dt_assoc array with the specified key
        /// </summary>
        public static item GetItemFromArray(dt_assoc array, string key)
        {
            return array.Items.Cast<item>().FirstOrDefault(i => i.key == key);
        }

        /// <summary>
        /// Returns an item from a response stored in the attributes dt_assoc array with the specified key
        /// </summary>
        public static item GetResponseAttributeItem(OPS_envelope response, string key)
        {
            var responseBodyArray = (dt_assoc)response.body.data_block.Item;
            var attributes = GetItemFromArray(responseBodyArray, "attributes");

            if (attributes == null)
                return null;

            var responseAttributesArray = (dt_assoc)attributes.Item;

            return GetItemFromArray(responseAttributesArray, key);
        }

        /// <summary>
        /// Builds the standard message header
        /// </summary>
        private static header BuildHeader(string version)
        {
            var header = new header();
            header.version = new version { Text = version };

            return header;
        }
    }
}
