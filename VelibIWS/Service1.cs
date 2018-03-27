using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.IO;
using System.Net;
using System.Threading;

namespace VelibIWS
{
    // REMARQUE : vous pouvez utiliser la commande Renommer du menu Refactoriser pour changer le nom de classe "Service1" à la fois dans le code et le fichier de configuration.
    public class VelibIWService : IVelibIWService
    {
        public const int DelayMilliseconds = 10000;

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
            station = station.ToLower();
            foreach(JObject item in stations)
            {
                string name = ((string)item.SelectToken("name")).ToLower();
                if (name.Contains(station))
                    return (string)(item.SelectToken("available_bikes"));
            }
            return "";
        }

        public List<string> GetNbVelibInStations(string stationsName)
        {
            stationsName = stationsName.ToLower();
            List<string> res = new List<string>();
            foreach(JObject item in stations)
            {
                string name = ((string)item.SelectToken("name")).ToLower();
                if (name.Contains(stationsName))
                {
                    res.Add((string)(item.SelectToken("name")));
                    res.Add((string)(item.SelectToken("available_bikes")));
                }

            }
            return res;
        }

        public List<string> GetStations(string ville)
        {
            List<string> res = new List<string>();
            ville = ville.ToLower();
            foreach(JObject station in stations)
            {
                string name = ((string)station.SelectToken("contract_name")).ToLower();
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

        public IAsyncResult BeginGetVilles(AsyncCallback callback, object state)
        {
            var asyncResult = new SimpleAsyncResult<List<string>>(state);

            // mimic a long running operation
            var timer = new System.Timers.Timer(DelayMilliseconds);
            timer.Elapsed += (_, args) =>
            {
                asyncResult.Result = GetVilles();
                asyncResult.IsCompleted = true;
                callback(asyncResult);
                timer.Enabled = false;
                timer.Close();
            };
            timer.Enabled = true;
            return asyncResult;
        }

        public List<string> EndGetVilles(IAsyncResult asyncResult)
        {
            return ((SimpleAsyncResult<List<string>>)asyncResult).Result;
        }

        public IAsyncResult BeginGetStations(string ville, AsyncCallback callback, object state)
        {
            var asyncResult = new SimpleAsyncResult<List<string>>(state);

            // mimic a long running operation
            var timer = new System.Timers.Timer(DelayMilliseconds);
            timer.Elapsed += (_, args) =>
            {
                asyncResult.Result = GetStations(ville);
                asyncResult.IsCompleted = true;
                callback(asyncResult);
                timer.Enabled = false;
                timer.Close();
            };
            timer.Enabled = true;
            return asyncResult;
        }

        public List<string> EndGetStations(IAsyncResult asyncResult)
        {
            return ((SimpleAsyncResult<List<string>>)asyncResult).Result;
        }

        public IAsyncResult BeginGetNbVelib(string station, AsyncCallback callback, object state)
        {
            var asyncResult = new SimpleAsyncResult<string>(state);

            // mimic a long running operation
            var timer = new System.Timers.Timer(DelayMilliseconds);
            timer.Elapsed += (_, args) =>
            {
                asyncResult.Result = GetNbVelib(station);
                asyncResult.IsCompleted = true;
                callback(asyncResult);
                timer.Enabled = false;
                timer.Close();
            };
            timer.Enabled = true;
            return asyncResult;
        }

        public string EndGetNbVelib(IAsyncResult asyncResult)
        {
            return ((SimpleAsyncResult<string>)asyncResult).Result;
        }

        public IAsyncResult BeginGetNbVelibInStations(string stationsName, AsyncCallback callback, object state)
        {
            var asyncResult = new SimpleAsyncResult<List<string>>(state);

            // mimic a long running operation
            var timer = new System.Timers.Timer(DelayMilliseconds);
            timer.Elapsed += (_, args) =>
            {
                asyncResult.Result = GetNbVelibInStations(stationsName);
                asyncResult.IsCompleted = true;
                callback(asyncResult);
                timer.Enabled = false;
                timer.Close();
            };
            timer.Enabled = true;
            return asyncResult;
        }

        public List<string> EndGetNbVelibInStations(IAsyncResult asyncResult)
        {
            return ((SimpleAsyncResult<List<string>>)asyncResult).Result;
        }



        public class SimpleAsyncResult<T> : IAsyncResult
        {
            private readonly object accessLock = new object();
            private bool isCompleted = false;
            private T result;

            public SimpleAsyncResult(object asyncState)
            {
                AsyncState = asyncState;
            }

            public T Result
            {
                get
                {
                    lock (accessLock)
                    {
                        return result;
                    }
                }
                set
                {
                    lock (accessLock)
                    {
                        result = value;
                    }
                }
            }

            public bool IsCompleted
            {
                get
                {
                    lock (accessLock)
                    {
                        return isCompleted;
                    }
                }
                set
                {
                    lock (accessLock)
                    {
                        isCompleted = value;
                    }
                }
            }

            // WCF seems to use the async callback rather than checking the wait handle
            // so we can safely return null here.
            public WaitHandle AsyncWaitHandle { get { return null; } }

            // We will always be doing an async operation so completed synchronously should always
            // be false.
            public bool CompletedSynchronously { get { return false; } }

            public object AsyncState { get; private set; }

            WaitHandle IAsyncResult.AsyncWaitHandle => throw new NotImplementedException();
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
