using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Threading.Tasks;
using Newtonsoft.Json.Linq;

namespace Altantis_DeviceSensorEP.Mapper
{
    public static class MapperRawData
    {
        public static Business.RawData DAOToBusiness(DAO.RawData dao)
        {
            try
            {
                var jo = JObject.Parse(dao.Data);

                DateTime dt = DateTime.ParseExact(jo["metricDate"].ToString(), "yyyy-MM-dd HH:mm:ss", null);
                Double d = Double.Parse(jo["metricValue"].ToString(), CultureInfo.CreateSpecificCulture("en-EN"));

                return new Business.RawData(jo["macAddress"].ToString(), dt, jo["deviceType"].ToString(), jo["name"].ToString(), d);
            }
            catch {return null;}
        }
    }
}
