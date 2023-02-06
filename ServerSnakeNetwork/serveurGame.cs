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
using System.Threading;

namespace ServerSnakeNetwork
{
    public partial class serveurGame : Form
    {
        public IndexServeur index;

        const int TAILLE_SERPENT_INITIAL = 10;

        int cols, rows;
        int classementSerpent;

        NetworkSnake server = new NetworkSnake();

        List<Connection> connections;
        List<Serpent> serpents;
        List<Classement> classements;

        Monde map;

        List<KnownColor> colorList = new List<KnownColor>();

        private static Random rng = new Random();

        int turn;

        int iEveryoneConnect;
        int posPommeX, posPommeY;
        //int tailleSerpent = 20;
        int tailleTerrainX = 50, tailleTerrainY = 25;
        int nombreRoundJoue;
        int ecranPomme;
        public int ScoreMax, roundGame , iVitesse;
        public string gameName, password;
        bool pommeEstEmpoisonne;
        bool firstLap;
        bool gameEndNormaly = false;

        public serveurGame()
        {
            connections = new List<Connection>();
            InitializeComponent();

            colorList = Enum.GetValues(typeof(KnownColor)).Cast<KnownColor>().ToList();
            this.MaximumSize = new Size(this.Width, this.Height);
            this.MinimumSize = new Size(this.Width, this.Height);
        }

        private void Form1_Load(object sender, EventArgs e)
        {
            resizeAutoForm();

            nombreRoundJoue = roundGame;
            connections[0].AppendIncomingPacketHandler<bool>("gameCanStart", gameStart);
        }

        public void setNewClientConnection(Connection connection)
        {
            connections.Add(connection);
            lbxAdresseIP.Items.Add(connection.ConnectionInfo.RemoteEndPoint.ToString());
        }

        /// <summary>
        /// Envoie a tout les clients un message pour indiquer que la partie commence
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void gameStart(PacketHeader packetHeader, Connection connection, bool gameStart)
        {
            if (connections.Count > 0)
            {
                turn = 0;
                iEveryoneConnect = 0;
                firstLap = true;

                classementSerpent = connections.Count;

                if (nombreRoundJoue == roundGame)
                {
                    index.Invoke(new MethodInvoker(delegate
                    {
                        index.lobbyStart(this);
                    }));
                    index = null;

                    tPartie.Interval = iVitesse;

                    serpents = new List<Serpent>();
                    classements = new List<Classement>();

                    foreach (Connection conn in connections)
                    {
                        serpents.Add(new Serpent(conn.ToString()));
                        classements.Add(new Classement(conn.ToString()));
                    }
                }
                else
                {
                    foreach (Serpent s in serpents)
                    {
                        s.SerpentEstIntoxique = false;
                    }
                }

                canLaunchTheGame();
                receiveCaps();

                foreach (Connection conn in connections)
                {
                    if (conn.ConnectionInfo.ConnectionState == NetworkCommsDotNet.ConnectionState.Established)
                    {
                        conn.SendObject("GameStart", new int[] { connections.IndexOf(conn), connections.Count });
                    }
                }

            }
        }

        /// <summary>
        /// A chauqe tick du chronomètre, le serpent se déplace
        /// Si la tete du serpent se trouve sur une pomme, une nouvelle pomme est crée
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void tDeplacment_Tick(object sender, EventArgs e)
        {
            bool atouche = false, aMangeTropDePomme = false;
            List<Serpent> SDelete = new List<Serpent>();

            turn++;

            foreach (Serpent s in serpents.Where(s => s.ListePiecesSerpent != null))
            {
                s.TetePosX += s.DeplacementX;
                s.TetePosY += s.DeplacementY;

                serpentEstHautBas(s);
                serpentEstGaucheDroite(s);

                if (s.TetePosX == posPommeX && s.TetePosY == posPommeY && ecranPomme == s.TeteEcran)
                {
                    s.ListePiecesSerpent.Add(s.ListePiecesSerpent[s.ListePiecesSerpent.Count - 1]);
                }

                s.deplacer();

                if (OnSerpent(s.TetePosX, s.TetePosY, s.TeteEcran, s))
                {
                    atouche = true;
                    SDelete.Add(s);
                }

                if (s.TetePosX == posPommeX && s.TetePosY == posPommeY && ecranPomme == s.TeteEcran)
                {
                    if (pommeEstEmpoisonne)
                    {
                        foreach (Connection conn in connections)
                        {
                            if (conn.ToString() == s.SerpentConnection)
                            {
                                conn.SendObject("empoisonne",true);
                            }
                        }
                        s.SerpentEstIntoxique = true;
                        tBonus.Start();
                    }
                    apparaitrePomme(true, s);
                    if (s.Score >= ScoreMax && ScoreMax > 0)
                    {
                        aMangeTropDePomme = true;
                    }
                }
            }

            if (atouche)
            {
                snakeIsDead(SDelete);
            }
            
            creeMonde();
            sendMonde();

            gameAsEnd(aMangeTropDePomme);
        }

        /// <summary>
        /// Envoie l'information a chaque client qu'ils doivent se fermer
        /// </summary>
        /// <param name="sender"></param>
        /// <param name="e"></param>
        private void Form1_FormClosing(object sender, FormClosingEventArgs e)
        {
            foreach (Connection conn in connections)
            {
                conn.RemoveIncomingPacketHandler("touche");
                conn.RemoveIncomingPacketHandler("LaunchTheGame");
                if (conn.ConnectionInfo.ConnectionState == NetworkCommsDotNet.ConnectionState.Established)
                {
                    //int numConnection = lbxAdresseIP.Items.IndexOf(adresse);
                    if (!gameEndNormaly)
                    {
                        conn.SendObject("turnOff", true);
                    }
                }
            }
            connections[0].RemoveIncomingPacketHandler("gameCanStart");
            connections = null;
            this.Dispose();
        }

        /// <summary>
        /// Redimensionne le panel en fonction du texte qui est contenu
        /// </summary>
        private void resizeAutoForm()
        {
            lblConf.Text = server.ToString();
            pConfiguration.Height = txtConfiguration.Height + lblConf.Height + 25;
            label1.Location = new Point(label1.Location.X, pConfiguration.Location.Y + pConfiguration.Height + 10);
            lbxAdresseIP.Location = new Point(lbxAdresseIP.Location.X, label1.Location.Y + label1.Height + 10);
            lbxAdresseIP.Height = this.ClientSize.Height - lbxAdresseIP.Location.Y - 20;
        }

        /// <summary>
        /// Une fois que le bouton Start Game a été appuyé,
        /// Chaque client enverra la confirmation qu'il est pret
        /// Ici sera calculé la position de la premiere piece du serpent
        /// Ainsi que la position de la premiere pomme
        /// </summary>
        private void canLaunchTheGame()
        {
            foreach (Connection conn in connections)
            {
                conn.AppendIncomingPacketHandler<int[]>("LaunchTheGame", (packetHeader, connection, infoUser) =>
                {
                    if (connection == connections[0])
                    {
                        setWorldSize();
                    }

                    int iSerpent = connections.IndexOf(connection);

                    if (this.nombreRoundJoue == roundGame)
                    {
                        serpents[iSerpent].CouleurSerpent = setSerpentCouleur(infoUser[2]);
                        classements.Where(c => c.connection == connection.ToString()).First().colorSerpent = serpents[iSerpent].CouleurSerpent;
                    }
                    else
                    {
                        serpents[iSerpent].resetSerpent();
                    }
                    serpents[iSerpent].TetePosX = rng.Next(tailleTerrainX);
                    serpents[iSerpent].TetePosY = rng.Next(tailleTerrainY);
                    serpents[iSerpent].TeteEcran = connections.IndexOf(connection);

                    serpents[iSerpent].ID = iSerpent;

                    serpents[iSerpent].initialiseSeprent(TAILLE_SERPENT_INITIAL);
                    iEveryoneConnect++;

                    if (iEveryoneConnect == connections.Count)
                    {
                        apparaitrePomme(false, null);
                        creeMonde();
                        sendMonde();
                    }
                });
            }
        }

        /// <summary>
        /// Indique le nombre de ligne et de colone que contient le monde
        /// </summary>
        private void setWorldSize()
        {
            cols = tailleTerrainX * this.connections.Count();
            rows = tailleTerrainY;
        }

        /// <summary>
        /// Change la couleur du serpent en fonction de ce que choisit l'utilisateur
        /// </summary>
        /// <param name="userColor">Couleur choisi pas l'utilisateur</param>
        /// <returns>La couleur du serpent</returns>
        private string setSerpentCouleur(int userColor)
        {
            Color User;
            if (userColor == 0)
            {
                User = randomColor();
            }
            else
            {
                User = Color.FromArgb(userColor);
            }

            foreach (KnownColor kc in colorList)
            {
                Color color = Color.FromKnownColor(kc);
                if (User.ToArgb() == color.ToArgb())
                {
                    return kc.ToString();
                }
            }
            return "Blue";
        }

        /// <summary>
        /// Si un client a séléctionner couleur aléatoire,
        /// c'est dans cette méthode qu'elle sera choisit
        /// </summary>
        /// <returns>Une couleur aléatoire</returns>
        private Color randomColor()
        {
            // Pour les couleurs du serpent
            // https://www.codeproject.com/Questions/826358/How-to-choose-a-random-color-from-System-Drawing-C
            int numColor;
            //L'utilisateur peut chosir sa couleur
            do
            {
                numColor = rng.Next(0, colorList.Count - 1);
            } while (colorIsCloseToWhite(Color.FromKnownColor(colorList[numColor])) || colorList[numColor] == KnownColor.Red);

            return Color.FromKnownColor(colorList[numColor]);
        }

        /// <summary>
        /// Vérifie que la couleur ne soit pas trop proche du blanc,
        /// Utilisé pour que les couleurs soit visible tout au long de la partie
        /// </summary>
        /// <param name="a"></param>
        /// <param name="threshold"></param>
        /// <returns>Si la couleur est proche ou non du blanc</returns>
        private bool colorIsCloseToWhite(Color a, int threshold = 90)
        {
            //Code from https://stackoverflow.com/questions/25168445/how-to-determine-if-a-color-is-close-to-another-color
            int r = (int)a.R - Color.White.R,
                g = (int)a.G - Color.White.G,
                b = (int)a.B - Color.White.B;
            return (r * r + g * g + b * b) <= threshold * threshold;
        }

        /// <summary>
        /// A chaque fois qu'une touche est appuyé sur un client elle est inscrite dans un historique,
        /// Permet de calculer le mouvment suivant de la piece
        /// Lorsque la premiere touche est envoyé, le timer démarre
        /// </summary>
        private void receiveCaps()
        {
            foreach (Connection conn in connections)
            {
                conn.AppendIncomingPacketHandler<string>("touche", (packetHeader, connection, touche) =>
                {

                    if (firstLap)
                    {
                        this.Invoke(new MethodInvoker(delegate
                        {
                            tPartie.Start();
                        }));
                        firstLap = false;
                    }

                    histTouche.Invoke(new MethodInvoker(delegate
                    {
                        histTouche.Items.Add(touche);
                        histTouche.TopIndex = histTouche.Items.Count - 1;
                    }));
                    foreach (Serpent s in serpents.Where(s => s.ListePiecesSerpent != null))
                    {
                        if (s.SerpentConnection == connection.ToString())
                        {
                            deplaceSerpent(touche.ToUpper(), s);
                        }
                    }
                });
            }
        }

        /// <summary>
        /// Verifie si le serpent est sur une case tout en haut ou en bas d'un ecran
        /// </summary>
        private void serpentEstHautBas(Serpent serpent)
        {
            if (serpent.TetePosY >= tailleTerrainY || serpent.TetePosY < 0)
            {
                if (serpent.TetePosY >= tailleTerrainY)
                {
                    serpent.TetePosY = 0;
                }
                if (serpent.TetePosY < 0)
                {
                    serpent.TetePosY = tailleTerrainY - 1;
                }
            }
        }

        /// <summary>
        /// Verifie si le serpent est sur une case tout a droite ou a gauche d'un ecran
        /// </summary>
        private void serpentEstGaucheDroite(Serpent serpent)
        {
            if (serpent.TetePosX >= tailleTerrainX || serpent.TetePosX < 0)
            {
                if (serpent.TetePosX >= tailleTerrainX)
                {
                    if (serpent.TeteEcran == connections.Count - 1)
                    {
                        serpent.TeteEcran = 0;
                    }
                    else
                    {
                        serpent.TeteEcran++;
                    }
                    serpent.TetePosX = 0;
                }
                if (serpent.TetePosX < 0)
                {
                    if (serpent.TeteEcran == 0)
                    {
                        serpent.TeteEcran = connections.Count - 1;
                    }
                    else
                    {
                        serpent.TeteEcran--;
                    }
                    serpent.TetePosX = tailleTerrainX - 1;
                }
            }
        }

        /// <summary>
        /// Calcul la postion d'une pomme qui ne se trouve pas sur le serpent
        /// envoie au petit ecran d'information ça position et le nouveau score
        /// </summary>
        /// <param name="augmenteScore">Si une pomme a été manger</param>
        private void apparaitrePomme(bool augmenteScore, Serpent serpent)
        {
            if (augmenteScore)
            {
                serpent.Score++;
            }

            int YPomme;
            int XPomme;

            do
            {
                YPomme = rng.Next(rows);
                XPomme = rng.Next(cols);

                ecranPomme = (XPomme / 50);
                posPommeX = (XPomme % 50);
                posPommeY = YPomme;

                pommeEstEmpoisonne = false;

                if (rng.Next(15) == 1)
                    pommeEstEmpoisonne = true;

            } while (OnSerpent(posPommeX, posPommeY, ecranPomme, null));

            sendInfoPomme();
        }

        /// <summary>
        /// Recupère le score de chaque serpent encore en lice
        /// Si le serpent est mort, le serveur lui envoie -1 pour le prévenir de pas changer le score afficher
        /// Envoie a chaque serveur l'emplacement de la pomme
        /// </summary>
        private void sendInfoPomme()
        {
            int scoreSerpent = 0;
            bool serpentExist = false;

            foreach (Connection conn in connections)
            {
                foreach (Serpent s in serpents.Where(s => s.ListePiecesSerpent != null))
                {
                    if (s.SerpentConnection == conn.ToString())
                    {
                        scoreSerpent = s.Score;
                        serpentExist = true;
                    }
                }
                if (!serpentExist)
                {
                    scoreSerpent = -1;
                    serpentExist = false;
                }
                if (conn.ConnectionInfo.ConnectionState == NetworkCommsDotNet.ConnectionState.Established)
                {
                    conn.SendObject("infoPomme", new int[2] { ecranPomme, scoreSerpent });
                }
                else
                {
                    this.Close();
                }
            }
        }

        /// <summary>
        /// Verifie que la nouvelle pomme ne soit pas sur une case du serpent
        /// </summary>
        /// <param name="X">Position X d'une pomme</param>
        /// <param name="Y">Position Y d'une pomme</param>
        /// <param name="Ecran">Ecran sur le quelle se trouve la pomme</param>
        /// <returns>vrai si la pomme est sur une case du serpent</returns>
        private bool OnSerpent(int X, int Y, int Ecran, Serpent s)
        {
            foreach (Serpent serpent in serpents.Where(x => x.ListePiecesSerpent != null))
            {
                if (s != serpent)
                {
                    foreach (Pieces pieces in serpent.ListePiecesSerpent)
                    {
                        if (Ecran == pieces.Ecran && X == pieces.X && Y == pieces.Y)
                        {
                            return true;
                        }
                    }
                }
            }
            return false;
        }

        /// <summary>
        /// Si deux serpent sont en contact, le programme va vérifier qui a touché qui avec la tete
        /// et fait dispaitre tout les serpents ayant eu un contact avec un autre serpent
        /// </summary>
        /// <param name="SDelete"></param>
        private void snakeIsDead(List<Serpent> SDelete)
        {
            foreach (Serpent s in SDelete)
            {
                foreach (Classement classement in classements)
                {
                    if (classement.connection == s.SerpentConnection)
                    {
                        classement.setPointClassement(classementSerpent);
                        classement.setPointPomme(s.Score);
                    }
                }
                s.ListePiecesSerpent = null;
            }
            classementSerpent -= SDelete.Count;
        }

        /// <summary>
        /// Vérifie que l'une des conditions pour qu'une partie se finisse ne soit pas atteinte
        /// Condition 1 : Il ne reste qu'un seul ou aucun serpent en vie
        /// Condition 2 : Le nombre maximum de pomme a été mangé
        /// </summary>
        /// <param name="aMangeTropDePomme"></param>
        private void gameAsEnd(bool aMangeTropDePomme)
        {
            if (connections.Count > 1 && serpents.Where(s => s.ListePiecesSerpent != null).Count() <= 1 || aMangeTropDePomme == true)
            {
                nombreRoundJoue--;
                tBonus.Stop();
                tPartie.Stop();
                if (aMangeTropDePomme)
                {
                    maxPommeMange();
                }

                if (serpents.Where(s => s.ListePiecesSerpent != null).Count() == 1 && connections.Count > 1)
                {
                    dernierSerpentVivant();
                }
                createClassement();
            }
        }

        /// <summary>
        /// Calcul le nombre de point au classement si le nombre max de pomme a été mangé
        /// </summary>
        private void maxPommeMange()
        {
            List<Serpent> classementSerpent = new List<Serpent>(serpents.Where(s => s.ListePiecesSerpent != null));
            classementSerpent.Sort();

            foreach (Serpent s in classementSerpent)
            {
                foreach (Classement c in classements)
                {
                    if (c.connection == s.SerpentConnection)
                    {
                        c.setPointClassement(classementSerpent.IndexOf(s) + 1);
                        c.setPointPomme(s.Score);
                    }
                }
            }
        }

        private void tBonus_Tick(object sender, EventArgs e)
        {
            foreach (Serpent s in serpents)
            {
                if (s.SerpentEstIntoxique)
                {
                    foreach (Connection conn in connections)
                    {
                        if(s.SerpentConnection == conn.ToString())
                        {
                            conn.SendObject("empoisonne",false);
                        }
                    }
                    s.SerpentEstIntoxique = false;
                }
            }
            tBonus.Stop();
        }

        /// <summary>
        /// Calcul le nombre de point au classement si il ne reste qu'un seul serpent en vie
        /// </summary>
        private void dernierSerpentVivant()
        {
            foreach (Classement c in classements)
            {
                if (serpents.Where(s => s.ListePiecesSerpent != null).First().SerpentConnection == c.connection)
                {
                    c.setPointClassement(classementSerpent);
                    c.setPointPomme(serpents[0].Score);
                }
            }
        }

        /// <summary>
        /// Si il reste des round -> envoie a chaque client un message comme quoi il faut redémarrer la partie
        /// si il n'y a plus de round -> envoie a chaque client un message pour informer que la partie est fini
        /// </summary>
        private void createClassement()
        {
            if (nombreRoundJoue == 0)
            {
                foreach (Connection conn in connections)
                {
                    //conn.SendObject("turnOff", true);
                    conn.RemoveIncomingPacketHandler("touche");
                    conn.RemoveIncomingPacketHandler("LaunchTheGame");
                    conn.SendObject("classement", JsonConvert.SerializeObject(classements));
                    gameEndNormaly = true;
                }
                //afficherClassement();
                this.Close();
            }
            else
            {
                foreach (Connection conn in connections)
                {
                    conn.RemoveIncomingPacketHandler("touche");
                    conn.RemoveIncomingPacketHandler("LaunchTheGame");
                    conn.SendObject<int>("restart", turn * tPartie.Interval / 1000);
                }
            }
        }

        /// <summary>
        /// Affiche a l'aide de Label un classement
        /// Chaque label a la couleur de son serpent
        /// la taille du label dépend du nombre de point
        /// </summary>
        private void afficherClassement()
        {
            foreach (Classement c in classements)
            {
                this.MaximumSize = new Size(this.Width, this.Height + 30);
                this.Height += 30;
                Label label = new Label();
                label.Location = new Point(lbxAdresseIP.Location.X, this.ClientSize.Height - 25);
                label.Size = new Size(100 + c.nombrePointFinal(), 20);
                label.BackColor = Color.FromName(c.colorSerpent);

                if (colorIsCloseToWhite(Color.FromName(c.colorSerpent), threshold: 312))
                {
                    label.ForeColor = Color.Black;
                }
                else
                {
                    label.ForeColor = Color.White;
                }
                label.RightToLeft = RightToLeft.Yes;
                label.Text = c.nombrePointFinal().ToString();
                this.Controls.Add(label);
            }
        }

        /// <summary>
        /// Creer un monde qui sera envoyé a chaque client
        /// Un monde est composé d'un serpent et d'une pomme
        /// </summary>
        private void creeMonde()
        {
            map = new Monde();
            map.pomme = new Pomme(posPommeX, posPommeY, ecranPomme, pommeEstEmpoisonne);
            map.snake = serpents.Where(s => s.ListePiecesSerpent != null).ToList();
        }

        /// <summary>
        /// Evoie le monde précédement creer a tout les clients
        /// </summary>
        private void sendMonde()
        {
            string json = JsonConvert.SerializeObject(map);
            foreach (Connection conn in connections)
            {
                if (conn.ConnectionInfo.ConnectionState == NetworkCommsDotNet.ConnectionState.Established)
                {

                    conn.SendObject("monde", json);
                }
                else
                {
                    this.Close();
                }
            }
        }

        /// <summary>
        /// Indique la direction du serpent
        /// </summary>
        /// <param name="c">touch envoyé par le client</param>
        private void deplaceSerpent(string c, Serpent serpent)
        {
            switch (c)
            {
                case "W":
                case "UP":
                    serpent.DeplacementX = 0;
                    serpent.DeplacementY = !serpent.SerpentEstIntoxique ? -1 : 1;
                    break;
                case "S":
                case "DOWN":
                    serpent.DeplacementX = 0;
                    serpent.DeplacementY = !serpent.SerpentEstIntoxique ? 1 : -1;
                    break;
                case "A":
                case "LEFT":
                    serpent.DeplacementX = !serpent.SerpentEstIntoxique ? -1 : 1; ;
                    serpent.DeplacementY = 0;
                    break;
                case "D":
                case "RIGHT":
                    serpent.DeplacementX = !serpent.SerpentEstIntoxique ? 1 : -1;
                    serpent.DeplacementY = 0;
                    break;
            }
        }
    }
}
