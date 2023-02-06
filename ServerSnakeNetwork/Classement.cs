using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuchosalN
{
    public class Classement
    {
        public string connection { get; set; }
        public string colorSerpent { get; set; }
        public int iNbPommeMange { get; set; }
        public int iPointPlacement { get; set; }

        public Classement(string connection)
        {
            this.connection = connection;
        }

        public int nombrePointFinal()
        {
            return iNbPommeMange + iPointPlacement;
        }

        public void setPointClassement(int classement)
        {
            iPointPlacement += 50 / classement;
        }
        public void setPointPomme(int ipommeMange)
        {
            iNbPommeMange += ipommeMange;
        }
    }
}
