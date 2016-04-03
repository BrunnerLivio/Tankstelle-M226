using Businesslogic;
using System;
using System.Collections.Generic;

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
            gasTap.GasPump.LockAllGasTaps();
        }

        /// <summary>
        /// Gives back the used fuel in Milliliters while the transaction was running.
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
        /// <exception cref="Exception">When the Gastap of the Transaction is not in use</exception>
        public void TankUp(int amount)
        {
            if (gasTap.IsInUse)
            {
                usedFuel += amount;
                gasTap.Tank.AddFuel(amount * -1);
            }
            else
            {
                throw new Exception("Zapfsäule wird nicht benutzt. Wählen Sie zuerst eine Zapfsäule, bevor Sie Tanken.");
            }

        }

        /// <summary>
        /// Gets the current cost of the Transaction in Rappen
        /// </summary>
        public int Cost
        {
            get
            {
                return usedFuel * gasTap.Tank.Fuel.RappenPerMilliliters;
            }
        }
    }
}
