namespace OpensenseMap.Models
{
    internal class CurrentLocation
    {
        public DateTime timestamp { get; set; }
        public List<double> coordinates { get; set; }
        public string type { get; set; }
    }
}
