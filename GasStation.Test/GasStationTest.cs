using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GasStation.Businesslogic;
using System.Linq;
using Businesslogic;
using System.Collections.Generic;

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
            gasStation = new Businesslogic.GasStation("Jonas & Livios Tankstelle");
            petrolFuel = new Fuel(5, "Petrol");
            dieselFuel = new Fuel(7, "Diesel");
            petrolTank = new Tank(petrolFuel, 1000);
            dieselTank = new Tank(dieselFuel, 4000);
            gasPump1 = new GasPump(gasStation, "Gas Pump 1");
            gasPump2 = new GasPump(gasStation, "Gas Pump 2");

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
            using(GasTapTransaction gasTapTransaction = selectedGasTap.Use())
            {
                //Er schliesst den Tankvorgang ab und geht zur Kasse um den Betrag zu bezahlen.
                gasTapTransaction.TankUp(300);
                //Er teilt der Kassenperson mit welche Zapfsäule er verwendet hat.
                PayStation paySation = gasStation.Pay(gasTapTransaction);
                //Die Kassenperson teilt ihm den ausstehenden Betrag mit und der Kunde bezahlt den Betrag.
                while(paySation.GetValueInput() < gasTapTransaction.Cost)
                {
                    paySation.InsertCoin(Coin.TenFrancs);
                }
                //Wenn der Kunde einen höheren Betrag dem Kassenpersonal übergibt als verlangt, so gibt die Person das Rückgeld zurück.
                List<Coin> change =  gasTapTransaction.AcceptValueInput();
                Assert.AreEqual((int)Coin.FiveFrancs, change.Sum(c => (int)c));
                //Dem Kunden wird einen Quittung ausgehändigt.
                Receipt receipt = gasTapTransaction.GetReceipt();
            }
            
        }
    }
}
