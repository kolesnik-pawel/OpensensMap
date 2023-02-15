namespace OpensenseMap.Models
{
    internal class BoxesDataMeasurementsReturn
    {
        public DateTime createdAt { get; set; }
        public string value { get; set; }
        public string sensorId { get; set; }
        public double lat { get; set; }
        public double lon { get; set; }
        public string boxId { get; set; }
        public string phenomenon { get; set; }
        public string exposure { get; set; }
        public string boxName { get; set; }
        public string unit { get; set; }
    }
}
