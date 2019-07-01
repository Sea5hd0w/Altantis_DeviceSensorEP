using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altantis_DeviceSensorEP.Business
{
    public class RawData
    {
        public string MacAddress { get; set; }
        public DateTime MetricDate { get; set; }
        public string SensorType { get; set; }
        public string Name { get; set; }
        public double MetricValue { get; set; }

        public RawData(string macAddress, DateTime metricDate, string sensorType, string name, double metricValue)
        {
            MacAddress = macAddress;
            MetricDate = metricDate;
            SensorType = sensorType;
            Name = name;
            MetricValue = metricValue;
        }
    }
}
