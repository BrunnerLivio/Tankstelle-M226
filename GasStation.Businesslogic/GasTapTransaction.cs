using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.Businesslogic
{
    public class GasTapTransaction : IDisposable
    {
        private GasTap gasTap;

        // Used Fuel in Milliliters
        private int usedFuel;
        /// <summary>
        /// Initializes a Transaction and locks the Gas Taps from the Gas Pump expect the given
        /// </summary>
        /// <param name="gasTap">The relevant Gas Tap for the transaction</param>
        public GasTapTransaction(GasTap gasTap)
        {
            this.gasTap = gasTap;
            gasTap.IsInUse = true;
            gasTap.GasPump.LockGasTapsExcept(gasTap);
        }

        public void Dispose()
        {
            gasTap.IsInUse = false;
            gasTap.GasPump.UnlockGasTaps();
        }

        /// <summary>
        /// Gives back the used fuel while the transaction was running.
        /// </summary>
        public int UsedFuel
        {
            get
            {
                return usedFuel;
            }
        }
        /// <summary>
        /// Use the Tank
        /// </summary>
        /// <param name="amount">Amount in milliliters</param>
        public void TankUp(int amount)
        {
            usedFuel += amount;
            gasTap.GasPump.Tank.AddFuel(amount * -1);
        }

        /// <summary>
        /// Gets the current cost of the Transaction in Rappen
        /// </summary>
        public int Cost
        {
            get
            {
                return usedFuel * gasTap.GasPump.Tank.Fuel.PricePerMilliliters;
            }
        }
    }
}
