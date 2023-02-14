using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpensenseMap.Models
{
    internal class Boxes
    {
        public string _id { get; set; }
        public DateTime createdAt { get; set; }
        public DateTime updatedAt { get; set; }
        public string name { get; set; }
        public CurrentLocation currentLocation { get; set; }
        public string exposure { get; set; }
        public List<Sensor> sensors { get; set; }
        public string model { get; set; }
        public DateTime lastMeasurementAt { get; set; }
        public List<string> grouptag { get; set; }
        public string description { get; set; }
        public string weblink { get; set; }
        public string image { get; set; }
    }
}
