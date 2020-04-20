using System;
using System.Collections.Generic;
using System.Text;
using System.Media;

namespace Text_Adventure_Game
{
    [Serializable]
    public class PlayerClass
    {
        Random rand = new Random();

        public string name;
        public int ID;
        public int health = 50;
        public int money = 0;
        public int damage = 10;
        public int healthSpray = 1;
        public int knifeValue = 2;

        public int equipment = 0;

        public enum GameClass {USBC, STARS, BSAA, RPD};
        public GameClass currentClass = GameClass.RPD;

        public int GetHealth()
        {
            int upper = (2 * equipment + 4);
            int lower = (equipment + 2);
            return rand.Next(lower, upper);
        }

        public int GetPower()
        {
            int upper = (2 * equipment + 3);
            int lower = (equipment + 1);
            return rand.Next(lower, upper);
        }

        public int GetCoins()
        {
            int upper = (20 * equipment + 50);
            int lower = (10 * equipment + 10);
            return rand.Next(lower, upper);
        }

        public int GetCoinsBoss()
        {
            int upper = (40 * equipment + 100);
            int lower = (20 * equipment + 20);
            return rand.Next(lower, upper);
        }
    }
}
