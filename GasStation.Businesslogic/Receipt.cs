using AnttittyFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.Businesslogic
{
    [Serializable]
    public class Receipt : DbItem
    {

        private string gasStationName;
        private string fuelName;
        private int rappenPerMilliliters;
        private int usedFuel;
        private string gasPumpName;
        private int givenMoney;
        private DateTime date;

        /// <summary>
        /// Initializes a Receipt
        /// </summary>
        /// <param name="gasStationName">Name of the GasStation</param>
        /// <param name="fuelName">Name of the used fuel</param>
        /// <param name="rappenPerMilliliters">Rappen per Milliliters of the fuel</param>
        /// <param name="usedFuel">The used fuel</param>
        /// <param name="gasPumpName">The Name of the Gas Pump</param>
        /// <param name="givenMoney">The given Money</param>
        /// <param name="date">The Date of the Transaction</param>
        public Receipt(string gasStationName, string fuelName, int rappenPerMilliliters, int usedFuel, string gasPumpName, int givenMoney, DateTime date)
        {
            this.gasStationName = gasStationName;
            this.fuelName = fuelName;
            this.rappenPerMilliliters = rappenPerMilliliters;
            this.usedFuel = usedFuel;
            this.gasPumpName = gasPumpName;
            this.givenMoney = givenMoney;
            this.date = date;
        }

        /// <summary>
        /// Gives the Cost back
        /// </summary>
        public int Cost
        {
            get
            {
                return rappenPerMilliliters * usedFuel;
            }

        }

        /// <summary>
        /// Gives the Date of the Receipt back
        /// </summary>
        public DateTime Date
        {
            get
            {
                return date;
            }
        }
        /// <summary>
        /// Gives the FormattedOutput of the receipt back
        /// </summary>
        public string FormattedOutput
        {
            get
            {
                string formattedOutput = String.Format("{0}\n\n\n", gasStationName);
                formattedOutput += String.Format("*{0}             {1} Fr\n", fuelName, (double)Cost / 100);
                formattedOutput += String.Format("*{0} l             {1} Fr/Liter\n", (double)usedFuel / 1000, (double)rappenPerMilliliters);
                formattedOutput += String.Format("gegeben          {0} Fr\n", (double)givenMoney / 100);
                formattedOutput += String.Format("Rückgeld         {0} Fr\n", (double)(givenMoney - Cost) / 100);
                formattedOutput += "\n\n\n";
                return formattedOutput;
            }
        }
        /// <summary>
        /// Returns the fuelname of the receipt
        /// </summary>
        public string FuelName
        {
            get
            {
                return fuelName;
            }
        }
        /// <summary>
        /// Gives the used fuel back
        /// </summary>
        public int UsedFuel
        {
            get
            {
                return usedFuel;
            }
        }
    }
}