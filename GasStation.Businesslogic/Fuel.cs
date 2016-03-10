using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.Businesslogic
{
    public class Fuel
    {
        #region membervariables
        private string fuelName;
        private int fuelPrice;
        #endregion

        #region constructors
        public Fuel(int fuelPrice, string fuelName)
        {
            this.fuelName = fuelName;
            this.fuelPrice = fuelPrice;
        }
        #endregion
    }
}
