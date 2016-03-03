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
            Assert.Equals(tank.FilledCapacity, 100);


        }
    }
}
