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
        private string name;
        private int rappenPerMilliliters;

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
        /// <summary>
        /// Gives back how much franken a liter costs of the fuel
        /// </summary>
        /// <remarks>
        /// DO NOT USE FOR CALCULATION (not accurate).
        /// JUST FOR DISPLAY
        /// </remarks>
        public double FrankenPerLiter
        {
            get
            {
                return (double)rappenPerMilliliters / 100 * 1000;
            }
        }
        /// <summary>
        /// Gives the Name of the Fuel back
        /// </summary>
        /// <example>
        /// Diesel, Super or Petrol
        /// </example>
        public string Name
        {
            get
            {
                return name;
            }
        }
    }
}
