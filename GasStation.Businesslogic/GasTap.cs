using AnttittyFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.Businesslogic
{
    /// <summary>
    /// Represents one Gas Tap
    /// </summary>
    [Serializable]
    public class GasTap : DbItem
    {
        [NonSerialized]
        private GasPump gasPump;
        private bool isLocked = false;
        private Tank tank;
        private bool isInUse;
        /// <summary>
        /// Initializes a GasTap
        /// </summary>
        /// <param name="tank">The tank of the Gas Tap, where the fuel should be drained </param>
        /// <param name="gasPump">The Gas Pump which the GasTap belongs</param>
        public GasTap(Tank tank, GasPump gasPump)
        {
            this.tank = tank;
            this.gasPump = gasPump;
        }
        /// <summary>
        /// Gives back if the Gas Tap is locked and sets it
        /// </summary>
        public bool IsLocked
        {
            get
            {
                return isLocked;
            }
            set
            {
                isLocked = value;
            }
        }

        /// <summary>
        /// Gives back if the GasTap is in use
        /// </summary>
        public bool IsInUse
        {
            get
            {
                return isInUse;
            }
            set
            {
                isInUse = value;
            }
        }
        /// <summary>
        /// Uses the Gas Tap and Locks all the other Gas Taps from this GasPump
        /// </summary>
        /// <exception cref="Exception">When the Gas Tap is locked</exception>
        public GasTapTransaction Use()
        {
            if (isLocked)
            {
                throw new Exception("Gas Tap is locked");
            }
            
            return new GasTapTransaction(this);
        }
        
        /// <summary>
        /// Gets the parent gas pump
        /// </summary>
        public GasPump GasPump
        {
            get
            {
                return gasPump;
            }
        }
        public Tank Tank
        {
            get
            {
                return tank;
            }
        }
    }
}
