using System;
using System.Windows.Forms;

namespace ClientForm
{
    public partial class Form1 : Form
    {
        private VelibSOAP.VelibIWServiceClient client = new VelibSOAP.VelibIWServiceClient();

        public Form1()
        {
            InitializeComponent();

            comboBox2.Items.Clear();
            AsyncInterface.GetVillesAsync(client, comboBox2);
        }

        private void comboBox2_SelectedIndexChanged(object sender, EventArgs e)
        {
            comboBox1.Items.Clear();
            comboBox1.Text = String.Empty;
            label2.Text = "";
            AsyncInterface.GetStationsAsync(client, comboBox2.SelectedItem.ToString(), comboBox1);
           // comboBox1.Items.AddRange(client.GetStations(comboBox2.SelectedItem.ToString()).ToArray());
        }

        private void comboBox1_SelectedIndexChanged(object sender, EventArgs e)
        {
            label2.Text = "";
        }

        private void button1_Click(object sender, EventArgs e)
        {
            //label2.Text = client.GetNbVelib(comboBox1.SelectedItem.ToString());
            AsyncInterface.GetNbVelibAsync(client, comboBox1.SelectedItem.ToString(), label2);
        }
    }
}
