using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace OpenlyLocal.Core.Models
{
    public abstract class GeoJson
    {
        public string type { get; set; }
        public abstract List<List<Point>> Polygons { get; }
    }
    public class Polygon : GeoJson
    {
        public List<List<List<double>>> coordinates { get; set; }


        public override List<List<Point>> Polygons
        {
            get {
                return new List<List<Point>>{
                    coordinates.SelectMany(x=>x).Select(x=> new Point{ Lng =  x[0], Lat = x[1] }).ToList()
                };
            }
        }
    }
    public class MultiPolygon : GeoJson
    {
        public List<List<List<List<double>>>> coordinates { get; set; }

        public override List<List<Point>> Polygons
        {
            get
            {
                return coordinates.FirstOrDefault()
                    .Select(y => y.Select(x => new Point { Lng = x[0], Lat = x[1] }).ToList()).ToList();
            }
        }
    }

    public class Point : ILocation
    {
        public double Lat
        {
            get;
            set;
        }

        public double Lng
        {
            get;
            set;
        }
    }
}