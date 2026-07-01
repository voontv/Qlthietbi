using BusinessLogic.Providers;
using Elfie.Serialization;
using Newtonsoft.Json;
using QlThietBi.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Security.Cryptography;
using System.Text;
using System.Threading.Tasks;
using System.Xml;
using System.Xml.Linq;

namespace QlThietBi.LibsStartup
{
    public static class Utils
    {
        public static string MD5Hash(string pass)
        {
            var md5 = pass;
            using (var md5Hash = MD5.Create())
            {
                // Byte array representation of source string
                var sourceBytes = Encoding.UTF8.GetBytes(pass);

                // Generate hash value(Byte Array) for input data
                var hashBytes = md5Hash.ComputeHash(sourceBytes);

                // Convert hash byte array to string
                var hash = BitConverter.ToString(hashBytes).Replace("-", string.Empty);

                // Output the MD5 hash
                md5 = hash;
            }

            return md5;
        }

        public class PropertyCopier<TParent, TChild> where TParent : class
                                            where TChild : class
        {
            public static void Copy(TParent parent, TChild child)
            {
                var parentProperties = parent.GetType().GetProperties();
                var childProperties = child.GetType().GetProperties();

                foreach (var parentProperty in parentProperties)
                {
                    foreach (var childProperty in childProperties)
                    {
                        if (parentProperty.Name == childProperty.Name && parentProperty.PropertyType == childProperty.PropertyType)
                        {
                            childProperty.SetValue(child, parentProperty.GetValue(parent));
                            break;
                        }
                    }
                }
            }
        }

        public static DateTime firstDayInMonth()
        {
            var curentDate = DateTime.Now;
            var firstDate = new DateTime(curentDate.Year, curentDate.Month, 1);

            return firstDate;
        }

        public static DateTime firstDayInLastMonth()
        {
            var today = DateTime.Today;
            var fistDay = new DateTime(today.Year, lastMonth().Month, 1);

            return fistDay;
        }

        public static DateTime lastMonth()
        {
            var today = DateTime.Today;
            var lastMonth = new DateTime(today.Year, today.Month, 1).AddMonths(-1);

            return lastMonth;
        }

        public static DateTime lastDayInLastMonth()
        {
            var lastDay = firstDayInMonth().AddDays(-1);

            return lastDay;
        }
        public static string ConvertXmlToJson(string response_xml)
        {

            string json = string.Empty;

            if (!string.IsNullOrEmpty(response_xml))
            {
                var doc = XElement.Parse(response_xml);
                var node_cdata = doc.DescendantNodes().OfType<XCData>().ToList();

                foreach (var node in node_cdata)
                {
                    if (node.Parent != null)
                    {
                        node.Parent.Add(node.Value);
                    }
                    node.Remove();
                }

                json = JsonConvert.SerializeXNode(doc, Newtonsoft.Json.Formatting.None, false).ToString();
            }
            json = json.Replace(@"{""Data"":{""Item"":", "");
            if (json.Length >= 2)
            {
                json = json.Substring(0, json.Length - 2);
            }
            return json;
        }

        public static string PassConfig(WebConfig config)
        {

            return $@"<AGENT_CODE>{config.AgentCode}</AGENT_CODE>
                    <USR>{config.User.EncodeMD5()}</USR>
                    <PASS>{config.Pass.EncodeMD5()}</PASS>
                    <PASS_K>{config.PassK.EncodeMD5()}</PASS_K>";
        }
        public static bool IsCodeCustumer(string number)
        {
            if (number.Length != 9) return false;
            return System.Text.RegularExpressions.Regex.Match(number, @"^([0-9]{9})$").Success;
        }

        public static bool IsEmpty<T>(List<T> list)
        {
            if (list == null)
            {
                return true;
            }

            return list.Count < 1;
        }
    }
}
