using AnttittyFramework;
using GasStation.Businesslogic;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.Test
{
    [TestClass]
    public class TankTest
    {
        [TestMethod]
        public void AddFuelTest()
        {
            Tank tank = new Tank(FuelType.Petrol, 1000);
            tank.AddFuel(100);
            Assert.AreEqual(100, tank.FilledCapacity);
        }

        [TestMethod]
        public void SaveTankTest()
        {
            Tank tank = new Tank(FuelType.Petrol, 1000);
            tank.AddFuel(100);
            tank.Save();
        }
        
        [TestMethod]
        public void LoadTankTest()
        {
            DbContext dbContext = new DbContext();
            var a = dbContext.Load<Tank>();
        }

        [TestMethod]
        public void DeleteTankTest()
        {
            DbContext dbContext = new DbContext();
            Tank tank = new Tank(FuelType.Diesel, 3000);
            tank.Save();
            int oldTankCount = dbContext.Load<Tank>().Count;
            tank.Remove();

            Assert.AreEqual(dbContext.Load<Tank>().Count, oldTankCount - 1);



        }
    }
}
