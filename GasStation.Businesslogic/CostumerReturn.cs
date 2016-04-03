using Businesslogic;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GasStation.Businesslogic
{
    /// <summary>
    /// Represents one Customer Return
    /// </summary>
    public class CostumerReturn
    {
        /// <summary>
        /// Initializes a Costumer Return
        /// </summary>
        /// <param name="receipt">The Receipt the Costumer should receive</param>
        /// <param name="change">The Change the Costumer should get</param>
        public CostumerReturn(Receipt receipt, List<Coin> change)
        {
            Receipt = receipt;
            Change = change;
        }
        /// <summary>
        /// Gives the Receipt back
        /// </summary>
        public Receipt Receipt { get; private set; }
        /// <summary>
        /// Gives the Change back
        /// </summary>
        public List<Coin> Change { get; private set; }
    }
}
