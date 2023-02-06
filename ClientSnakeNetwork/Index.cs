using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DuchosalN;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;
using NetworkCommsDotNet.Connections.TCP;
using NetworkCommsDotNet.Connections.UDP;
using System.Net.NetworkInformation;
using System.IO;
using System.Net;
using System.Net.Sockets;
using Newtonsoft.Json;

namespace ClientSnakeNetwork
{
    public partial class Index : Form
    {
        NetworkSnake client = new NetworkSnake();

        Connection serverConnection;

        List<Color> listeCouleurSerpent = new List<Color>() { Color.Blue, Color.Green, Color.Black, Color.Cyan, Color.Magenta, Color.YellowGreen, Color.BlueViolet };
        string pathServeurIP = @".\ipServeur.txt";

        public Index()
        {
            InitializeComponent();
            //button2_Click(this, EventArgs.Empty);
            this.MinimumSize = new Size(this.Width, this.Height);
            this.MaximumSize = new Size(this.Width, this.Height);
           
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            cbxCouleurSerpent.DropDownStyle = ComboBoxStyle.DropDownList;
            cbxCouleurSerpent.Items.Add("Aleatoire");
            foreach (Color c in listeCouleurSerpent)
            {
                cbxCouleurSerpent.Items.Add(c.Name);
            }
            cbxCouleurSerpent.SelectedIndex = 0;

            if (File.Exists(pathServeurIP))
            {
                string file = File.ReadAllText(pathServeurIP);
                if (ValidateIPv4(file) == 0)
                {
                    fillAdresseTextBox(file.Split('.'));
                    button1_Click(null, null);
                }
            }
        }

        /// <summary>
        /// Test si l'adresse Entré dans les Textbox est celle d'un serveur 
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button1_Click(object sender, EventArgs e)
        {
            string adresseServer = "";
            bool ipIsValid = false;
            button1.Enabled = false;
            button2.Enabled = false;

            adresseServer = first.Text + "." + second.Text + "." + third.Text + "." + fourth.Text;
            adresseServer = adresseServer.Trim();
            adresseServer = adresseServer.Replace(" ", "");

            switch (ValidateIPv4(adresseServer))
            {
                case 0:
                    ConnectionInfo machineInfo = new ConnectionInfo(adresseServer, 50001);
                    try
                    {
                        if (!File.Exists(pathServeurIP))
                        {
                            File.WriteAllText(pathServeurIP, adresseServer);
                        }
                        serverConnection = TCPConnection.GetConnection(machineInfo);

                        serverConnection.SendObject("Iexist", client.getHostIP().ToString());
                        serverConnection.AppendIncomingPacketHandler<bool>("IseeU", serverSeeMe);
                        
                        ipIsValid = true;
                    }
                    catch(ConnectionSetupException ex)
                    {
                        lblError.Text = "Impossible de se connecter au serveur réessayer";
                        lblError.ForeColor = Color.Red;
                        lblError.Visible = true;
                    }
                    break;
                case -1:
                    lblError.Visible = true;
                    lblError.Text = "Veuillez Entrer une adresse IP";
                    lblError.ForeColor = Color.Red;
                    break;
                case -2:
                    lblError.Visible = true;
                    lblError.Text = "Adresse IP incomplète";
                    lblError.ForeColor = Color.Red;
                    break;
                case -3:
                    lblError.Visible = true;
                    lblError.Text = "Adresse IP non valide";
                    lblError.ForeColor = Color.Red;
                    break;
            }
            if (!ipIsValid)
            {
                button1.Enabled = true;
                button2.Enabled = true;
            }
        }

        /// <summary>
        /// Entre automatiquement l'adresse de Loopback
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void button2_Click(object sender, EventArgs e)
        {
            button2.Enabled = false;
            button1.Enabled = false;
            fillAdresseTextBox(new string[] { "127", "0", "0", "1" });
            button1_Click(sender, e);
        }

        private void btnCreerPartie_Click(object sender, EventArgs e)
        {
            clientNewGame client = new clientNewGame();
            if (client.ShowDialog() == DialogResult.OK)
            {
                string[] game = new string[] { client.gameName, client.password, client.nombreRoundJoue.ToString(), client.iVitesse.ToString(), client.ScoreMax.ToString() };
                string gameSerialise = JsonConvert.SerializeObject(game);
                bool nameExiste = serverConnection.SendReceiveObject<string, bool>("createANewLobby", "youCanCreate", 1000, gameSerialise);

                if (!nameExiste)
                {
                    showColorBox();
                    btnCreerPartie.Visible = false;
                    btnRejoindreGame.Visible = false;
                    btnLauchGame.Visible = true;
                    if (lblFalsePass.Visible == true)
                    {
                        lblFalsePass.Visible = false;
                    }
                    serverConnection.AppendIncomingPacketHandler<int[]>("GameStart", openGameForm);
                }
                else
                {
                    this.MaximumSize = this.MinimumSize = new Size(this.Width, (this.Height - this.ClientSize.Height) + lblFalsePass.Location.Y + lblFalsePass.Height + 12);
                    lblFalsePass.Text = "Le nom de la partie existe de deja";
                    lblFalsePass.Visible = true;
                }
                //Envoie Message -> ouvre nouveau serveur
            }
        }

        private void btnRejoindreGame_Click(object sender, EventArgs e)
        {
            string listLobby = serverConnection.SendReceiveObject<bool, string>("needAllLobby", "AllLobby",1000, true);
            string[] allLobby = JsonConvert.DeserializeObject<string[]>(listLobby);
            clientJoinGame client = new clientJoinGame();
            client.connectionServeur = serverConnection;
            client.lobby = allLobby;
            if (client.ShowDialog() == DialogResult.OK)
            {
                string infoConnection = JsonConvert.SerializeObject(new string[] { client.selectedLobby, client.password });
                bool canConnect = serverConnection.SendReceiveObject<string, bool>("joinLobby", "canConnect", 1000, infoConnection);
                if (canConnect)
                {
                    showColorBox();
                    btnCreerPartie.Enabled = false;
                    btnRejoindreGame.Enabled = false;
                    if (lblFalsePass.Visible == true)
                    {
                        lblFalsePass.Visible = false;
                    }
                    serverConnection.AppendIncomingPacketHandler<int[]>("GameStart", openGameForm);
                }
                else
                {
                    this.MaximumSize = this.MinimumSize = new Size(this.Width, (this.Height - this.ClientSize.Height) + lblFalsePass.Location.Y + lblFalsePass.Height + 12);
                    lblFalsePass.Text = "Mot de passe incorect";
                    lblFalsePass.Visible = true;
                }
            }
        }

        private void btnLauchGame_Click(object sender, EventArgs e)
        {
            serverConnection.SendObject("gameCanStart", true);
            // Demarre la partie sur le serveur
        }


        /// <summary>
        /// change le text de la combobox de couleur en fonction de la couleur séléctionner
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxCouleurSerpent_SelectedIndexChanged(object sender, EventArgs e)
        {
            if (cbxCouleurSerpent.SelectedIndex == 0)
            {
                cbxCouleurSerpent.ForeColor = Color.Black;
            }
            else
            {
                cbxCouleurSerpent.ForeColor = listeCouleurSerpent[cbxCouleurSerpent.SelectedIndex - 1];
            }
        }

        /// <summary>
        /// change la couleur du text a l'intérieur d'une combobox
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void cbxCouleurSerpent_DrawItem(object sender, DrawItemEventArgs e)
        {
            SolidBrush brush;
            // Draw the background 
            //e.DrawBackground();

            // Get the item text    
            string text = ((ComboBox)sender).Items[e.Index].ToString();

            // Determine the forecolor based on whether or not the item is selected
            if (e.Index == 0)
            {
                brush = new SolidBrush(Color.Black);
            }
            else
            {
                brush = new SolidBrush(listeCouleurSerpent[e.Index - 1]);
            }

            // Draw the text    
            e.Graphics.DrawString(text, e.Font, brush, e.Bounds);
            e.DrawFocusRectangle();
        }

        /// <summary>
        /// Méthode active apres la réponse du serveur
        /// pour lui indiquer que la partie va commencer
        /// </summary>
        private void openGameForm(PacketHeader packetHeader, Connection connection, int[] numero)
        {
            //Une fois que l'administrateur du jeu decide que la partie commence
            //Chaque client recoit ce message et recupere l'adresse IP du server
            IPAddress serverIP = IPAddress.Parse(connection.ConnectionInfo.RemoteEndPoint.ToString().Split(':')[0]);
            this.Invoke(new MethodInvoker(delegate
            {
                //ouvre le form qui contient le jeux
                this.Hide();
                cbxCouleurSerpent.Enabled = false;

                SnakeForm snakeForm = new SnakeForm();

                snakeForm.serveurConnection = this.serverConnection;
                snakeForm.lastForm = this;
                snakeForm.numero = numero[0];
                snakeForm.Text = "SnakeForm" + numero[0];

                snakeForm.Show();
                snakeForm.openInfoForm(cbxCouleurSerpent.SelectedIndex == 0 ? 0 : listeCouleurSerpent[cbxCouleurSerpent.SelectedIndex - 1].ToArgb(), numero[1]);
                connection.RemoveIncomingPacketHandler("GameStart");
            }));
        }

        /// <summary>
        /// Le server confirme avoir recu la connection
        /// </summary>
        private void serverSeeMe(PacketHeader packetHeader, Connection connection, bool heSeeMe)
        {
            lblError.Invoke(new MethodInvoker(delegate
            {
                lblError.Text = "Bienvenue dans le Lobby";
                lblError.ForeColor = Color.Green;
                lblError.Visible = true;
                pnlAdresseIP.Enabled = false;
                btnCreerPartie.Visible = true;
                btnRejoindreGame.Visible = true;
            }));
            connection.RemoveIncomingPacketHandler("IseeU");
        }

        private void showColorBox()
        {
            cbxCouleurSerpent.Visible = true;
            this.MaximumSize = this.MinimumSize = new Size(this.Width, cbxCouleurSerpent.Location.X + cbxCouleurSerpent.Height);
            lblColor.Visible = true;
        }

        public void gameIsAbleToStart()
        {
            serverConnection.AppendIncomingPacketHandler<int[]>("GameStart", openGameForm);
        }

        public void wantToReplay()
        {
            serverConnection.SendObject("Iexist", client.getHostIP().ToString());
            serverConnection.AppendIncomingPacketHandler<bool>("IseeU", serverSeeMe);
            this.Size = this.MaximumSize = this.MinimumSize = new Size(this.Width, this.Height - this.ClientSize.Height + btnCreerPartie.Location.Y + btnCreerPartie.Height + 12);
            btnCreerPartie.Enabled = true;
            btnRejoindreGame.Enabled = true;
            btnLauchGame.Visible = false;
            cbxCouleurSerpent.Visible = false;
            cbxCouleurSerpent.Enabled = true;
            lblColor.Visible = false;
        }

        private void serverStop()
        {
            serverConnection.AppendIncomingPacketHandler<bool>("turnOff", (packetHeader, connection, severDoIt) =>
            {
                this.Invoke(new MethodInvoker(delegate
                {
                    this.Close();
                }));
            });
        }

        /// <summary>
        /// Remplie les textBox qui contienent les adresses IP
        /// </summary>
        /// <param name="adresse">tableau contenant une adressIP séparer</param>
        public void fillAdresseTextBox(string[] adresse)
        {
            first.Text = adresse[0];
            second.Text = adresse[1];
            third.Text = adresse[2];
            fourth.Text = adresse[3];
        }

        /// <summary>
        /// Vérifie que l'adresse IP soit une vrai adresse IP
        /// </summary>
        /// <param name="ipString"></param>
        /// <returns></returns>
        public int ValidateIPv4(string ipString)
        {
            if (String.IsNullOrWhiteSpace(ipString) || ipString == "...")
            {
                return -1;
            }

            string[] splitValues = ipString.Split('.');
            foreach (string Values in splitValues)
            {
                if (String.IsNullOrWhiteSpace(Values))
                {
                    return -2;
                }
            }

            byte tempForParsing;
            return splitValues.All(r => byte.TryParse(r, out tempForParsing)) ? 0 : -3;
        }

        #region TextBox AdresseIP

        //Fonctionnement:
        //4 MaskedTextBox dans un panel
        //Lorsque le mask est complet -> Va dans la TextBox suivante
        //Si le le point est appuyé -> va directement dans la Textbox suivante
        //Si la fleche gauche ou droite est appuye -> va dans la TextBox suivante/precedante
        //Si le bouton supprimé est appuyé dans la derniere case -> supprime le caractère de la textbox suivante
        //On peut copier-coller des adresses IP complète

        bool bfirst, bsecond, bthird, bfourth;
        string strfirst, strSecond, strThird, strfourth;
        string clipBoard = "";
        bool bClipBoard = false;

        private void first_KeyDown(object sender, KeyEventArgs e)
        {
            MaskedTextBox text = sender as MaskedTextBox;
            strSecond = second.Text;
            if (first.SelectionStart == first.Mask.Length && e.KeyCode == Keys.Right)
            {
                second.Focus();
            }
            cutIpAdresse(text, e);
        }

        private void first_KeyUp(object sender, KeyEventArgs e)
        {
            if (first.Text != strfirst)
            {
                bfirst = true;
            }
            if (first.MaskFull && bfirst || first.Focused && e.KeyCode == Keys.OemPeriod && first.Text.Length >= 1)
            {
                if (e.KeyCode != Keys.Delete && e.KeyCode != Keys.Back)
                {
                    second.Focus();
                    bfirst = false;
                    strfirst = first.Text;
                }
            }
            if (bClipBoard)
            {
                bClipBoard = false;
                Clipboard.SetText(clipBoard);
            }
        }

        private void second_KeyDown(object sender, KeyEventArgs e)
        {
            MaskedTextBox text = sender as MaskedTextBox;
            strSecond = second.Text;
            if (text.SelectionStart == 0)
            {
                if (e.KeyCode == Keys.Left)
                {
                    first.Focus();
                }
                if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                {
                    if (first.Text.Length > 0)
                    {
                        first.Text = first.Text.Remove(first.Text.Length - 1, 1);
                    }
                    first.Focus();
                }
            }
            if (second.SelectionStart == second.Mask.Length && e.KeyCode == Keys.Right)
            {
                third.Focus();
            }
            cutIpAdresse(text, e);
        }

        private void second_KeyUp(object sender, KeyEventArgs e)
        {
            if (second.Text != strSecond)
            {
                bsecond = true;
            }
            if (second.MaskFull && bsecond || second.Focused && e.KeyCode == Keys.OemPeriod && second.Text.Length >= 1)
            {
                if (e.KeyCode != Keys.Delete && e.KeyCode != Keys.Back)
                {
                    strSecond = second.Text;
                    third.Focus();
                    bsecond = false;
                }
            }
            if (bClipBoard)
            {
                bClipBoard = false;
                Clipboard.SetText(clipBoard);
            }
        }

        private void third_KeyDown(object sender, KeyEventArgs e)
        {
            MaskedTextBox text = sender as MaskedTextBox;
            strThird = third.Text;
            if (text.SelectionStart == 0)
            {
                if (e.KeyCode == Keys.Left)
                {
                    second.Focus();
                }
                if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                {
                    if (second.Text.Length > 0)
                    {
                        second.Text = second.Text.Remove(second.Text.Length - 1, 1);
                    }
                    second.Focus();
                }
            }
            if (text.SelectionStart == text.Mask.Length && e.KeyCode == Keys.Right)
            {
                fourth.Focus();
            }
            cutIpAdresse(text, e);
        }

        private void third_KeyUp(object sender, KeyEventArgs e)
        {
            if (third.Text != strThird)
            {
                bthird = true;
            }
            if (third.MaskFull && bthird || third.Focused && e.KeyCode == Keys.OemPeriod && third.Text.Length >= 1)
            {
                if (e.KeyCode != Keys.Delete && e.KeyCode != Keys.Back)
                {
                    fourth.Focus();
                    bthird = false;
                    strThird = third.Text;
                }
            }
            if (bClipBoard)
            {
                bClipBoard = false;
                Clipboard.SetText(clipBoard);
            }
        }

        private void fourth_KeyDown(object sender, KeyEventArgs e)
        {
            MaskedTextBox text = sender as MaskedTextBox;
            strfourth = fourth.Text;
            if (text.SelectionStart == 0)
            {
                if (e.KeyCode == Keys.Left)
                {
                    third.Focus();
                }
                if (e.KeyCode == Keys.Delete || e.KeyCode == Keys.Back)
                {
                    if (third.Text.Length > 0)
                    {
                        third.Text = third.Text.Remove(third.Text.Length - 1, 1);
                    }
                    third.Focus();
                }
            }
            if (fourth.SelectionStart == 0 && e.KeyCode == Keys.Left)
            {
                third.Focus();
            }
            cutIpAdresse(text, e);
        }

        private void fourth_KeyUp(object sender, KeyEventArgs e)
        {
            if (fourth.Text != strfourth)
            {
                bfourth = true;
            }
            if (fourth.MaskFull && bfourth || fourth.Focused && e.KeyCode == Keys.OemPeriod && fourth.Text.Length >= 1)
            {
                if (e.KeyCode != Keys.Delete && e.KeyCode != Keys.Back)
                {
                    button1.Focus();
                    bfourth = false;
                    strfourth = fourth.Text;
                }
            }
            if (bClipBoard)
            {
                bClipBoard = false;
                Clipboard.SetText(clipBoard);
            }
        }

        public void cutIpAdresse(MaskedTextBox text, KeyEventArgs e)
        {
            if (e.Control && e.KeyCode == Keys.V)
            {
                if (Clipboard.ContainsText())
                {
                    if (ValidateIPv4(Clipboard.GetText()) == 0)
                    {
                        fillAdresseTextBox(Clipboard.GetText().Split('.'));
                        clipBoard = Clipboard.GetText();
                        bClipBoard = true;
                        Clipboard.Clear();
                    }
                    else
                    {
                        text.Text = Clipboard.GetText();
                        clipBoard = Clipboard.GetText();
                        bClipBoard = true;
                        Clipboard.Clear();
                    }
                }
            }
        }

        #endregion
    }
}

