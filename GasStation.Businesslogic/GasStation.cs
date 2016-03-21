using AnttittyFramework;
using Businesslogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.Businesslogic
{
    public class GasStation
    {
        #region Membervariables
        DbContext dbContext;
        List<GasPump> gasPumps = new List<GasPump>();
        List<Tank> tanks = new List<Tank>();
        List<Fuel> fuels = new List<Fuel>();
        #endregion
        #region Constructors
        public GasStation()
        {
            dbContext = new DbContext();
            tanks = dbContext.Load<Tank>();
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
        #endregion
    }
}
