using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using GeoJSON.Net.Geometry;
using Globals;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace Data
{
    public class ReadFile
    {
        private List<Country> countries;

        public ReadFile()
        {
            this.LoadJson();
        }

        private void LoadJson()
        {
            string fileContent;
            using (StreamReader r = new StreamReader(@"../../../Data/Resources/europe-partial-100-AL2.geojson"))
            {
                fileContent = r.ReadToEnd();
            }

            dynamic deserialized = (JObject)JsonConvert.DeserializeObject(fileContent); ;

            countries = new List<Country>();

            foreach (var item in deserialized.features)
            {
                var geometry = item["geometry"];
                string name = item["properties"]["name"];

                var json = JsonConvert.SerializeObject(geometry);
                if (geometry["type"] == "MultiPolygon")
                {
                    var multiPolygon = JsonConvert.DeserializeObject<MultiPolygon>(json);
                    countries.Add(new Country(name, multiPolygon));
                }
                else if (geometry["type"] == "Polygon") 
                { 
                    var polygon =JsonConvert.DeserializeObject<Polygon>(json);
                    countries.Add(new Country(name, polygon));
                }
                
            }
        }
        public List<Country> GetCountries()
        {
            return this.countries;
        }

    }
}
