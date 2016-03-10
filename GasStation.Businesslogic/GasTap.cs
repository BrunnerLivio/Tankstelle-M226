using AnttittyFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.Businesslogic
{
    /// <summary>
    /// Represents one Gas Tap
    /// </summary>
    [Serializable]
    public class GasTap : DbItem
    {
        private GasPump gasPump;
        public Fuel fuel;   
        public void Use()
        {

        }
    }
}
