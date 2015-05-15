using System;
using System.Globalization;
using System.Linq;
using OpenSrsLib.Commands;
using OpenSrsLib.Entities;

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
        /// Returns an item from a response stored in the dt_assoc array with the specified key
        /// </summary>
        public static item GetResponseDataBlockItem(OPS_envelope response, string key)
        {
            var responseBodyArray = (dt_assoc)response.body.data_block.Item;
            if (responseBodyArray == null)
                return null;

            return GetItemFromArray(responseBodyArray, key);
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
        /// Converts a .net bool to an OpenSrs bool
        /// </summary>
        /// <param name="b">The bool to convert</param>
        /// <returns>bool converted to a string</returns>
        public static string NetBoolToSrsBool(bool b)
        {
            return b ? "1" : "0";
        }

        /// <summary>
        /// Converts an OpenSrs bool to a .net bool
        /// </summary>
        /// <param name="s">Bool string to convert</param>
        /// <returns>The converted bool</returns>
        public static bool SrsBoolToNetBool(string s)
        {
            if (s == "1" || s == "Y")
            {
                return true;
            }

            if (s == "0" || s == "N")
            {
                return false;
            }

            return false;
        }

        /// <summary>
        /// Converts a date string to a nullable DateTime
        /// </summary>
        /// <param name="dateString">The string to convert</param>
        /// <returns>The nullable DateTime</returns>
        public static DateTime? ConvertToNullableDateTime(string dateString)
        {
            if (!string.IsNullOrEmpty(dateString))
            {
                DateTime dateTime;
                if (DateTime.TryParseExact(dateString, "yyyy-MM-dd HH:mm:ss", null, DateTimeStyles.None, out dateTime))
                {
                    return dateTime;
                }
            }
            return null;
        }

        /// <summary>
        /// Converts a date string to a DateTime
        /// </summary>
        /// <param name="dateString">The string to convert</param>
        /// <returns>The DateTime</returns>
        public static DateTime ConvertToDateTime(string dateString)
        {
            DateTime dateTime = new DateTime();
            if (!string.IsNullOrEmpty(dateString))
            {
                if (DateTime.TryParseExact(dateString, "yyyy-MM-dd HH:mm:ss", null, DateTimeStyles.None, out dateTime))
                {
                    return dateTime;
                }
            }
            return dateTime;
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
