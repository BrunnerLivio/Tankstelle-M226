using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.Businesslogic.Statistic
{
    /// <summary>
    /// Represents one Statistic for example a year or a month
    /// </summary>
    public class Statistic
    {
        private IEnumerable<Receipt> receipts;
        /// <summary>
        /// Initializes one Statistic
        /// </summary>
        /// <param name="receipts">The receipts of the statistic</param>
        public Statistic(IEnumerable<Receipt> receipts)
        {
            this.receipts = receipts;
        }
        /// <summary>
        /// Calculates the Sales of the Statistic
        /// </summary>
        public int Sales
        {
            get
            {
                return receipts.Sum(r => r.Cost);
            }
        }
        
        private IEnumerable<string> FuelTypes
        {
            get
            {
                return receipts.GroupBy(r => r.FuelName).Select(group => group.Key);
            }
        }
        /// <summary>
        /// Gives the Fuel Statistics back
        /// </summary>
        public IEnumerable<FuelStatistic> FuelStatistics
        {
            get
            {
                foreach (string fuelType in FuelTypes)
                {
                    yield return new FuelStatistic(fuelType, receipts.Where(r => r.FuelName == fuelType).Sum(r => r.UsedFuel));
                }
            }
        }

    }
}
