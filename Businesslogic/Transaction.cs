using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Businesslogic
{
    public class Transaction
    {
        #region Membervariables
        private List<Coin> insertedMoney = new List<Coin>();
        #endregion
        #region Methods
        public void AddMoney(Coin coin)
        {
            insertedMoney.Add(coin);
        }
        #endregion
        #region Properties

        public int Money
        {
            get
            {
                int money = 0;
                foreach(Coin coin in insertedMoney)
                {
                    money += (int)coin;
                }
                return money;
            }
        }
        public void Accept()
        {
            //PayStation payStation = new PayStation();
            //foreach(Coin coin in InsertedMoney)
            //{
            //    payStation.CoinContainers.Where(c => c.Cointype == coin).First().AddCoins(1);
            //}
        }

        public List<Coin> InsertedMoney
        {
            get
            {
                return insertedMoney;
            }

            set
            {
                insertedMoney = value;
            }
        }

        public void FlushMoney()
        {
            insertedMoney.Clear();
        }


        #endregion
    }
}
