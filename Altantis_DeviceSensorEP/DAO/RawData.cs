using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Altantis_DeviceSensorEP.DAO
{
    public class RawData
    {
        public string Data { get; set; }

        public RawData(string data)
        {
            Data = data;
        }
    }
}
