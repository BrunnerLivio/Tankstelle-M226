using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Businesslogic
{
    public class CoinContainer
    {
        #region Membervariables
        private Coin cointype;
        private int amountCoins;
        private int maxAmount;
        

        #endregion
        #region Constructors
        public CoinContainer(Coin cointype, int maxAmount)
        {
            this.cointype = cointype;
            this.maxAmount = maxAmount;
#if DEBUG
            this.amountCoins = 100;
#endif
        }
        #endregion
        #region Methods
        public void AddCoins(int amount)
        {
            if(this.amountCoins + amount <= maxAmount && this.amountCoins + amount >= 0)
            {
                this.amountCoins += amount;
            }
            else
            {
                throw new Exception(String.Format("Sie haben {0}x zu viel Münzen eingworfen. Das System kann nur {1} Münzen dieser Art annehmen.", this.amountCoins + amount - maxAmount, maxAmount));
            }
           
        }
        #endregion
        #region Properties
        public Coin Cointype
        {
            get
            {
                return cointype;
            }

        }
        public int AmountCoins
        {
            get
            {
                return amountCoins;
            }
        }
        public int Money
        {
            get
            {
                return amountCoins * (int)this.cointype;
            }
        }
       
        #endregion
    }
}
