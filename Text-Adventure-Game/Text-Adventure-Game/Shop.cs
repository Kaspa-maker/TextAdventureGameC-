using System;
using System.Collections.Generic;
using System.Text;
using System.Media;

namespace Text_Adventure_Game
{
    public class Shop
    {

        public static void ShopMenu(PlayerClass p)
        {
            RunShop(p);
        }

        public static void RunShop(PlayerClass p)
        {
            int healthSpraypts;
            int weaponPts;
            int difficultyMod;

            while (true)
            {
                SoundPlayer sp = new SoundPlayer("sounds/theme1.wav");
                sp.PlayLooping();
                healthSpraypts = 100 + 10 * p.equipment;
                weaponPts = 500 * (p.knifeValue);
                difficultyMod = 700 + 100 * p.equipment;

                Console.Clear();
                Console.WriteLine("---Welcome Stranger---");
                Console.WriteLine("1.Health Spray: " + healthSpraypts + "pts");
                Console.WriteLine("2.Weapon Upgrade: " + weaponPts + "pts");
                Console.WriteLine("3.Difficulty Modifier: " + difficultyMod + "pts");
                Console.WriteLine("4.Exit Shop");
                Console.WriteLine("----------------------");
                Console.WriteLine();
                Console.WriteLine("---"+ p.name +"'s Stats---");
                Console.WriteLine("Health: " + p.health);
                Console.WriteLine("Weapon Strenght: " + p.knifeValue);
                Console.WriteLine("Health Spray: " + p.healthSpray);
                Console.WriteLine("Difficulty modifier: " + p.equipment);
                Console.WriteLine("Points: " + p.money);
                Console.WriteLine("----------------------");
                Console.WriteLine();
                Console.WriteLine("5.Quit Game");

                string input = Console.ReadLine().ToLower();
                if(input == "1" || input == "health spray")
                {
                    TryBuy("health spray", healthSpraypts, p);
                }else if (input == "2" || input == "Weapon upgrade")
                {
                    TryBuy("Weapon upgrade", weaponPts, p);
                }
                else if(input == "3" || input == "Difficulty modifier")
                {
                    TryBuy("Difficulty modifier", difficultyMod, p);
                }else if(input == "5" || input == "Quit")
                {
                    Program.Quit();
                }
                else if(input == "4" || input == "Exit")
                    break;

                sp.Stop();
            }
            
        }

        static void TryBuy(string item, int cost, PlayerClass p)
        {
            if(p.money >= cost)
            {
                if (item == "health spray")
                    p.healthSpray++;
                else if (item == "Weapon upgrade")
                    p.knifeValue++;
                else if (item == "Difficulty modifier")
                    p.equipment++;

                p.money -= cost;
            }else
            {
                Console.WriteLine("Not enough points");
                Console.ReadKey();
            }
        }
    }
}
