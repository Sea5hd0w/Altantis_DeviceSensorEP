using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Altantis_DeviceSensorEP.Controllers
{
    [Produces("application/json")]
    [Route("api/Sensor")]
    public class SensorController : Controller
    {
        // GET: api/Sensor
        [HttpGet]
        public string GetStatus()
        {
            return Service.MQTT.Instance.Status;
        }

        // POST: api/Sensor
        [HttpPost]
        public async Task<string> ReadStringDataManual()
        {
            using (StreamReader reader = new StreamReader(Request.Body, Encoding.UTF8))
            {
                Console.WriteLine(reader.ReadToEndAsync().Result);
                return await reader.ReadToEndAsync();
            }
        }
    }
}
