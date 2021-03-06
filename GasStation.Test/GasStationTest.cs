﻿using System;
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
        GasPump gasPump1;
        GasPump gasPump2;
        GasPump selectedGasPump;
        GasTap selectedGasTap;
        private void Init()
        {
            gasStation = new Businesslogic.GasStation("Jonas & Livios Tankstelle");
            petrolFuel = new Fuel(5, "Petrol");
            dieselFuel = new Fuel(7, "Diesel");
            petrolTank = new Tank(petrolFuel, 1000, 100, gasStation);
            dieselTank = new Tank(dieselFuel, 4000, 100, gasStation);
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

            gasStation.PayStationCommunicators.Add(new PayStationCommunicator(gasStation));
            gasStation.PayStationCommunicators.Add(new PayStationCommunicator(gasStation));
            gasStation.PayStationCommunicators.Add(new PayStationCommunicator(gasStation));
            gasStation.PayStationCommunicators.Add(new PayStationCommunicator(gasStation));

            foreach (PayStationCommunicator payStationCommunicator in gasStation.PayStationCommunicators)
            {
                foreach(Coin coin in (Coin[])Enum.GetValues(typeof(Coin)))
                {
                    for (int i = 0; i < 100; i++)
                    {
                        payStationCommunicator.InsertCoinDirectly(coin);
                    }
                }
            }
            gasStation.DbContext.ClearDb();

        }
        [TestMethod]
        public void TestCase1()
        {
            Init();
            //Er wählt eine Zapfsäule der Tankstelle aus.
            GasPump selectedGasPump = gasStation.GasPumps.First();

            //Er wählt die Benzinsorte (Zapfhahn) und startet den Tankvorgang.
            GasTap selectedGasTap = selectedGasPump.GasTaps.Where(gt => gt.Tank.Fuel.Name == "Petrol").First();
            using (GasTapTransaction gasTapTransaction = selectedGasTap.Use())
            {
                //Er schliesst den Tankvorgang ab und geht zur Kasse um den Betrag zu bezahlen.
                gasTapTransaction.TankUp(300);

            }
            //Er teilt der Kassenperson mit welche Zapfsäule er verwendet hat.
            PayStationCommunicator selectedPayStationCommunicator = gasStation.PayStationCommunicators.First();
            //Die Kassenperson teilt ihm den ausstehenden Betrag mit 
            int moneyToPay = selectedPayStationCommunicator.TellGasTap(selectedGasTap);
            //und der Kunde bezahlt den Betrag.
            while (selectedPayStationCommunicator.GetValueInput() < moneyToPay)
            {
                selectedPayStationCommunicator.InsertCoin(Coin.TewntyFrancs);
            }
            //Wenn der Kunde einen höheren Betrag dem Kassenpersonal übergibt als verlangt, so gibt die Person das Rückgeld zurück.
            //Dem Kunden wird einen Quittung ausgehändigt.
            CostumerReturn costumerReturn = selectedPayStationCommunicator.AcceptValueInput();

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
            GasPump selectedGasPump = gasStation.GasPumps.First();

            GasTap selectedGasTap = selectedGasPump.GasTaps.Where(gt => gt.Tank.Fuel.Name == "Petrol").First();
            using (GasTapTransaction gasTapTransaction = selectedGasTap.Use())
            {
                foreach (GasTap gasTap in selectedGasPump.GasTaps)
                {
                    if (gasTap != selectedGasTap)
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
            GasPump selectedGasPump = gasStation.GasPumps.First();

            GasTap selectedGasTap = selectedGasPump.GasTaps.Where(gt => gt.Tank.Fuel.Name == "Petrol").First();
            using (GasTapTransaction gasTapTransaction = selectedGasTap.Use())
            {
                gasTapTransaction.TankUp(300);
                Assert.AreEqual(300, gasTapTransaction.UsedFuel);
                Assert.AreEqual(1500, gasTapTransaction.Cost);
            }


        }
        [TestMethod]
        public void Punkt7()
        {
            Init();
            GasPump selectedGasPump = gasStation.GasPumps.First();

            GasTap selectedGasTap = selectedGasPump.GasTaps.Where(gt => gt.Tank.Fuel.Name == "Petrol").First();
            using (GasTapTransaction gasTapTransaction = selectedGasTap.Use())
            {
                gasTapTransaction.TankUp(300);
            }
            PayStationCommunicator selectedPayStation = gasStation.PayStationCommunicators.First();
            Assert.IsTrue(selectedGasTap.IsLocked);

            int moneyToPay = selectedPayStation.TellGasTap(selectedGasTap);
            selectedPayStation.InsertCoin(Coin.TenFrancs);
            while (selectedPayStation.GetValueInput() < moneyToPay)
            {
                selectedPayStation.InsertCoin(Coin.FiveFrancs);
            }

            Assert.IsTrue(selectedGasTap.IsLocked);
            selectedPayStation.AcceptValueInput();
            Assert.IsFalse(selectedGasTap.IsLocked);

        }


        [TestMethod]
        public void Punkt8()
        {
            Init();
            GasPump selectedGasPump = gasStation.GasPumps.First();

            GasTap selectedGasTap = selectedGasPump.GasTaps.Where(gt => gt.Tank.Fuel.Name == "Petrol").First();
            PayStationCommunicator selectedPayStation = gasStation.PayStationCommunicators.First();

            Assert.IsFalse(selectedPayStation.HasGasTapOpenTransaction);
            using (GasTapTransaction gasTapTransaction = selectedGasTap.Use())
            {
                gasTapTransaction.TankUp(300);

            }
            int moneyToPay = selectedPayStation.TellGasTap(selectedGasTap);

            Assert.IsTrue(selectedPayStation.HasGasTapOpenTransaction);
        }

        [TestMethod]
        public void Punkt9()
        {
            Init();
            GasPump selectedGasPump = gasStation.GasPumps.First();

            GasTap selectedGasTap = selectedGasPump.GasTaps.Where(gt => gt.Tank.Fuel.Name == "Petrol").First();
            using (GasTapTransaction gasTapTransaction = selectedGasTap.Use())
            {
                gasTapTransaction.TankUp(300);
            }
            PayStationCommunicator selectedPayStation = gasStation.PayStationCommunicators.First();
            Assert.IsTrue(selectedGasTap.IsLocked);

            selectedPayStation.TellGasTap(selectedGasTap);

        }
        [TestMethod]
        public void Punkt10()
        {
            Init();
            GasPump selectedGasPump = gasStation.GasPumps.First();

            GasTap selectedGasTap = selectedGasPump.GasTaps.Where(gt => gt.Tank.Fuel.Name == "Petrol").First();
            using (GasTapTransaction gasTapTransaction = selectedGasTap.Use())
            {
                gasTapTransaction.TankUp(300);
            }
            PayStationCommunicator selectedPayStation = gasStation.PayStationCommunicators.First();

            int moneyToPay = selectedPayStation.TellGasTap(selectedGasTap);
            Assert.AreEqual(1500, moneyToPay);

        }

        [TestMethod]
        public void Punkt11()
        {
            Init();
            GasPump selectedGasPump = gasStation.GasPumps.First();

            GasTap selectedGasTap = selectedGasPump.GasTaps.Where(gt => gt.Tank.Fuel.Name == "Petrol").First();
            using (GasTapTransaction gasTapTransaction = selectedGasTap.Use())
            {
                gasTapTransaction.TankUp(300);
            }
            PayStationCommunicator selectedPayStation = gasStation.PayStationCommunicators.First();

            int moneyToPay = selectedPayStation.TellGasTap(selectedGasTap);
            selectedPayStation.InsertCoin(Coin.TenFrancs);
            while (selectedPayStation.GetValueInput() < moneyToPay)
            {
                selectedPayStation.InsertCoin(Coin.FiveFrancs);
            }
            selectedPayStation.InsertCoin(Coin.FiveFrancs);


            CostumerReturn costumerReturn = selectedPayStation.AcceptValueInput();
            Assert.AreEqual(500, costumerReturn.Change.Sum(c => (int)c));
        }

        [TestMethod]
        public void Punkt12()
        {
            Init();
            GasPump selectedGasPump = gasStation.GasPumps.First();

            GasTap selectedGasTap = selectedGasPump.GasTaps.Where(gt => gt.Tank.Fuel.Name == "Petrol").First();
            using (GasTapTransaction gasTapTransaction = selectedGasTap.Use())
            {
                gasTapTransaction.TankUp(300);
            }
            PayStationCommunicator selectedPayStation = gasStation.PayStationCommunicators.First();

            int moneyToPay = selectedPayStation.TellGasTap(selectedGasTap);
            selectedPayStation.InsertCoin(Coin.TenFrancs);
            while (selectedPayStation.GetValueInput() < moneyToPay)
            {
                selectedPayStation.InsertCoin(Coin.FiveFrancs);
            }
            selectedPayStation.InsertCoin(Coin.FiveFrancs);


            CostumerReturn costumerReturn = selectedPayStation.AcceptValueInput();
            Assert.AreEqual(300, costumerReturn.Receipt.UsedFuel);
            Assert.AreEqual(1500, costumerReturn.Receipt.Cost);
            Assert.AreEqual("Petrol", costumerReturn.Receipt.FuelName);
            Assert.AreEqual(DateTime.Now.ToString("dd.MM.yyyy"), costumerReturn.Receipt.FormattedDate);
            Assert.AreEqual(DateTime.Now.ToString("hh:mm"), costumerReturn.Receipt.FormattedTime);
        }


        [TestMethod]
        public void Punkt14()
        {
            Init();
            GasPump selectedGasPump = gasStation.GasPumps.First();

            GasTap selectedGasTap = selectedGasPump.GasTaps.Where(gt => gt.Tank.Fuel.Name == "Petrol").First();
            Assert.IsNull(gasStation.LastTimeTankMinimumReached);
            using (GasTapTransaction gasTapTransaction = selectedGasTap.Use())
            {
                gasTapTransaction.TankUp(950);
            }
            Assert.IsNotNull(gasStation.LastTimeTankMinimumReached);
        }

        [TestMethod]
        public void Punkt13()
        {
            Punkt7();
            Punkt7();
        }

        [TestMethod]
        public void Punkt15()
        {
            Init();
            Tank tank = gasStation.Tanks.Where(t => t.Fuel.Name == "Diesel").First();
            Assert.AreEqual(4000, tank.FilledCapacity);
            Receipt receipt = new Receipt("Livio & Jonas Tankstelle", "Diesel", 2, 1000, "Gas Pump 1", 5000, DateTime.Now.AddYears(-1));
            receipt.Save();
            receipt = new Receipt("Livio & Jonas Tankstelle", "Diesel", 2, 500, "Gas Pump 1", 5000, DateTime.Now.AddYears(-1));
            receipt.Save();
            receipt = new Receipt("Livio & Jonas Tankstelle", "Diesel", 2, 1000, "Gas Pump 1", 5000, DateTime.Now.AddYears(-1));
            receipt.Save();


            GasPump selectedGasPump = gasStation.GasPumps.First();

            GasTap selectedGasTap = selectedGasPump.GasTaps.Where(gt => gt.Tank.Fuel.Name == "Diesel").First();
            Assert.IsNull(gasStation.LastTimeTankMinimumReached);
            using (GasTapTransaction gasTapTransaction = selectedGasTap.Use())
            {
                gasTapTransaction.TankUp(2500);
            }

            Assert.AreEqual(1500, tank.FilledCapacity);

            gasStation.RefillTanks();
            Assert.AreEqual(2500, tank.FilledCapacity);

        }

        [TestMethod]
        public void Punkt16()
        {
            Init();
            Receipt receipt = new Receipt("Livio & Jonas Tankstelle", "Diesel", 2, 300, "Gas Pump 1", 5000, DateTime.Now);
            receipt.Save();
            receipt = new Receipt("Livio & Jonas Tankstelle", "Petrol", 3, 500, "Gas Pump 1", 20000, DateTime.Now.AddDays(-5));
            receipt.Save();
            receipt = new Receipt("Livio & Jonas Tankstelle", "Petrol", 3, 1000, "Gas Pump 1", 10000, DateTime.Now.AddDays(-10));
            receipt.Save();

            receipt = new Receipt("Livio & Jonas Tankstelle", "Diesel", 2, 1000, "Gas Pump 1", 5000, DateTime.Now.AddMonths(-3));
            receipt.Save();

            //Day
            Statistic statistic = gasStation.GasStationStatistics.GetSales(DateTime.Today, DateTime.Today.AddDays(1));
            Assert.AreEqual(600, statistic.Sales);
            Assert.AreEqual(1, statistic.FuelStatistics.Count());
            Assert.AreEqual(300, statistic.FuelStatistics.Where(fs => fs.Name == "Diesel").First().UsedFuel);
            //Week
            statistic = gasStation.GasStationStatistics.GetSales(DateTime.Today.AddDays(-7), DateTime.Now);
            Assert.AreEqual(2100, statistic.Sales);
            Assert.AreEqual(2, statistic.FuelStatistics.Count());
            Assert.AreEqual(300, statistic.FuelStatistics.Where(fs => fs.Name == "Diesel").First().UsedFuel);
            Assert.AreEqual(500, statistic.FuelStatistics.Where(fs => fs.Name == "Petrol").First().UsedFuel);
            //Month
            statistic = gasStation.GasStationStatistics.GetSales(DateTime.Now.AddMonths(-1), DateTime.Now);
            Assert.AreEqual(5100, statistic.Sales);
            Assert.AreEqual(2, statistic.FuelStatistics.Count());
            Assert.AreEqual(300, statistic.FuelStatistics.Where(fs => fs.Name == "Diesel").First().UsedFuel);
            Assert.AreEqual(1500, statistic.FuelStatistics.Where(fs => fs.Name == "Petrol").First().UsedFuel);
            //Year
            statistic = gasStation.GasStationStatistics.GetSales(DateTime.Now.AddYears(-1), DateTime.Now);
            Assert.AreEqual(7100, statistic.Sales);
            Assert.AreEqual(2, statistic.FuelStatistics.Count());
            Assert.AreEqual(1300, statistic.FuelStatistics.Where(fs => fs.Name == "Diesel").First().UsedFuel);
            Assert.AreEqual(1500, statistic.FuelStatistics.Where(fs => fs.Name == "Petrol").First().UsedFuel);
        }
    }
}
