
using UnityEngine;

namespace ErfanDeveloper
{
    public static class PlayerHQ
    {
        private static int coin;
        
        public static int coinNum
        {
            get => coin;
        }

        public static void LoadData()
        {
            coin = PlayerPrefs.GetInt("CoinAmount");
        }
        public static void CoinTransactions(bool isGive, int amount)
        {
            int currentCoin = PlayerPrefs.GetInt("CoinAmount");
            if (isGive)
            {
                currentCoin += amount;
            }
            else
            {
                currentCoin -= amount;
            }

            PlayerPrefs.SetInt("CoinAmount", currentCoin);
            coin = PlayerPrefs.GetInt("CoinAmount");
        }
    }
}