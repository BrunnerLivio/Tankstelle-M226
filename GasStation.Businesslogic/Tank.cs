using AnttittyFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GasStation.Businesslogic
{
    [Serializable]
    public class Tank : DbItem
    {
        #region Membervariables
        private Fuel fuel;
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
        public Tank(Fuel fuel, int maxCapacity)
        {
            this.fuel = fuel;
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
        /// <summary>
        /// Gives the filled Capacity back in Milliliters
        /// </summary>
        public int FilledCapacity
        {
            get
            {
                return filledCapacity;
            }
        }
        /// <summary>
        /// Gives the maximal Capacity back in Milliliters
        /// </summary>
        public int MaxCapacity
        {
            get
            {
                return maxCapacity;
            }
        }
        /// <summary>
        /// Gets the Fuel of the Tank
        /// </summary>
        public Fuel Fuel
        {
            get
            {
                return fuel;
            }
        }
        #endregion

    }
}
