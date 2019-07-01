using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altantis_DeviceSensorEP.Business
{
    public class RawData
    {
        [JsonProperty(PropertyName = "macAddress")]
        public string MacAddress { get; set; }
        [JsonProperty(PropertyName = "metricDate")]
        public DateTime MetricDate { get; set; }
        [JsonProperty(PropertyName = "deviceType")]
        public string DeviceType { get; set; }
        [JsonProperty(PropertyName = "name")]
        public string Name { get; set; }
        [JsonProperty(PropertyName = "metricValue")]
        public double MetricValue { get; set; }

        public RawData(string macAddress, DateTime metricDate, string deviceType, string name, double metricValue)
        {
            MacAddress = macAddress;
            MetricDate = metricDate;
            DeviceType = deviceType;
            Name = name;
            MetricValue = metricValue;
        }
    }
}
