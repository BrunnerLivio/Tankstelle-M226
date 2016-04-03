using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.Businesslogic.Statistic
{
    /// <summary>
    /// Represents a Gas Station Statistics Class.
    /// The Stastics are getting calculated here.
    /// </summary>
    public class GasStationStatistics
    {
        GasStation gasStation;
        /// <summary>
        /// Initializes a GasStationStatistic
        /// </summary>
        /// <param name="gasStation">The GasStation</param>
        public GasStationStatistics(GasStation gasStation)
        {
            this.gasStation = gasStation;
        }
        /// <summary>
        /// Calculates the Sales
        /// </summary>
        /// <param name="endDate">Enddate of the Sales which should be calculated</param>
        /// <param name="startDate">Startdate of the Sales which should be calculated</param>
        /// <returns>The Sales of the given timespan</returns>
        public Statistic GetSales(DateTime startDate, DateTime endDate)
        {
            return new Statistic(gasStation.Receipts.Where(r => r.Date > startDate && r.Date < endDate));
        }


    }
}
