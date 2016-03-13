using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.Businesslogic
{
    [Serializable]
    public class GasPump
    {
        public List<GasTap> gasTaps = new List<GasTap>();
        private GasStation gasStation;
        private Tank tank;

        /// <summary>
        /// Initializes a GasPump
        /// </summary>
        /// <param name="gasStation">The relevant gas station</param>
        /// <param name="tank">The tank where the fuel should be drained</param>
        public GasPump(GasStation gasStation, Tank tank)
        {
            this.gasStation = gasStation;
            this.tank = tank;
        }
        /// <summary>
        /// Locks all the Gas Taps from this Gas Pump expect the given
        /// </summary>
        /// <param name="gasTap">The Gas Tap which shouldn't get locked</param>
        public void LockGasTapsExcept(GasTap gasTap)
        {
            gasTaps.Where(g => g != gasTap).ToList().ForEach(g => g.IsLocked = true);
        }
        /// <summary>
        /// Unlocks all the Gas Taps from this Gas Pump
        /// </summary>
        public void UnlockGasTaps()
        {
            gasTaps.ForEach(g => g.IsLocked = false);
        }
        /// <summary>
        /// Gives the relevant Tank back
        /// </summary>
        public Tank Tank
        {
            get
            {
                return tank;
            }
        }
    }
}
