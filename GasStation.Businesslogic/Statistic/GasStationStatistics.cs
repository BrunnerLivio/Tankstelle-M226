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
        /// Calculates the Sales of the last year
        /// </summary>
        /// <returns>
        /// The Sales of the last year in rappen.
        /// </returns>
        public Statistic GetSalesOfLastYear()
        {
            return new Statistic(gasStation.Receipts.Where(r => r.Date > DateTime.Now.AddYears(-1) && r.Date < DateTime.Now));
        }

        /// <summary>
        /// Calculates the Sales of the last month
        /// </summary>
        /// <returns>
        /// The Sales of the last month in rappen.
        /// </returns>
        public Statistic GetSalesOfLastMonth()
        {
            return new Statistic(gasStation.Receipts.Where(r => r.Date > DateTime.Now.AddMonths(-1) && r.Date < DateTime.Now));
        }
        /// <summary>
        /// Calculates the Sales of the last week
        /// </summary>
        /// <returns>
        /// The Sales of the last week in rappen.
        /// </returns>
        public Statistic GetSalesOfLastWeek()
        {
            return new Statistic(gasStation.Receipts.Where(r => r.Date > DateTime.Today.AddDays(-7) && r.Date < DateTime.Today.AddDays(1)));
        }

        /// <summary>
        /// Calculates the Sales of the day
        /// </summary>
        /// <returns>
        /// The Sales of the day in rappen.
        /// </returns>
        public Statistic GetSalesOfTheDay()
        {
            return new Statistic(gasStation.Receipts.Where(r => r.Date > DateTime.Today && r.Date < DateTime.Today.AddDays(1)));
        }
    }
}
