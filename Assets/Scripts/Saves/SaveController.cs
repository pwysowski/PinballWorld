using System;
using UnityEngine;

namespace Assets.Scripts.Saves
{
    public class SaveController : ISaveController
    {
        private static string MONEY_KEY = "Money";
        private static string LAST_LOGIN = "Last_Login";
        private static string DAILY_COUNT = "Daily_Count";
        private static string FIRST_GAME = "First_Game";
        public void SaveMoney(int money)
        {
            PlayerPrefs.SetInt(MONEY_KEY, money);
        }
        public int LoadMoney()
        {
            return PlayerPrefs.GetInt(MONEY_KEY, 0);
        }

        public void SaveLastLogin(DateTime date)
        {
            PlayerPrefs.SetString(LAST_LOGIN, date.ToString());
        }
        public DateTime LoadLastLogin()
        {
            string dateString = PlayerPrefs.GetString(LAST_LOGIN, "");
            DateTime date;
            bool success = DateTime.TryParse(dateString, out date);

            if (!success)
            {
                date = DateTime.Now;
            }
            return date;
        }

        public void SaveDailyCount(int count)
        {
            PlayerPrefs.SetInt(DAILY_COUNT, count);
        }
        public int LoadDailyCount()
        {
            return PlayerPrefs.GetInt(DAILY_COUNT, 0);
        }

        public void SaveIsFirstGame()
        {
            PlayerPrefs.SetInt(FIRST_GAME, 1);
        }

        public bool LoadIsFirstGame()
        {
            int integerValue = PlayerPrefs.GetInt(FIRST_GAME, 0);
            return integerValue == 1;
        }
    }
}