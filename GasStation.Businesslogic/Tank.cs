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
        //In Milliliter
        private int minimumCapacity;
        [NonSerialized]
        private GasStation gasStation;
        #endregion
        /// <summary>
        /// Creates a new Tank with the given Fueltype and the Maximum Capacity
        /// </summary>
        /// <param name="fuelType">The type of the Tanks Fuel</param>
        /// <param name="maxCapacity">The Maximum Capacity of the Tank in Milliliters</param>
        /// <param name="minimumCapacity">The Minimum Capacity of the Tank in Milliliters</param>
        /// <param name="gasStation">The GasStation</param>
        public Tank(Fuel fuel, int maxCapacity, int minimumCapacity, GasStation gasStation)
        {
            this.fuel = fuel;
            this.maxCapacity = maxCapacity;
            this.minimumCapacity = minimumCapacity;
            this.gasStation = gasStation;
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
                if(minimumCapacity >= amount)
                {
                    gasStation.NotifyTankMinimumReached(this);
                }
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
