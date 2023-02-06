using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuchosalN
{
    public class Pomme
    {
        public int X { get; set; }
        public int Y { get; set; }
        public int Ecran { get; set; }
        public bool estEmpoisonne { get; set; }

        public Pomme(int x, int y, int ecran, bool estempoisonne)
        {
            X = x;
            Y = y;
            Ecran = ecran;
            estEmpoisonne = estEmpoisonne;
        }
        public Pomme()
        {
        }

    }
}
