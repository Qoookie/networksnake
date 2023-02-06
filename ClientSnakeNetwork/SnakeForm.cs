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
    public partial class SnakeForm : Form
    {
        private const int pieceSize = 20;

        //Passez par le Form1
        public int numero;
        public Form lastForm;
        public Connection serveurConnection;
        Monde ancienMonde;
        minimap miniForm;

        bool gameIsEnded = false;

        //recupere les paramètrages IP de la machine
        NetworkSnake client = new NetworkSnake();

        bool serveurClose = true;

        Label lblpomme;
        TextBox lbxNumPost, lbxScore, lbxPommePost;
        TextBox lbxEmpoisonne = new TextBox();

        public SnakeForm()
        {
            InitializeComponent();
            //Si false -> afficher beaucoup de label fait clignoter
            DoubleBuffered = true;
        }

        private void SnakeForm_Paint(object sender, PaintEventArgs e)
        {
            //Dessine une ligne pour marquer la séparation des deux formulaire
            Pen blackPen = new Pen(Color.Black, 2);

            PointF point1 = new PointF(0, 501);
            PointF point2 = new PointF(this.Width, 501);

            e.Graphics.DrawLine(blackPen, point1, point2);
        }

        /// <summary>
        /// Se lance si un round se fini mais pas la partie
        /// Permet au formulaire de départ de se relancer
        /// </summary>
        /// <param name="packetHeader"></param>
        /// <param name="connection"></param>
        /// <param name="time"></param>
        private void roundEnd(PacketHeader packetHeader, Connection connection, int time)
        {
            ancienMonde = null;
            serveurClose = false;
            miniForm.Invoke(new MethodInvoker(delegate
            {
                miniForm.Dispose();
            }));
            lastForm.Invoke(new MethodInvoker(delegate
            {
                if (lastForm is Index)
                {
                    (lastForm as Index).gameIsAbleToStart();
                }
                lastForm.Show();
                this.Close();
            }));

        }

        /// <summary>
        /// Se lance si tout les round d'une partie sont fini
        /// Ou lorsque le serveur se ferme
        /// </summary>
        /// <param name="packetHeader"></param>
        /// <param name="connection"></param>
        /// <param name="serverDoIt"></param>
        private void serveurStop(PacketHeader packetHeader, Connection connection, bool serverDoIt)
        {
            serveurClose = serverDoIt;
            miniForm.Invoke(new MethodInvoker(delegate
            {
                miniForm.Dispose();
            }));
            lastForm.Invoke(new MethodInvoker(delegate
            {
                this.Close();
            }));
        }

        private void yourSnakeIsSick(PacketHeader packetHeader, Connection connection, bool empoisonne)
        {
            

            this.Invoke(new MethodInvoker(delegate
            {
                if (empoisonne)
                {
                    lbxEmpoisonne.Visible = true;
                    lbxEmpoisonne.Enabled = false;
                    lbxEmpoisonne.Text = "Empoisonne";
                    lbxEmpoisonne.BackColor = Color.Violet;
                    lbxEmpoisonne.ForeColor = Color.White;
                    lbxEmpoisonne.Size = TextRenderer.MeasureText(lbxEmpoisonne.Text, lbxEmpoisonne.Font);
                    lbxEmpoisonne.Location = new Point(lbxPommePost.Right + 12, 505);
                    this.Controls.Add(lbxEmpoisonne);
                }
                else
                {
                    this.Controls.Remove(lbxEmpoisonne);
                }
            }));
        }


        private void gameEnd(PacketHeader packetHeader, Connection connection, string classement)
        {
           
            gameIsEnded = true;
            showClassement showClassement = new showClassement(JsonConvert.DeserializeObject<List<Classement>>(classement), this.numero);
            this.Invoke(new MethodInvoker(delegate
            {
                showClassement.ShowDialog();
            }));
            miniForm.Invoke(new MethodInvoker(delegate
            {
                miniForm.Dispose();
            }));
            if (showClassement.DialogResult == DialogResult.Retry)
            {
                serveurClose = false;
                lastForm.Invoke(new MethodInvoker(delegate
                {
                    //if (lastForm is Index)
                    (lastForm as Index).wantToReplay();
                    lastForm.Show();
                    this.Dispose();
                }));
            }
            if (showClassement.DialogResult == DialogResult.Abort)
            {
                lastForm.Invoke(new MethodInvoker(delegate
                {
                    this.Close();
                }));
            }
        }



        /// <summary>
        /// ouvre le petit formulaire d'information
        /// </summary>
        public void openInfoForm(int color, int nombreSerpent)
        {
            lbxNumPost = new TextBox();
            lbxPommePost = new TextBox();
            lbxScore = new TextBox();

            //Ecris le numero du poste
            lbxNumPost.Text = "Numéro du poste: " + (this.numero);
            lbxNumPost.Font = lbxScore.Font = lbxPommePost.Font = new Font("Comic Sans MS", 14);
            lbxNumPost.Enabled = lbxScore.Enabled = lbxPommePost.Enabled = false;
            lbxNumPost.Location = new Point(10, 505);
            lbxNumPost.Size = TextRenderer.MeasureText(lbxNumPost.Text, lbxNumPost.Font);
            lbxScore.Location = new Point(20 + lbxNumPost.Width, 505);

            this.Height += 40;

            //Ecoute a chaque fois qu'une pomme est mangé
            serveurConnection.AppendIncomingPacketHandler<int[]>("infoPomme", getNewPommeInfo);
            this.Controls.AddRange(new TextBox[3] { lbxNumPost, lbxScore, lbxPommePost});

            serveurConnection.SendObject("LaunchTheGame", new int[3] { this.Width, this.Height - 40, color });
            serveurConnection.AppendIncomingPacketHandler<string>("monde", afficherMonde);
            serveurConnection.AppendIncomingPacketHandler<int>("restart", roundEnd);
            serveurConnection.AppendIncomingPacketHandler<string>("classement", gameEnd);
            serveurConnection.AppendIncomingPacketHandler<bool>("turnOff", serveurStop);
            serveurConnection.AppendIncomingPacketHandler<bool>("empoisonne", yourSnakeIsSick);

            //panel1.Enabled = false;
            miniForm = new minimap(serveurConnection, nombreSerpent);
            this.panel1.Controls.Add(miniForm);
            miniForm.Show();

            if (miniForm.Width > this.Height)
            {
                panel1.AutoScroll = true;
                this.Height += 17 + miniForm.Height;
                panel1.Size = new Size(this.Width, 17 + miniForm.Height + 3);
            }
            else
            {
                panel1.AutoScroll = false;
                this.Height += miniForm.Height;
                panel1.Size = new Size(this.Width, miniForm.Height + 3);
            }
            miniForm.Enabled = false;
        }

        /// <summary>
        /// Se lance lorsque le serveur affiche une nouvelle pomme
        /// </summary>
        /// <param name="packetHeader"></param>
        /// <param name="connection"></param>
        /// <param name="infoPomme"></param>
        private void getNewPommeInfo(PacketHeader packetHeader, Connection connection, int[] infoPomme)
        {
            lbxScore.Invoke(new MethodInvoker(delegate
            {
                if (infoPomme[1] > -1)
                {
                    lbxScore.Text = "Score: " + infoPomme[1];
                    lbxScore.Size = TextRenderer.MeasureText(lbxScore.Text, lbxScore.Font);
                }
            }));
            lbxPommePost.Invoke(new MethodInvoker(delegate
            {
                lbxPommePost.Text = "Numéro du poste de la pomme: " + (infoPomme[0]);
                lbxPommePost.Size = TextRenderer.MeasureText(lbxPommePost.Text, lbxPommePost.Font);
                lbxPommePost.Location = new Point(30 + lbxNumPost.Width + lbxScore.Width, 505);
            }));

        }

        #region Affichage snake

        /// <summary>
        /// affiche et enlève les différents Label pour que l'utilisateur voit le serpent bouger
        /// </summary>
        /// <param name="packetHeader"></param>
        /// <param name="connection"></param>
        /// <param name="map"></param>
        private void afficherMonde(PacketHeader packetHeader, Connection connection, string map)
        {
            Monde monde = JsonConvert.DeserializeObject<Monde>(map);

            List<Control> worldAjouter;
            List<Control> worldEnlever;

            if (ancienMonde == null)
            {
                ancienMonde = monde;
                worldAjouter = initialiserMonde(monde);
                worldEnlever = null;
            }
            else
            {
                List<Label> c = this.Controls.OfType<Label>().Cast<Label>().ToList();
                afficherNouvellePomme(monde, out worldAjouter, out worldEnlever);
                if (monde.snake.Count != ancienMonde.snake.Count)
                {
                    worldEnlever.AddRange(snakeIsDead(monde, c));
                }
                worldEnlever.AddRange(effacerAnciennePiece(monde, ancienMonde, this.numero, c));
                worldAjouter.AddRange(ajouterNouvellePiece(monde, ancienMonde, this.numero));
                ancienMonde = monde;
            }

            this.Invoke(new MethodInvoker(delegate
            {
                if (worldEnlever != null)
                {
                    foreach (Control control in worldEnlever)
                    {
                        control.Dispose();
                    }
                }
                this.Controls.AddRange(worldAjouter.ToArray());
            }));
        }

        /// <summary>
        /// cree le monde la premiere fois
        /// </summary>
        /// <param name="monde">monde transferer par le serveur</param>
        /// <param name="worldAjouter">List qui permettra au client de savoir ce qu'il doit afficher</param>
        private List<Control> initialiserMonde(Monde monde)
        {
            List<Control> worldAjouter = new List<Control>();
            if (monde.pomme.Ecran == this.numero)
            {
                lblpomme = new Label();
                lblpomme.BackColor = Color.Red;
                lblpomme.Size = new Size(pieceSize, pieceSize);
                lblpomme.Location = new Point(monde.pomme.X * pieceSize, monde.pomme.Y * pieceSize);
                worldAjouter.Add(lblpomme);
            }

            foreach (Serpent s in monde.snake)
            {
                if (s.TeteEcran == this.numero)
                {
                    foreach (Pieces pieces in s.ListePiecesSerpent)
                    {
                        if (pieces.Ecran == this.numero)
                        {
                            Label serpent = new Label();
                            serpent.BackColor = Color.FromName(s.CouleurSerpent);
                            serpent.Size = new Size(pieceSize, pieceSize);
                            serpent.Location = new Point(pieces.X * pieceSize, pieces.Y * pieceSize);
                            serpent.Name = pieces.ID.ToString();
                            worldAjouter.Add(serpent);
                        }
                    }
                }
            }
            return worldAjouter;
        }

        /// <summary>
        /// Lorsqu'une pomme est mangé
        /// </summary>
        /// <param name="monde">monde transferer par le serveur</param>
        /// <param name="worldAjouter">List qui permettra au client de savoir ce qu'il doit afficher</param>
        /// <param name="worldEnlever">List qui permettra au client de savoir ce qu'il doit supprimer</param>
        public void afficherNouvellePomme(Monde monde, out List<Control> worldAjouter, out List<Control> worldEnlever)
        {
            worldAjouter = new List<Control>();
            worldEnlever = new List<Control>();
            if (ancienMonde.pomme.X == monde.pomme.X && ancienMonde.pomme.Y == monde.pomme.Y && ancienMonde.pomme.Ecran == monde.pomme.Ecran)
            {
            }
            else
            {
                if (lblpomme != null)
                {
                    worldEnlever.Add(lblpomme);
                }
                if (this.numero == monde.pomme.Ecran)
                {
                    lblpomme = new Label();
                    lblpomme.BackColor = Color.Red;
                    lblpomme.Size = new Size(pieceSize, pieceSize);
                    lblpomme.Location = new Point(monde.pomme.X * pieceSize, monde.pomme.Y * pieceSize);
                    worldAjouter.Add(lblpomme);
                }
            }
        }

        /// <summary>
        /// Calcul qu'elle piece existe encore dans l'ancien monde mais plus le nouveau pour la supprimer
        /// </summary>
        /// <param name="monde">monde transferer par le serveur</param>
        /// <param name="worldEnlever">List qui permettra au client de savoir ce qu'il doit supprimer</param>
        public List<Control> effacerAnciennePiece(Monde monde, Monde ancienMonde, int numero, List<Label> c)
        {
            List<Control> worldEnlever = new List<Control>();
            bool bSupp = false;
            foreach (Serpent ancienSerpent in ancienMonde.snake)
            {
                foreach (Pieces piecesSupprimer in ancienSerpent.ListePiecesSerpent)
                {
                    if (piecesSupprimer.Ecran == numero)
                    {
                        foreach (Pieces pieces in monde.snake[ancienMonde.snake.IndexOf(ancienSerpent)].ListePiecesSerpent)
                        {
                            if (piecesSupprimer.ID == pieces.ID)
                            {
                                bSupp = true;
                            }
                        }
                        if (bSupp == false)
                        {
                            foreach (Label tbx in c)
                            {
                                if (tbx.Name == piecesSupprimer.ID.ToString())
                                {
                                    worldEnlever.Add(tbx);
                                }
                            }
                        }
                        bSupp = false;
                    }
                }
            }
            return worldEnlever;
        }

        ///<summary>
        /// Calcul qu'elle est la piece qui existe dans le nouveau monde et pas l'ancien pour l'ajouter au formulaire
        /// </summary>
        /// <param name="monde">monde transferer par le serveur</param>
        public List<Control> ajouterNouvellePiece(Monde monde, Monde ancienMonde, int numero)
        {
            List<Control> worldAjouter = new List<Control>();
            bool bAdd = false;
            foreach (Serpent nouveauSerpent in monde.snake)
            {
                foreach (Pieces piecesAjouter in nouveauSerpent.ListePiecesSerpent)
                {
                    if (piecesAjouter.Ecran == numero)
                    {
                        foreach (Pieces pieces in ancienMonde.snake[monde.snake.IndexOf(nouveauSerpent)].ListePiecesSerpent)
                        {

                            if (piecesAjouter.ID == pieces.ID)
                            {
                                bAdd = true;
                            }

                        }
                        if (bAdd == false)
                        {
                            Label serpent = new Label();
                            serpent.BackColor = Color.FromName(nouveauSerpent.CouleurSerpent);
                            serpent.Size = new Size(pieceSize, pieceSize);
                            serpent.Location = new Point(piecesAjouter.X * pieceSize, piecesAjouter.Y * pieceSize);
                            serpent.Name = piecesAjouter.ID.ToString();
                            worldAjouter.Add(serpent);
                        }
                        bAdd = false;
                    }
                }
            }
            return worldAjouter;
        }

        /// <summary>
        /// Des qu'il n'y a plus le meme nombre de serpent dans le nouveau monde et l'ancien monde,
        /// le programme récupère la liste des pièces qui ne sont plus sur le terrains
        /// </summary>
        /// <param name="monde"></param>
        /// <param name="c"></param>
        /// <returns>Liste des pièces a faire disparaitre</returns>
        private List<Control> snakeIsDead(Monde monde, List<Label> c)
        {
            List<Control> worldEnlever = new List<Control>();

            bool bCeSerpent = false;
            List<Serpent> ChangeTheWorld = new List<Serpent>();
            foreach (Serpent ancienSerpent in ancienMonde.snake)
            {
                foreach (Serpent nouveauSerpent in monde.snake)
                {
                    if (ancienSerpent.ID == nouveauSerpent.ID)
                    {
                        bCeSerpent = true;
                    }
                }
                if (bCeSerpent == false)
                {
                    ChangeTheWorld.Add(ancienSerpent);
                    foreach (Pieces p in ancienSerpent.ListePiecesSerpent)
                    {
                        foreach (Label tbx in c)
                        {
                            if (tbx.Name == p.ID.ToString())
                            {
                                worldEnlever.Add(tbx);
                            }
                        }
                    }
                }
                bCeSerpent = false;
            }
            ChangeTheWorld.ForEach(x => ancienMonde.snake.Remove(x));

            return worldEnlever;
        }

        #endregion

        /// <summary>
        /// Envoie la touche séléctionner par l'utilistateur au serveur
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void SnakeForm_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.KeyCode)
            {
                case Keys.W:
                case Keys.A:
                case Keys.S:
                case Keys.D:
                case Keys.Up:
                case Keys.Left:
                case Keys.Down:
                case Keys.Right:
                    if (!gameIsEnded)
                        serveurConnection.SendObject("touche", e.KeyCode.ToString());
                    break;
            }
        }

        private void SnakeForm_FormClosed(object sender, FormClosedEventArgs e)
        {
            if (serveurClose)
            {
                lastForm.Close();
            }
        }

        private void SnakeForm_MouseDown(object sender, MouseEventArgs e)
        {
            //code from : http://csharphelper.com/blog/2015/07/move-a-form-without-a-title-bar-in-c/
            if (e.Button == MouseButtons.Left)
            {
                // Release the mouse capture started by the mouse down.
                this.Capture = false;

                // Create and send a WM_NCLBUTTONDOWN message.
                const int WM_NCLBUTTONDOWN = 0x00A1;
                const int HTCAPTION = 2;
                Message msg =
                    Message.Create(this.Handle, WM_NCLBUTTONDOWN,
                        new IntPtr(HTCAPTION), IntPtr.Zero);
                this.DefWndProc(ref msg);
            }
        }
    }
}
