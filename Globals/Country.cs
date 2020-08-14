using GeoJSON.Net.Geometry;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Globals
{
    public class Country
    {
        private string name;
        private MultiPolygon multiPolygon;

        private List<GeoJSON.Net.Geometry.Polygon> polygons = new List<GeoJSON.Net.Geometry.Polygon>();

        public Country(string name, MultiPolygon multipolygon)
        {
            this.name = name;
            this.multiPolygon = multipolygon;

            foreach (var item in multiPolygon.Coordinates)
            {
                this.polygons.Add(item);
            }
        }
        public Country(string name, Polygon polygon)
        {
            this.name = name;
            this.multiPolygon = null;
            this.polygons.Add(polygon);

        }
        public string GetName()
        {
            return this.name;
        }
        public MultiPolygon GetMultiPolygon()
        {
            return this.multiPolygon;
        }
        public  List<GeoJSON.Net.Geometry.Polygon> GetPolygons()
        {
            return this.polygons;
        }
    }
}
