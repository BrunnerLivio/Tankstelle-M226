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
        private int rappenPerMilliliters;
        #endregion

        #region constructors
        /// <summary>
        /// Initializes a Fuel
        /// </summary>
        /// <param name="price">Rappen of the Fuel per Milliliters</param>
        /// <param name="name">The Name of the Fuel e.g. "Petrol"</param>
        public Fuel(int rappenPerMilliliters, string name)
        {
            this.name = name;
            this.rappenPerMilliliters = rappenPerMilliliters;
        }

        /// <summary>
        /// Gets rappen per milliliters of this fuel type.
        /// </summary>
        public int RappenPerMilliliters
        {
            get
            {
                return rappenPerMilliliters;
            }
        }

        public string Name
        {
            get
            {
                return name;
            }
        }
        #endregion
    }
}
