using AnttittyFramework;
using Businesslogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.Businesslogic
{
    /// <summary>
    /// Represents one GasStation
    /// </summary>
    public class GasStation
    {
        #region Membervariables
        DbContext dbContext;
        List<GasPump> gasPumps = new List<GasPump>();
        List<Tank> tanks = new List<Tank>();
        List<Fuel> fuels = new List<Fuel>();
        PayStation payStation = new PayStation();
        private string name;
        #endregion
        #region Constructors
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
        }
        #endregion
        #region Methods
        #endregion
        #region Properties
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
        /// Starts a Paytransaction
        /// </summary>
        /// <param name="gasTapTransaction">The current Transaction</param>
        /// <returns>One PayStation</returns>
        public PayStation Pay(GasTapTransaction gasTapTransaction)
        {
            return payStation;
        }

        /// <summary>
        /// Gives the PayStation of the GasStation back.
        /// </summary>
        public PayStation PayStation
        {
            get
            {
                return payStation;
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
        #endregion
    }
}
