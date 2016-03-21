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
        private string name;
        /// <summary>
        /// Initializes a GasPump
        /// </summary>
        /// <param name="gasStation">The relevant gas station</param>
        public GasPump(GasStation gasStation, string name)
        {
            this.name = name;
            this.gasStation = gasStation;
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
        /// Gives the GasTaps of this GasPump back.
        /// </summary>
        public List<GasTap> GasTaps
        {
            get
            {
                return gasTaps;
            }
        }

        /// <summary>
        /// Gibt den Anzeigenamen der GasPump zurück.
        /// </summary>
        public string Name
        {
            get
            {
                return name;
            }
        }
        /// <summary>
        /// Gibt die Gas Station zurück
        /// </summary>
        public GasStation GasStation
        {
            get
            {
                return gasStation;
            }
        }
    }
}
