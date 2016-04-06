using System;
using System.Text;
using System.Collections.Generic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GasStation.Businesslogic;
using Businesslogic;

namespace GasStation.Test
{
    /// <summary>
    /// Summary description for SequenzdiagramTest
    /// </summary>
    [TestClass]
    public class SequenzdiagramTest
    {
        

        [TestMethod]
        public void Sequenzdiagram()
        {
            Businesslogic.GasStation gasStation = new Businesslogic.GasStation("Jonas & Livios Tankstelle");
            GasPump gasPump = new GasPump(gasStation, "Gas Pump 1");
            Fuel fuel = new Fuel(5, "Petrol");
            Tank tank = new Tank(fuel, 10000, 100, gasStation);
            GasTap gasTap = new GasTap(tank, gasPump);
            PayStationCommunicator payStationCommunicator = new PayStationCommunicator(gasStation);
            


            using(GasTapTransaction gasTapTransaction = gasTap.Use())
            {
                gasTapTransaction.TankUp(300);
            }

            int moneyToPay = payStationCommunicator.TellGasTap(gasTap);

            while (payStationCommunicator.GetValueInput() < moneyToPay)
            {
                payStationCommunicator.InsertCoin(Coin.TewntyFrancs);
            }

            CostumerReturn costumerReturn = payStationCommunicator.AcceptValueInput();
        }
    }
}
