using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Assets.Scripts.Saves
{
    public interface ISaveController
    {
        void SaveMoney(int money);
        int LoadMoney();

        void SaveLastLogin(DateTime date);
        DateTime LoadLastLogin();

        void SaveDailyCount(int count);
        int LoadDailyCount();

        void SaveIsFirstGame();
        bool LoadIsFirstGame();
    }
}
