using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ClientConsole
{
    class AsyncInterface
    {
        public static async Task<string[]> GetVillesAsync(VelibSOAP.VelibIWServiceClient client)
        {
            return await client.GetVillesAsync();
        }

        public static async Task<string[]> GetStationsAsync(VelibSOAP.VelibIWServiceClient client, string ville)
        {
            return await client.GetStationsAsync(ville);
        }

        public static async Task<string> GetNbVelib(VelibSOAP.VelibIWServiceClient client, string station)
        {
            return await client.GetNbVelibAsync(station);
        }

        public static async Task<string[]> GetNbVelibInStations(VelibSOAP.VelibIWServiceClient client, string stationsName)
        {
            return await client.GetNbVelibInStationsAsync(stationsName);
        }
    }
}
