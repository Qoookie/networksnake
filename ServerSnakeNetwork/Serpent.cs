using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using NetworkCommsDotNet;
using NetworkCommsDotNet.Connections;

namespace DuchosalN
{
    public class Serpent : IComparable
    {
        private int id;
        private int iIdPiece;
        private List<Pieces> snake;
        private int iDeplacementX, iDeplacementY;
        private int iTetePosX, iTetePosY, iTeteEcran;
        private string strConnection;
        private string snakeColor;
        private int iScore;
        private bool serpentEstIntoxique;

        public int ID { get => id; set => id = value; }
        public List<Pieces> ListePiecesSerpent { get => snake; set => snake = value; }
        public int DeplacementX { get => iDeplacementX; set => iDeplacementX = value; }
        public int DeplacementY { get => iDeplacementY; set => iDeplacementY = value; }
        public int TetePosX { get => iTetePosX; set => iTetePosX = value; }
        public int TetePosY { get => iTetePosY; set => iTetePosY = value; }
        public int TeteEcran { get => iTeteEcran; set => iTeteEcran = value; }
        public string SerpentConnection { get => strConnection;  set => strConnection = value; }
        public string CouleurSerpent { get => snakeColor;  set => snakeColor = value; }
        public int Score { get => iScore; set => iScore = value; }
        public int IDPiece { get => iIdPiece; set => iIdPiece = value; }
        public bool SerpentEstIntoxique { get => serpentEstIntoxique; set => serpentEstIntoxique = value; }

        public Serpent(string connection)
        {
            ListePiecesSerpent = new List<Pieces>();
            DeplacementX = 0;
            DeplacementY = 0;
            this.strConnection = connection;
        }

        public void resetSerpent()
        {
            ListePiecesSerpent = new List<Pieces>();
            DeplacementX = 0;
            DeplacementY = 0;
            Score = 0;
        }

        public void initialiseSeprent(int tailleDepart)
        {
            for (int i = 0; i < tailleDepart; i++)
            {
                this.ListePiecesSerpent.Add(new Pieces(this.TetePosX, this.TetePosY, this.TeteEcran, ID + "" + i));
            }
            this.IDPiece = this.ListePiecesSerpent.Count - 1;
        }

        public void deplacer()
        {
            for (int i = this.ListePiecesSerpent.Count - 1; i >= 0; i--)
            {
                if (i >= 1)
                {
                    this.ListePiecesSerpent[i] = this.ListePiecesSerpent[i - 1];
                }
                else
                {
                    if (IDPiece <= int.MaxValue - 1)
                    {
                        IDPiece++;
                    }
                    else
                    {
                        IDPiece = 0;
                    }
                    this.ListePiecesSerpent[0] = new Pieces(this.TetePosX, this.TetePosY, this.TeteEcran, this.ID + "" + IDPiece);
                }
            }
        }

        public int CompareTo(object obj)
        {
            Serpent s = obj as Serpent;
            if (Score > s.Score)
                return -1;
            if (Score < s.Score)
                return 1;
            return 0;
        }
    }
}
