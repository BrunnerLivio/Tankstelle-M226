using AnttittyFramework;
using Businesslogic;
using GasStation.Businesslogic.Statistic;
using System.Collections.Generic;
using System;
using System.Linq;

namespace GasStation.Businesslogic
{
    /// <summary>
    /// Represents one GasStation
    /// </summary>
    public class GasStation
    {
        private DbContext dbContext;
        private List<GasPump> gasPumps = new List<GasPump>();
        private List<Tank> tanks = new List<Tank>();
        private List<Fuel> fuels = new List<Fuel>();
        private List<PayStationCommunicator> payStationCommunicators = new List<PayStationCommunicator>();
        private List<Receipt> receipts = new List<Receipt>();
        private GasStationStatistics gasStationStatistics;
        private string name;

        /// <summary>
        /// Initializies a GasStation
        /// </summary>
        /// <param name="name">Name of the GasStation</param>
        public GasStation(string name)
        {
            dbContext = new DbContext();
            tanks = dbContext.Load<Tank>();
            this.name = name;
            gasPumps = dbContext.Load<GasPump>();
            gasStationStatistics = new GasStationStatistics(this);
            
        }

        /// <summary>
        /// Gives the DbContext back.
        /// </summary>
        public DbContext DbContext
        {
            get
            {
                return dbContext;
            }
        }
        /// <summary>
        /// Refills the Tanks with the amount which was used in the last month
        /// </summary>
        public void RefillTanks()
        {
            DateTime lastYear = DateTime.Now.AddYears(-1);
            DateTime endOfTheMonthLastYear = new DateTime(lastYear.Year, DateTime.Today.Month, DateTime.DaysInMonth(lastYear.Year, DateTime.Today.Month));
            DateTime startOfTheMonthLastYear = new DateTime(lastYear.Year, DateTime.Today.Month, 1);
            Statistic.Statistic statistic = gasStationStatistics.GetSales(startOfTheMonthLastYear.AddDays(-1), endOfTheMonthLastYear.AddDays(1));
            foreach(Tank tank in tanks)
            {
                FuelStatistic fuelStatistic = statistic.FuelStatistics.Where(fs => fs.Name == tank.Fuel.Name).FirstOrDefault();
                if (fuelStatistic != null)
                {
                    if (fuelStatistic.UsedFuel > tank.FilledCapacity)
                    {
                        tank.AddFuel(fuelStatistic.UsedFuel - tank.FilledCapacity);
                    }
                }
                else
                {
                    tank.AddFuel(tank.MaxCapacity - tank.FilledCapacity);
                }
            }
        }
        /// <summary>
        /// Gets all Tanks of the Gas Station
        /// </summary>
        public List<Tank> Tanks
        {
            get
            {
                return tanks;
            }
        }
        /// <summary>
        /// Gets all Gas Pumps of the Gas Station
        /// </summary>
        public List<GasPump> GasPumps
        {
            get
            {
                return gasPumps;
            }
        }
        /// <summary>
        /// Gives back the last time the Tank minimum was reached
        /// </summary>
        public DateTime? LastTimeTankMinimumReached { get; private set; }
        /// <summary>
        /// Notifies the Tankwart
        /// </summary>
        /// <param name="tank">The Tank which has reached the minimum</param>
        internal void NotifyTankMinimumReached(Tank tank)
        {
            LastTimeTankMinimumReached = DateTime.Now;
            //SEND MAIL
        }

        /// <summary>
        /// Gives the PayStationCommunicators of the GasStation back.
        /// </summary>
        public List<PayStationCommunicator> PayStationCommunicators
        {
            get
            {
                return payStationCommunicators;
            }
        }
        /// <summary>
        /// Gives back the Name of the Gas Station
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }
        public List<Receipt> Receipts
        {
            get
            {
                return dbContext.Load<Receipt>();
            }
        }
        
        public GasStationStatistics GasStationStatistics
        {
            get
            {
                return gasStationStatistics;
            }
        }
    }
}
