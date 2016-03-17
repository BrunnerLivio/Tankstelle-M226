using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GasStation.Businesslogic;
using System.Linq;

namespace GasStation.Test
{
    [TestClass]
    public class GasStationTest
    {
        Businesslogic.GasStation gasStation;
        Fuel petrolFuel;
        Fuel dieselFuel;
        Tank petrolTank;
        Tank dieselTank;
        GasTap gasTap;
        GasPump gasPump1;
        GasPump gasPump2;
        private void Init()
        {
            gasStation = new Businesslogic.GasStation();
            petrolFuel = new Fuel(5, "Petrol");
            dieselFuel = new Fuel(7, "Diesel");
            petrolTank = new Tank(petrolFuel, 1000);
            dieselTank = new Tank(dieselFuel, 4000);
            gasPump1 = new GasPump(gasStation);
            gasPump2 = new GasPump(gasStation);

            petrolTank.AddFuel(1000);
            dieselTank.AddFuel(4000);

            gasStation.Tanks.Add(petrolTank);
            gasStation.Tanks.Add(dieselTank);
            
            gasStation.GasPumps.Add(gasPump1);
            gasStation.GasPumps.Add(gasPump2);

            gasPump1.GasTaps.Add(new GasTap(dieselTank, gasPump1));
            gasPump1.GasTaps.Add(new GasTap(petrolTank, gasPump1));

            gasPump2.GasTaps.Add(new GasTap(dieselTank, gasPump2));
            gasPump2.GasTaps.Add(new GasTap(petrolTank, gasPump2));

        }
        [TestMethod]
        public void TestCase1()
        {
            Init();
            //Er wählt eine Zapfsäule der Tankstelle aus.
            GasPump selectedGasPump = gasStation.GasPumps.First();

            //Er wählt die Benzinsorte (Zapfhahn) und startet den Tankvorgang.
            GasTap selectedGasTap = selectedGasPump.GasTaps.Where(gt => gt.Tank.Fuel.Name == "Petrol").First();
            using(GasTapTransaction transaction = selectedGasTap.Use())
            {
                //Er schliesst den Tankvorgang ab und geht zur Kasse um den Betrag zu bezahlen.
                transaction.TankUp(300);
            }
            
        }
    }
}
