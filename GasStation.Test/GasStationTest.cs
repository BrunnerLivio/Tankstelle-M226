using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GasStation.Businesslogic;
using System.Linq;
using Businesslogic;
using System.Collections.Generic;
using GasStation.Businesslogic.Statistic;

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
        GasPump selectedGasPump;
        GasTap selectedGasTap;
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
            gasStation.DbContext.ClearDb();
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
       [TestMethod]
        public void Punkt1()
        {
            Init();
            selectedGasPump = gasStation.GasPumps.First();
            Assert.AreEqual("Gas Pump 1", selectedGasPump.Name);
        }
        [TestMethod]
        public void Punkt2()
        {
            Punkt1();
            selectedGasTap = selectedGasPump.GasTaps.Where(gt => gt.Tank.Fuel.Name == "Diesel").FirstOrDefault();
            Assert.IsNotNull(selectedGasTap);
        }

        [TestMethod]
        public void Punkt3()
        {
            Punkt2();
            Assert.AreEqual(70, selectedGasTap.Tank.Fuel.FrankenPerLiter);
        }
        [TestMethod]
        public void Punkt4()
        {
            Init();
            gasStation.DbContext.ClearDb();
            GasPump selectedGasPump = gasStation.GasPumps.First();

            GasTap selectedGasTap = selectedGasPump.GasTaps.Where(gt => gt.Tank.Fuel.Name == "Petrol").First();
            using (GasTapTransaction gasTapTransaction = selectedGasTap.Use())
            {
                Assert.AreEqual(0, gasTapTransaction.UsedFuel);
                Assert.AreEqual(0, gasTapTransaction.Cost);
            }
        }

        [TestMethod]
        public void Punkt5()
        {
            Init();
            gasStation.DbContext.ClearDb();
            GasPump selectedGasPump = gasStation.GasPumps.First();

            GasTap selectedGasTap = selectedGasPump.GasTaps.Where(gt => gt.Tank.Fuel.Name == "Petrol").First();
            using (GasTapTransaction gasTapTransaction = selectedGasTap.Use())
            {
                foreach(GasTap gasTap in selectedGasPump.GasTaps)
                {
                    if(gasTap != selectedGasTap)
                    {
                        Assert.IsTrue(gasTap.IsLocked);
                        Assert.IsFalse(gasTap.IsInUse);
                    }
                    else
                    {
                        Assert.IsFalse(gasTap.IsLocked);
                        Assert.IsTrue(gasTap.IsInUse);
                    }
                }
            }
        }

        [TestMethod]
        public void Punkt6()
        {
            Init();
            gasStation.DbContext.ClearDb();
            GasPump selectedGasPump = gasStation.GasPumps.First();

            GasTap selectedGasTap = selectedGasPump.GasTaps.Where(gt => gt.Tank.Fuel.Name == "Petrol").First();
            using (GasTapTransaction gasTapTransaction = selectedGasTap.Use())
            {
                Assert.AreEqual(0, gasTapTransaction.UsedFuel);
                Assert.AreEqual(0, gasTapTransaction.Cost);
                gasTapTransaction.TankUp(300);
                Assert.AreEqual(300, gasTapTransaction.UsedFuel);
                Assert.AreEqual(1500 ,gasTapTransaction.Cost);
            }
        }

        [TestMethod]
        public void Punkt16()
        {
            Init();
            gasStation.DbContext.ClearDb();
            Receipt receipt = new Receipt("Livio & Jonas Tankstelle", "Diesel", 2, 300, "Gas Pump 1", 5000, DateTime.Now);
            receipt.Save();
            receipt = new Receipt("Livio & Jonas Tankstelle", "Petrol", 3, 500, "Gas Pump 1", 20000, DateTime.Now.AddDays(-5));
            receipt.Save();
            receipt = new Receipt("Livio & Jonas Tankstelle", "Petrol", 3, 1000, "Gas Pump 1", 10000, DateTime.Now.AddDays(-10));
            receipt.Save();

            receipt = new Receipt("Livio & Jonas Tankstelle", "Diesel", 2, 1000, "Gas Pump 1", 5000, DateTime.Now.AddMonths(-3));
            receipt.Save();

            //Day
            Statistic statistic = gasStation.GasStationStatistics.GetSalesOfTheDay();
            Assert.AreEqual(600, statistic.Sales);
            Assert.AreEqual(1, statistic.FuelStatistics.Count());
            Assert.AreEqual(300, statistic.FuelStatistics.Where(fs => fs.Name == "Diesel").First().UsedFuel);
            //Week
            statistic = gasStation.GasStationStatistics.GetSalesOfLastWeek();
            Assert.AreEqual(2100, statistic.Sales);
            Assert.AreEqual(2, statistic.FuelStatistics.Count());
            Assert.AreEqual(300, statistic.FuelStatistics.Where(fs => fs.Name == "Diesel").First().UsedFuel);
            Assert.AreEqual(500, statistic.FuelStatistics.Where(fs => fs.Name == "Petrol").First().UsedFuel);
            //Month
            statistic = gasStation.GasStationStatistics.GetSalesOfLastMonth();
            Assert.AreEqual(5100, statistic.Sales);
            Assert.AreEqual(2, statistic.FuelStatistics.Count());
            Assert.AreEqual(300, statistic.FuelStatistics.Where(fs => fs.Name == "Diesel").First().UsedFuel);
            Assert.AreEqual(1500, statistic.FuelStatistics.Where(fs => fs.Name == "Petrol").First().UsedFuel);
            //Year
            statistic = gasStation.GasStationStatistics.GetSalesOfLastYear();
            Assert.AreEqual(7100, statistic.Sales);
            Assert.AreEqual(2, statistic.FuelStatistics.Count());
            Assert.AreEqual(1300, statistic.FuelStatistics.Where(fs => fs.Name == "Diesel").First().UsedFuel);
            Assert.AreEqual(1500, statistic.FuelStatistics.Where(fs => fs.Name == "Petrol").First().UsedFuel);
        }
    }
}
