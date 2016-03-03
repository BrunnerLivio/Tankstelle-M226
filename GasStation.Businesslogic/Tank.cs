using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GasStation.Businesslogic
{
    public class Tank
    {
        #region Membervariables
        private FuelType fuelType;
        //In Milliliter
        private int maxCapacity;
        //In Milliliter
        private int filledCapacity;
        #endregion
        /// <summary>
        /// Creates a new Tank with the given Fueltype and the Maximum Capacity
        /// </summary>
        /// <param name="fuelType">The type of the Tanks Fuel</param>
        /// <param name="maxCapacity">The Maximum Capacity of the Tank in Milliliters</param>
        public Tank(FuelType fuelType, int maxCapacity)
        {
            this.fuelType = fuelType;
            this.maxCapacity = maxCapacity;
        }
        /// <summary>
        /// Adds the given amount to the filled Capacity
        /// </summary>
        /// <remarks>
        /// Negative Values are allowed.
        /// If it should pass over the size of the Maximum Capacity of the Tank, it throws an Exception.
        /// </remarks>
        /// <param name="amount">The amount in Milliliters</param>
        public void AddFuel(int amount)
        {
            if (filledCapacity + amount <= maxCapacity)
            {
                filledCapacity += amount;
            }
            else
            {
                throw new Exception("Not enough Capacity in Tank", new Exception(String.Format("This Tank has a Capacity of {0}", this.maxCapacity)));
            }
        }
        #region Properties
        public int FilledCapacity
        {
            get
            {
                return filledCapacity;
            }
        }

        #endregion
    }
}
