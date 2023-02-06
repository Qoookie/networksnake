using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ClientSnakeNetwork
{
    public partial class clientNewGame : Form
    {
        public string gameName { get; private set; }
        public string password { get; private set; }
        public int ScoreMax { get; private set; }
        public int iVitesse { get; private set; }
        public int nombreRoundJoue { get; private set; }

        int nudTemp;

        public clientNewGame()
        {
            InitializeComponent();
            nudTemp = (int)nudRound.Value;
        }

        private void clientNewGame_Load(object sender, EventArgs e)
        {
            tbxNomPartie.Text = Environment.UserName + " game's";
            this.MaximumSize = this.MinimumSize = this.Size;
        }

        /// <summary>
        /// Les paramètres définis par l'utilisateur sur le serveur sont modifiés ici
        /// </summary>
        private void setGameParameter()
        {
            if (rbtnPoint50.Checked)
            {
                ScoreMax = 50;
            }
            if (rbtnPointMax.Checked)
            {
                ScoreMax = -1;
            }
            if (rbtnVitesse1.Checked)
            {
                iVitesse = 80;
            }
            if (rbtnVitesse2.Checked)
            {
                iVitesse = 40;
            }
            if (rbtnVitesse3.Checked)
            {
                iVitesse = 10;
            }
            nombreRoundJoue = (int)nudRound.Value;

        }

        private void button1_Click(object sender, EventArgs e)
        {
            if (!string.IsNullOrEmpty(tbxNomPartie.Text))
            {
                gameName = tbxNomPartie.Text;
                setGameParameter();
                if (!string.IsNullOrEmpty(tbxMotDePass.Text))
                {
                    byte[] encData_byte = new byte[tbxMotDePass.Text.Length];
                    encData_byte = Encoding.UTF32.GetBytes(tbxMotDePass.Text);
                    string encodedData = Convert.ToBase64String(encData_byte);
                    password = encodedData;
                }
                this.DialogResult = DialogResult.OK;
            }
        }

        private void rbtnPointMax_CheckedChanged(object sender, EventArgs e)
        {
            RadioButton check = sender as RadioButton;
            if (check.Checked == true)
            {
                nudTemp = (int)nudRound.Value;
                nudRound.Value = 1;
                nudRound.Enabled = false;
            }
            if (check.Checked == false)
            {
                nudRound.Value = nudTemp;
                nudRound.Enabled = true;
            }
        }
    }
}
