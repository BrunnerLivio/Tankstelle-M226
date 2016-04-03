using AnttittyFramework;
using Businesslogic;
using GasStation.Businesslogic.Statistic;
using System.Collections.Generic;

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
