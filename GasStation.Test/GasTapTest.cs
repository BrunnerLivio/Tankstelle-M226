using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using GasStation.Businesslogic;

namespace GasStation.Test
{
    [TestClass]
    public class GasTapTest
    {

        GasPump gasPump;
        Fuel petrolFuel = new Fuel(2, "Petrol");
        Fuel dieselFuel = new Fuel(3, "Diesel");
        private void Init()
        {
            Businesslogic.GasStation gasStation = new Businesslogic.GasStation();

            Tank tank = new Tank(petrolFuel, 5000);
            while(tank.FilledCapacity < tank.MaxCapacity)
            {
                tank.AddFuel(1000);
            }
            gasPump = new GasPump(gasStation);
            gasPump.gasTaps.Add(new GasTap(tank, gasPump));
            gasPump.gasTaps.Add(new GasTap(tank, gasPump));


        }
        [TestMethod]
        [ExpectedException(typeof(Exception))]
        public void UseLockedGasTap()
        {

            Init();


            using (gasPump.gasTaps[0].Use())
            {
                Assert.IsFalse(gasPump.gasTaps[0].IsLocked);
                Assert.IsTrue(gasPump.gasTaps[0].IsInUse);

                Assert.IsTrue(gasPump.gasTaps[1].IsLocked);
                Assert.IsFalse(gasPump.gasTaps[1].IsInUse);
                gasPump.gasTaps[1].Use();
            }
        }

        [TestMethod]
        public void GasTapTransaction()
        {
            Init();
            GasTap selectedGasTap = gasPump.gasTaps[0];
            using (GasTapTransaction transaction = selectedGasTap.Use())
            {

                Assert.AreEqual(5000, selectedGasTap.Tank.FilledCapacity);

                while (transaction.UsedFuel < 1000)
                {
                    transaction.TankUp(10);
                }

                Assert.AreEqual(1000, transaction.UsedFuel);

                Assert.AreEqual(4000, selectedGasTap.Tank.FilledCapacity);

                Assert.AreEqual(2 * 1000, transaction.Cost);
            }
        }
    }
}
