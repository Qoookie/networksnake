using System;
using System.IO;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Drawing.Text;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;
using DuchosalN;
using NetworkCommsDotNet.Connections;

namespace ClientSnakeNetwork
{
    public partial class showClassement : Form
    {
        private const int marge = 12;
        int graphSize;
        int maxSize;
        private List<Classement> classements;

        bool buttonPress = false;
        bool monScore = false;
        private int numero;

        public showClassement(List<Classement> affichage, int iNumero)
        {
            classements = affichage;
            numero = iNumero;
            InitializeComponent();
            this.MaximizeBox = false;
        }

        private void showClassement_Load(object sender, EventArgs e)
        {
            maxSize = 0;
            graphSize = (this.Size.Width - marge  - marge * (classements.Count + 1)) / classements.Count;
            foreach (Classement c in classements)
            {
                if (c.nombrePointFinal() > maxSize)
                {
                    maxSize = c.nombrePointFinal();
                }
            }
            maxSize += 100;
        }

        private void showClassement_Paint(object sender, PaintEventArgs e)
        {
            Graphics graphClassement;
            graphClassement = this.CreateGraphics();

            if (!monScore)
            {
                int i = 0;
                label1.Text = "Classement General";
                Point p = new Point();
                foreach (Classement c in classements)
                {
                    SolidBrush myPen = new SolidBrush(Color.FromName(c.colorSerpent));
                    graphClassement.FillRectangle(myPen, drawRectangle(c, i));

                    graphClassement.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

                    StringFormat format = new StringFormat()
                    {
                        Alignment = StringAlignment.Center,
                        LineAlignment = StringAlignment.Center
                    };
                    p = new Point(marge * (i + 1) + (graphSize / 2) + graphSize * i, 2 * marge + maxSize + button1.Height + 5);

                    // Draw the text onto the image
                    graphClassement.DrawString(c.nombrePointFinal().ToString(), new Font("Tahoma", 10), Brushes.Black, p, format);

                    i++;
                }
                this.Height = this.Height - this.ClientSize.Height + p.Y + 2 * marge + panel1.Height;
            }
            else
            {
                Point p = new Point();
                label1.Text = "Resultat Personnel";
                Rectangle rect = new Rectangle(12, button1.Location.Y + button1.Height + 5, this.Size.Width - 4 * marge, this.Size.Width - 4 * marge);
                float angle = 0, angleFinal = 0;
                double multiplicateur = 1.00;

                angleFinal = classements[numero].iNbPommeMange + classements[numero].iPointPlacement;
                multiplicateur = 100 / angleFinal;

                graphClassement.FillPie(new SolidBrush(Color.Red), rect, angle, Convert.ToSingle(classements[numero].iNbPommeMange * 3.6 * multiplicateur));
                angle += Convert.ToSingle(classements[numero].iNbPommeMange * 3.6 * multiplicateur);

                SolidBrush myPen = new SolidBrush(Color.FromName(classements[numero].colorSerpent));
                graphClassement.FillPie(myPen, rect, angle, Convert.ToSingle(classements[numero].iPointPlacement * 3.6 * multiplicateur));

                graphClassement.TextRenderingHint = TextRenderingHint.AntiAliasGridFit;

                StringFormat format = new StringFormat()
                {
                    Alignment = StringAlignment.Near,
                    LineAlignment = StringAlignment.Center
                };

                p = new Point(12, rect.X + rect.Height + 40);
                // Draw the text onto the image
                graphClassement.DrawString(classements[numero].iNbPommeMange + " pommes mangés", new Font("Tahoma", 10), Brushes.Red, p, format);
                p = new Point(12, rect.X + rect.Height + 54);
                graphClassement.DrawString(classements[numero].iPointPlacement + " point de placement", new Font("Tahoma", 10), Brushes.Black, p, format);
                this.Height = this.Height - this.ClientSize.Height + p.Y + panel1.Height + 2 * marge;
            }
        }

        private Rectangle drawRectangle(Classement classement ,int position)
        {
            Rectangle rect;
            rect = new Rectangle(marge * (position + 1) + position * graphSize,  marge + maxSize + button1.Height + 5, graphSize, -1 * (100 + classement.nombrePointFinal()));
            rect = SortedRect(new Point(rect.X, rect.Y), rect.Size);
            return rect;
        }

        private Rectangle SortedRect(Point P, Size S)
        {
            if (S.Width < 0)
            {
                S.Width = -S.Width;
                P.X = P.X - S.Width;
            }
            if (S.Height < 0)
            {
                S.Height = -S.Height;
                P.Y = P.Y - S.Height;
            }
            return new Rectangle(P, S);
        }

        private void showClassement_FormClosing(object sender, FormClosingEventArgs e)
        {
            if (!buttonPress)
            {
                this.DialogResult = DialogResult.Abort;
            }
        }

        private void btnRecommencer_Click(object sender, EventArgs e)
        {
            buttonPress = true;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            monScore = !monScore;
            this.Invalidate();
        }
    }
}
