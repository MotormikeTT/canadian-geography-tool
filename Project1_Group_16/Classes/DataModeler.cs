using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Xml;

namespace Project1_Group_16.Classes
{
    /// <summary>
    /// Non-generic DataModeler class to parse the data files
    /// </summary>
    class DataModeler
    {
        // Property to hold the city information
        private static Dictionary<string, CityInfo> _cityCatalogue;

        // Customized delegate
        private delegate void Parse(string fileName);

        /// <summary>
        /// Invoke one of the parsing methods using the delegate.
        /// </summary>
        /// <param name="fileName">the file name to be parsed</param>
        /// <param name="fileType">the type of file to be parsed</param>
        /// <returns></returns>
        public static Dictionary<string, CityInfo> ParseFile(string fileName, string fileType)
        {
            Parse parseDel;
            switch (fileType.ToLowerInvariant())
            {
                case "csv":
                    parseDel = ParseCSV;
                    break;

                case "json":
                    parseDel = ParseJSON;
                    break;

                case "xml":
                    parseDel = ParseXML;
                    break;

                default:
                    throw new Exception($"Invalid file type has been entered: {fileType}. File type should only be one of the following: xml, csv, json");
            }

            // create a new dictionary instance
            _cityCatalogue = new Dictionary<string, CityInfo>();
            // call the delegate to parse the specified file
            parseDel(fileName);

            return _cityCatalogue;
        }

        /// <summary>
        /// parse a xml file and add the parsed data to the city information dictionary
        /// </summary>
        /// <param name="fileName">the file name to be parsed</param>
        private static void ParseXML(string fileName)
        {
            XmlDocument doc = new XmlDocument();
            doc.Load(fileName);
            foreach (XmlNode node in doc.DocumentElement.ChildNodes)
            {
                string Id = node.ChildNodes[8]?.InnerText;
                string name = node.ChildNodes[0]?.InnerText;
                string asci = node.ChildNodes[1]?.InnerText;
                string population = node.ChildNodes[7]?.InnerText;
                string province = node.ChildNodes[5]?.InnerText;
                string lat = node.ChildNodes[2]?.InnerText;
                string lng = node.ChildNodes[3]?.InnerText;
                string capital = node.ChildNodes[6]?.InnerText;

                CityInfo cityInfo = new CityInfo(Id, name, asci, population, province, lat, lng, capital);

                // add to dictionary
                string key = $"{name}, {province}";
                if (!_cityCatalogue.ContainsKey(key))
                {
                    if (name != string.Empty)
                        _cityCatalogue.Add(key, cityInfo);
                }
            }
        }

        /// <summary>
        /// parse a JSON file and add the parsed data to the city information dictionary
        /// </summary>
        /// <param name="fileName">the file name to be parsed</param>
        private static void ParseJSON(string fileName)
        {
            using StreamReader r = new StreamReader(fileName);
            string json = r.ReadToEnd();
            JArray items = JsonConvert.DeserializeObject<JArray>(json);
            foreach (var item in items.Children())
            {
                string Id = item.Value<string>("id");
                string name = item.Value<string>("city");
                string asci = item.Value<string>("city_ascii");
                string population = item.Value<string>("population");
                string province = item.Value<string>("admin_name");
                string lat = item.Value<string>("lat");
                string lng = item.Value<string>("lng");
                string capital = item.Value<string>("capital");

                CityInfo cityInfo = new CityInfo(Id, name, asci, population, province, lat, lng, capital);

                // add to dictionary
                string key = $"{name}, {province}";
                if (!_cityCatalogue.ContainsKey(key))
                {
                    if (name != string.Empty)
                        _cityCatalogue.Add(key, cityInfo);
                }
            }
        }

        /// <summary>
        /// parse a csv file and add the parsed data to the city information dictionary
        /// </summary>
        /// <param name="fileName">the file name to be parsed</param>
        private static void ParseCSV(string fileName)
        {
            List<string> cityData = new List<string>(File.ReadAllLines(fileName));
            // remove headers
            cityData.RemoveAt(0);

            foreach (var c in cityData)
            {
                string[] city = c.Split(',');

                string Id = city[8];
                string name = city[0];
                string asci = city[1];
                string population = city[7];
                string province = city[5];
                string lat = city[2];
                string lng = city[3];
                string capital = city[6];

                CityInfo cityInfo = new CityInfo(Id, name, asci, population, province, lat, lng, capital);

                // add to dictionary
                string key = $"{name}, {province}";
                if (!_cityCatalogue.ContainsKey(key))
                {
                    if (name != string.Empty)
                        _cityCatalogue.Add(key, cityInfo);
                }
            }
        }
    }   // end DataModeler class
}
