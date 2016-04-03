using Businesslogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.Businesslogic
{
    /// <summary>
    /// Represents a PayStationCommunicator, which Communicates from the GasStation to the PayStation
    /// </summary>
    public class PayStationCommunicator : PayStation
    {
        GasTap selectedGasTap;
        GasStation gasStation;

        /// <summary>
        /// Initializes a new PayStation Communicator
        /// </summary>
        /// <param name="payStation">The GasStation</param>
        public PayStationCommunicator(GasStation gasStation)
        {
            this.gasStation = gasStation;
        }
        /// <summary>
        /// Tells the Pay Station which Gas Tap the costumer wants to pay
        /// </summary>
        /// <param name="selectedGasTap">The Gas Tap the costumer wants to pay</param>
        /// <returns>The Cost of the Transaction</returns>
        public int TellGasTap(GasTap selectedGasTap)
        {
            this.selectedGasTap = selectedGasTap;
            return selectedGasTap.CurrentGasTapTransaction.Cost;
        }

        /// <summary>
        /// Accepts the given Money Input 
        /// </summary>
        /// <exception cref="Exception">
        /// When you have not inputted enough money
        /// </exception>
        /// <returns>
        /// Change
        /// </returns>
        public new CustomerReturn AcceptValueInput()
        {
            if(GetValueInput() < selectedGasTap.CurrentGasTapTransaction.Cost)
            {
                throw new Exception("Sie haben zu wenig Geld eingeworfen");
            }
            List<Coin> change = GetChange(this.GetValueInput() - selectedGasTap.CurrentGasTapTransaction.Cost);
            Receipt receipt = new Receipt(gasStation.Name, 
                                          selectedGasTap.Tank.Fuel.Name, 
                                          selectedGasTap.Tank.Fuel.RappenPerMilliliters, 
                                          selectedGasTap.CurrentGasTapTransaction.UsedFuel, 
                                          selectedGasTap.GasPump.Name, GetValueInput(), 
                                          DateTime.Now);
            receipt.Save();
            base.AcceptValueInput();
            selectedGasTap.GasPump.UnlockGasTaps();
            selectedGasTap.CompleteTransaction();
            return new CustomerReturn(receipt ,change);
        }

    }
}
