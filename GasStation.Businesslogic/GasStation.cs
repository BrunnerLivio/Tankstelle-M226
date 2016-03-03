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
        List<GasPump> gasPumps = new List<GasPump>();
        List<Tank> tanks = new List<Tank>();
        #endregion
        #region Constructors

        #endregion
        #region Methods
#if DEBUG
        public void Initialize()
        {
            foreach (FuelType fuelType in Enum.GetValues(typeof(FuelType)))
            {
                //100 Liter
                Tank tank = new Tank(fuelType, 100000);
                tanks.Add(tank);
                //50 Liter
                tank.AddFuel(50000);
            }
            for (int i = 0; i < 10; i++)
            {
                gasPumps.Add(new GasPump());
            }
        }
#endif
        #endregion
        #region Properties
        
        #endregion
    }
}
