using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using Newtonsoft.Json;
using System.Security.Cryptography;

namespace ClientSnakeNetwork
{
    public partial class clientJoinGame : Form
    {
        public Connection connectionServeur { get; set; }
        public string password { get; private set; }
        public string[] lobby { private get; set; }
        public string selectedLobby { get; private set; }

        public clientJoinGame()
        {
            InitializeComponent();
        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (comboBox1.SelectedIndex > -1)
            {
                selectedLobby = comboBox1.Items[comboBox1.SelectedIndex].ToString();
                if (!String.IsNullOrEmpty(textBox1.Text))
                {
                    byte[] encData_byte = new byte[textBox1.Text.Length];
                    encData_byte = Encoding.UTF32.GetBytes(textBox1.Text);
                    string encodedData = Convert.ToBase64String(encData_byte);
                    password = encodedData;
                }
                this.DialogResult = DialogResult.OK;
            }
        }

        private void clientJoinGame_Load(object sender, EventArgs e)
        {
            comboBox1.DataSource = null;
            comboBox1.DataSource = lobby;
            this.MaximumSize = this.MinimumSize = this.Size;
        }

        private void button2_Click(object sender, EventArgs e)
        {
            lobby = JsonConvert.DeserializeObject<string[]>(connectionServeur.SendReceiveObject<bool, string>("needAllLobby", "AllLobby", 1000, true));
            comboBox1.DataSource = null;
            comboBox1.DataSource = lobby;
        }
    }
}
