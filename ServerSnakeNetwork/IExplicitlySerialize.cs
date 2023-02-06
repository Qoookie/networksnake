using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DuchosalN
{
    public interface IExplicitlySerialize
    {
        int[] Serialize();
        void Deserialize(int[] obj);
    }
}
