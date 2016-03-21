using Businesslogic;
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
        private bool isPaid;
        private Receipt receipt;
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
        /// <summary>
        /// If the Transaction is Paid
        /// </summary>
        public bool IsPaid
        {
            get
            {
                return isPaid;
            }
        }
        /// <summary>
        /// Accepts the Transaction
        /// </summary>
        /// <returns>The Change</returns>
        public List<Coin> AcceptValueInput()
        {
            PayStation payStation = gasTap.GasPump.GasStation.PayStation;
            int inputtedMoney = payStation.GetValueInput();
            int cost = Cost;
            if (inputtedMoney < cost)
            {
                throw new Exception("Sie haben nicht genügend Geld eingworfen");
            }
            payStation.AcceptValueInput();
            isPaid = true;
            receipt = new Receipt(gasTap.GasPump.GasStation.Name, gasTap.Tank.Fuel.Name, gasTap.Tank.Fuel.RappenPerMilliliters, usedFuel, gasTap.GasPump.Name, inputtedMoney, DateTime.Now);
            receipt.Save();

            if(inputtedMoney > cost)
            {
                return payStation.GetChange(inputtedMoney - cost);
            }
            return new List<Coin>();
        }
        /// <summary>
        /// Returns the Receipt if the Transaction is Paid.
        /// </summary>
        /// <exception cref="Exception">When the Transaction is not paid</exception>
        /// <exception cref="Exception">When the receipt is not generated.</exception>
        /// <returns>The Receipt of the Transaction</returns>
        public Receipt GetReceipt()
        {
            if (!isPaid)
            {
                throw new Exception("Die Transaktion wurde noch nicht bezahlt");
            }
            if(receipt != null)
            {
                return receipt;
            }
            else
            {
                throw new Exception("Die Quittung wurde noch nicht generiert");
            }
        }
    }
}
