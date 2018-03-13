using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;

namespace VelibIWS
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" à la fois dans le code et le fichier de configuration.
    public class VelibIWService : IVelibIWService
    {

        JArray villes;
        JArray stations;

        public VelibIWService()
        {
            WebRequest villesReq = WebRequest.Create("https://api.jcdecaux.com/vls/v1/contracts?apiKey=bf1b9470e4532b2bcf928ac1dc1ecaed8b38de11");
            WebRequest stationsReq = WebRequest.Create("https://api.jcdecaux.com/vls/v1/stations?apiKey=bf1b9470e4532b2bcf928ac1dc1ecaed8b38de11");

            WebResponse response = villesReq.GetResponse();

            Stream stream = response.GetResponseStream();
            StreamReader reader = new StreamReader(stream);

            string str = reader.ReadToEnd();

            reader.Close();
            response.Close();

            villes = JArray.Parse(str);

            response = stationsReq.GetResponse();

            stream = response.GetResponseStream();
            reader = new StreamReader(stream);

            str = reader.ReadToEnd();

            reader.Close();
            response.Close();

            stations = JArray.Parse(str);
        }

        public string GetNbVelib(string station)
        {
            foreach(JObject item in stations)
            {
                string name = ((string)item.SelectToken("name"));
                if (name.Contains(station))
                    return (string)(item.SelectToken("available_bikes"));
            }
            return "";
        }

        public List<string> GetStations(string ville)
        {
            List<string> res = new List<string>();
            foreach(JObject station in stations)
            {
                string name = ((string)station.SelectToken("contract_name"));
                if (name.Contains(ville))
                    res.Add((string)(station.SelectToken("name")));
            }

            return res;
        }

        public List<string> GetVilles()
        {
            List<string> res = new List<string>();

            foreach(JObject ville in villes)
            {
                res.Add((string)(ville.SelectToken("name")));
            }
            return res;
        }
        /*
        public string GetData(int value)
        {
            return string.Format("You entered: {0}", value);
        }

        public CompositeType GetDataUsingDataContract(CompositeType composite)
        {
            if (composite == null)
            {
                throw new ArgumentNullException("composite");
            }
            if (composite.BoolValue)
            {
                composite.StringValue += "Suffix";
            }
            return composite;
        }
    }*/
    }
}
