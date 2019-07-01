using Microsoft.Extensions.Configuration;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using uPLibrary.Networking.M2Mqtt;
using uPLibrary.Networking.M2Mqtt.Messages;
using System.Net.Http;

namespace Altantis_DeviceSensorEP.Service
{
    public class MQTT
    {
        private static readonly Lazy<MQTT> _lazy = new Lazy<MQTT>(() => new MQTT());
        public static MQTT Instance { get { return _lazy.Value; } }

        public string BrokerAddress { get; set; }
        public string TopicAddress { get; set; }
        public string UserName { get; set; }
        public string UserPassword { get; set; }
        public int ConnectionPort { get; set; }
        public string RawDataDetination { get; set; }
        public string CalculationEngine { get; set; }

        public MqttClient Client { get; set; }
        public string ClientId { get; set; }

        private static readonly HttpClient HTTPClient= new HttpClient();

        public string Status { get; set; }

        private MQTT()
        {
            LoadConfig();
            if (Status == "") StartMQTT();
        }

        private void LoadConfig()
        {
            Status = "";
            try
            {
                BrokerAddress = "";
                TopicAddress = "";
                IConfigurationRoot configuration = new ConfigurationBuilder()
                    .SetBasePath(AppDomain.CurrentDomain.BaseDirectory)
                    .AddJsonFile("config.json")
                    .Build();
                BrokerAddress = configuration.GetConnectionString("BrokerAddress");
                TopicAddress = configuration.GetConnectionString("TopicAddress");
                RawDataDetination = configuration.GetConnectionString("RawDataDetination");
                CalculationEngine = configuration.GetConnectionString("CalculationEngine");
                UserName = configuration.GetConnectionString("UserName");
                UserPassword = configuration.GetConnectionString("UserPassword");
                ConnectionPort = Convert.ToInt32(configuration.GetConnectionString("ConnectionPort"));
            }
            catch { Status = "🔴 - EndPoint Down - Config file Error"; }
        }

        private void StartMQTT()
        {
            try
            {
                //Initialize Client
                Client = new MqttClient(BrokerAddress, ConnectionPort, true, null, null, MqttSslProtocols.TLSv1_2);
                Client.MqttMsgPublishReceived += Client_MqttMsgPublishReceived;
                ClientId = Guid.NewGuid().ToString();
                Client.Connect(ClientId, UserName, UserPassword);

                //Subscibe
                if (BrokerAddress != "" & TopicAddress != "") Client.Subscribe(new string[] { TopicAddress }, new byte[] { 2 });

                Status = "🔵 - EndPoint Up - Connected to " + BrokerAddress + TopicAddress;
            }
            catch { Status = "🔴 - EndPoint Down - MQTT connection Error "; }
        }

        private void Client_MqttMsgPublishReceived(object sender, MqttMsgPublishEventArgs e)
        {
            string mess = Encoding.UTF8.GetString(e.Message);
            Business.RawData temp = Mapper.MapperRawData.DAOToBusiness(new DAO.RawData(mess));

            if(temp != null)
            {
                string json = Newtonsoft.Json.JsonConvert.SerializeObject(temp);
                Console.WriteLine(json);
                StringContent stringContent = new StringContent(json, UnicodeEncoding.UTF8, "application/json");

                try { HTTPClient.PostAsync(RawDataDetination, stringContent); } catch { }
                try { HTTPClient.PostAsync(CalculationEngine, stringContent); } catch { }
            }
        }
    }
}