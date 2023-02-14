using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OpensenseMap.Models
{
    internal class CurrentLocation
    {
        public DateTime timestamp { get; set; }
        public List<double> coordinates { get; set; }
        public string type { get; set; }
    }
}
