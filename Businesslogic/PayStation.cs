 using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Businesslogic
{
    public class PayStation
    {
        #region Membervariables
        private List<CoinContainer> coinContainers = new List<CoinContainer>();
        private Transaction currentTransaction;
        #endregion
        #region Constructors
        public PayStation()
        {
            foreach (Coin coin in Enum.GetValues(typeof(Coin)))
            {
                CoinContainer coinContainer = new CoinContainer(coin, 500);
#if DEBUG
                coinContainer.AddCoins(400);
#endif
                coinContainers.Add(coinContainer);
            }
            currentTransaction = new Transaction();
        }
        #endregion
        #region Methods
        public void InsertCoin(Coin coin)
        {
            currentTransaction.AddMoney(coin);
        }
        public void AcceptValueInput()
        {
            foreach(Coin coin in Enum.GetValues(typeof(Coin)))
            {
                int amount = currentTransaction.InsertedMoney.Where(c => c == coin).Count();
                if (amount > 0)
                {
                    CoinContainer selectedCoinContainer = coinContainers.Where(c => c.Cointype == coin).Single();
                    selectedCoinContainer.AddCoins(amount);
                }
                
            }
            currentTransaction.FlushMoney();
        }
        public void NotAcceptValueInput()
        {
            currentTransaction.FlushMoney();
        }
        public int GetValueInput()
        {
            return currentTransaction.Money;
        }
        
        public int GetValueTotal()
        {
            int moneyAmount = 0;
            foreach (CoinContainer coinContainer in coinContainers)
            {
                moneyAmount += coinContainer.Money;
            }
            return moneyAmount;
        }
        public int GetQuantityCoins()
        {
            return coinContainers.Sum(c => c.AmountCoins);
        }
        public List<Coin> GetChange(int value)
        {
            List<Coin> coins = new List<Coin>();
            int backGivenMoney = 0;
            while (backGivenMoney != value)
            {
                CoinContainer coinContainer = coinContainers
                    .Where(c => 
                        value >= (int)c.Cointype && 
                        c.AmountCoins >= 1)
                    .OrderByDescending(c => (
                        int)c.Cointype)
                     .First();

                coinContainer.AddCoins(-1);
                backGivenMoney += (int)coinContainer.Cointype;
                coins.Add(coinContainer.Cointype);
            }
            return coins;
        }
        #endregion
        #region Properties
        public List<CoinContainer> CoinContainers
        {
            get
            {
                return coinContainers;
            }
        }
        #endregion
    }
}
