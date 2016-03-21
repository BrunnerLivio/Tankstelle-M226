using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.Businesslogic.Statistic
{
    public class FuelStatistic
    {
        private string name;
        private int usedFuel;
        /// <summary>
        /// Initializes a FuelStatistic
        /// </summary>
        /// <param name="name">Name of the Fuel</param>
        /// <param name="usedFuel">The used fuel</param>
        public FuelStatistic(string name, int usedFuel)
        {
            this.name = name;
            this.usedFuel = usedFuel;
        }
        /// <summary>
        /// Gives the Name back of the Fuel Statistic
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }
        /// <summary>
        /// Gives the used fuel of the statistic back in milliliters
        /// </summary>
        public int UsedFuel
        {
            get
            {
                return usedFuel;
            }
        }
    }
}
