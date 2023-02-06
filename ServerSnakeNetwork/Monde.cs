using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuchosalN
{
    public class Monde
    {
        public List<Serpent> snake { get; set; }
        public Pomme pomme { get; set; }

        public Monde()
        {
            snake = new List<Serpent>();
            pomme = new Pomme();
        }
    }
}
