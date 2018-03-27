using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientForm
{
    class AsyncInterface
    {
        public static async Task GetVillesAsync(VelibSOAP.VelibIWServiceClient client, ComboBox c)
        {
            string[] res = await client.GetVillesAsync();

            c.Items.AddRange(res);
        }

        public static async Task GetStationsAsync(VelibSOAP.VelibIWServiceClient client, string ville, ComboBox c)
        {
            string[] res = await client.GetStationsAsync(ville);

            c.Items.AddRange(res);
        }

        public static async Task GetNbVelibAsync(VelibSOAP.VelibIWServiceClient client, string station, Label l)
        {
            string res = await client.GetNbVelibAsync(station);

            l.Text = res;
        }
    }
}
