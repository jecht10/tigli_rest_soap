using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Runtime.Serialization;
using System.ServiceModel;
using System.Text;

namespace WcfServiceLibrary1
{
    [ServiceBehavior(InstanceContextMode = InstanceContextMode.PerCall)]
    public class EventService : IEventService
    {
        private static readonly string API_KEY = "5adfd2b9c75749261a8f2640290caad53559518a";
        public const int DelayMilliseconds = 10000;

        static Action<int, string, string> m_Subscribe = delegate { };
        static Action m_SubscribeFinished = delegate { };

        public void Subscribe(string city, string station, int time)
        {
            int nbVelib = getAvailableBikesService(city, station);
            m_Subscribe(nbVelib, city, station);
            m_SubscribeFinished();
        }

        public void SubscribeEvent()
        {
            IEventServiceEvents subscriber = OperationContext.Current.GetCallbackChannel<IEventServiceEvents>();
            m_Subscribe += subscriber.Subscribe;
        }

        public void SubscribeFinishedEvent()
        {
            IEventServiceEvents subscriber = OperationContext.Current.GetCallbackChannel<IEventServiceEvents>();
            m_SubscribeFinished += subscriber.SubscribeFinished;
        }

        private int getAvailableBikesService(string city, string station)
        {
            try
            {
                WebRequest requete = WebRequest.Create("https://api.jcdecaux.com/vls/v1/stations?contract=" + city + "&apiKey=" + API_KEY);
                WebResponse reponse = requete.GetResponse();
                Stream stream = reponse.GetResponseStream();

                StreamReader reader = new StreamReader(stream);
                string json = reader.ReadToEnd();

                if (reponse == null || station == null)
                {
                    return -1;
                }

                reponse.Close();
                reader.Close();

                JArray jsonArray = JArray.Parse(json);

                foreach (JObject item in jsonArray)
                {
                    string name = (string)item.SelectToken("name");

                    if (name.Contains(station))
                    {
                        return (int)item.SelectToken("available_bikes");
                    }
                }
            }

            catch (Exception e)
            {

            }

            return -1;
        }
    }
}
