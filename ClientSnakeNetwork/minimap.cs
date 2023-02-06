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
using Newtonsoft.Json;

namespace ClientSnakeNetwork
{
    public partial class minimap : Form
    {
        private Connection serveurConnection;
        private int nbParticipant;
        private Monde ancienMonde;
        Label lblpomme;
        private int pieceSize = 3;


        public minimap(Connection serveur,int iNbParticpant)
        {
            DoubleBuffered = true;
            serveurConnection = serveur;
            nbParticipant = iNbParticpant;

            InitializeComponent();

            this.Size = new Size(50 * pieceSize * nbParticipant, 25 * pieceSize);
            serveurConnection.AppendIncomingPacketHandler<string>("monde", afficherMonde);
            this.TopLevel = false;
            this.AutoScroll = true;
        }

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
                worldEnlever.AddRange(effacerAnciennePiece(monde, ancienMonde,c));
                worldAjouter.AddRange(ajouterNouvellePiece(monde, ancienMonde));
                ancienMonde = monde;
            }
            try
            {
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
            catch
            { }
        }

        /// <summary>
        /// cree le monde la premiere fois
        /// </summary>
        /// <param name="monde">monde transferer par le serveur</param>
        /// <param name="worldAjouter">List qui permettra au client de savoir ce qu'il doit afficher</param>
        private List<Control> initialiserMonde(Monde monde)
        {
            List<Control> worldAjouter = new List<Control>();

            lblpomme = new Label();
            lblpomme.BackColor = Color.Red;
            lblpomme.Size = new Size(pieceSize, pieceSize);
            lblpomme.Location = new Point(monde.pomme.X * pieceSize + pieceSize * 50 * monde.pomme.Ecran, monde.pomme.Y * pieceSize);
            worldAjouter.Add(lblpomme);

            foreach (Serpent s in monde.snake)
            {
                foreach (Pieces pieces in s.ListePiecesSerpent)
                {
                    Label serpent = new Label();
                    serpent.BackColor = Color.FromName(s.CouleurSerpent);
                    serpent.Size = new Size(pieceSize, pieceSize);
                    serpent.Location = new Point(pieces.X * pieceSize + pieceSize * 50 * pieces.Ecran, pieces.Y * pieceSize);
                    serpent.Name = pieces.ID.ToString();
                    worldAjouter.Add(serpent);
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

                lblpomme = new Label();
                lblpomme.BackColor = Color.Red;
                lblpomme.Size = new Size(pieceSize, pieceSize);
                lblpomme.Location = new Point(monde.pomme.X * pieceSize + pieceSize * 50 * monde.pomme.Ecran, monde.pomme.Y * pieceSize);
                worldAjouter.Add(lblpomme);
            }
        }

        /// <summary>
        /// Calcul qu'elle piece existe encore dans l'ancien monde mais plus le nouveau pour la supprimer
        /// </summary>
        /// <param name="monde">monde transferer par le serveur</param>
        /// <param name="worldEnlever">List qui permettra au client de savoir ce qu'il doit supprimer</param>
        public List<Control> effacerAnciennePiece(Monde monde, Monde ancienMonde, List<Label> c)
        {
            List<Control> worldEnlever = new List<Control>();
            bool bSupp = false;
            foreach (Serpent ancienSerpent in ancienMonde.snake)
            {
                foreach (Pieces piecesSupprimer in ancienSerpent.ListePiecesSerpent)
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
            return worldEnlever;
        }

        ///<summary>
        /// Calcul qu'elle est la piece qui existe dans le nouveau monde et pas l'ancien pour l'ajouter au formulaire
        /// </summary>
        /// <param name="monde">monde transferer par le serveur</param>
        public List<Control> ajouterNouvellePiece(Monde monde, Monde ancienMonde)
        {
            List<Control> worldAjouter = new List<Control>();
            bool bAdd = false;
            foreach (Serpent nouveauSerpent in monde.snake)
            {
                foreach (Pieces piecesAjouter in nouveauSerpent.ListePiecesSerpent)
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
                        serpent.Location = new Point(piecesAjouter.X * pieceSize + pieceSize * 50 * piecesAjouter.Ecran, piecesAjouter.Y * pieceSize);
                        serpent.Name = piecesAjouter.ID.ToString();
                        worldAjouter.Add(serpent);
                    }
                    bAdd = false;
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
    }
}
