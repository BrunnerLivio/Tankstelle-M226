using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.Businesslogic
{
    [Serializable]
    public class Fuel
    {
        #region membervariables
        private string name;
        private int pricePerMilliliters;
        #endregion

        #region constructors
        /// <summary>
        /// Initializes a Fuel
        /// </summary>
        /// <param name="price">The Price of the Fuel per Milliliters</param>
        /// <param name="name">The Name of the Fuel e.g. "Petrol"</param>
        public Fuel(int pricePerMilliliters, string name)
        {
            this.name = name;
            this.pricePerMilliliters = pricePerMilliliters;
        }

        /// <summary>
        /// Gets the price per milliliters of this fuel type.
        /// </summary>
        public int PricePerMilliliters
        {
            get
            {
                return pricePerMilliliters;
            }
        }
        #endregion
    }
}
