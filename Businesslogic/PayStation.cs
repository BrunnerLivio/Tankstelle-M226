using System;
using System.Collections.Generic;
using System.Linq;

namespace Businesslogic
{
    public class PayStation
    {
        private List<CoinContainer> coinContainers = new List<CoinContainer>();
        private Transaction currentTransaction;

        public PayStation()
        {
            foreach (Coin coin in Enum.GetValues(typeof(Coin)))
            {
                CoinContainer coinContainer = new CoinContainer(coin, 500);
                coinContainers.Add(coinContainer);
            }
            currentTransaction = new Transaction();
        }

        public void InsertCoin(Coin coin)
        {
            currentTransaction.AddMoney(coin);
        }

        /// <summary>
        /// Inserts the Coins Directly and into the CoinContainers
        /// </summary>
        /// <remarks>
        /// JUST FOR ADMINISTRATION
        /// </remarks>
        /// <param name="coin">The Coin</param>
        public void InsertCoinDirectly(Coin coin)
        {
            currentTransaction.AddMoney(coin);
            AcceptValueInput();
        }
        public virtual void AcceptValueInput()
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
                     .FirstOrDefault();
                if(coinContainer == null)
                {
                    throw new Exception("PayStation hat kein Geld mehr!");
                }
                coinContainer.AddCoins(-1);
                backGivenMoney += (int)coinContainer.Cointype;
                coins.Add(coinContainer.Cointype);
            }
            return coins;
        }

        internal List<CoinContainer> CoinContainers
        {
            get
            {
                return coinContainers;
            }
        }

    }
}
