using System;
using System.Collections.Generic;
using System.Text;
using System.Media;

namespace Text_Adventure_Game
{
    public class Choices
    {
        static Random rand = new Random();

        public static void RandomEncounters()
        {
            switch (rand.Next(0, 5))
            {
                case 0:
                    BasicFightEncounter();
                    break;
                case 1:
                    BasicFightEncounter();
                    break;
                case 2:
                    NemesisEncounter();
                    break;
                case 3:
                    TyranEncounter();
                    break;
                case 4:
                    WeskerEnocounter();
                    break;
            }
        }

        public static void FirstEncounter()
        {
            SoundPlayer sp = new SoundPlayer("sounds/Fightmusic.wav");
            sp.PlayLooping();
            Program.Display("As the zombie approaches you he is making noises that scare you, you grab the knife closers telling him to stay away and that you will hurt him if he attacks you, but he is still making way towards you so you have no choice but to fight him");
            Console.ReadKey();
            Combat(false, "Zombie", 5, 10);
            sp.Stop();
        }

        public static void BasicFightEncounter()
        {
            SoundPlayer sp = new SoundPlayer("sounds/Fightmusic.wav");
            sp.PlayLooping();
            Console.Clear();
            Program.Display("As you make your way towards the finding the exit you see one of the monsters in front of you");
            Console.ReadKey();
            Combat(true, "", 0, 0);
            sp.Stop();
        }

        public static void NemesisEncounter()
        {
            SoundPlayer sp = new SoundPlayer("sounds/Fightmusic.wav");
            sp.PlayLooping();
            Console.Clear();
            Program.Display("While exploring the laboratory you come up on a big room with a single operation table and a enormous body laying on it");
            Program.Display("As you approach on the figure that looks like a human you can hear it breath and slowly get up");
            Program.Display("This is a monster that you have not seen yet. Huge body, massive arms, mutaded to a point of barely resembling a human");
            Console.ReadKey();
            
            CombatBoss(false, "T-01 Nemesis", 10, 30);
            sp.Stop();
        }

        public static void TyranEncounter()
        {
            SoundPlayer sp = new SoundPlayer("sounds/Fightmusic.wav");
            sp.PlayLooping();
            Console.Clear();
            Program.Display("On the map of the entire facilty you saw a big open room that was leading towards the exit");
            Program.Display("While making your way through the room a giant monster has dropped from the ceiling and started to chase you");
            Program.Display("Seeing he is to fast for you, you choose to fight yet another beast in this god forsaken place");
            Console.ReadKey();

            CombatBoss(false, "Tyran T-000", 7, 25);
            sp.Stop();
        }

        public static void WeskerEnocounter()
        {
            SoundPlayer sp = new SoundPlayer("sounds/Fightmusic.wav");
            sp.PlayLooping();
            Console.Clear();
            Program.Display("As you are seeing the exit just in front o you you also notice that a familiar figure is standing in front of it");
            Program.Display("When getting close you notice its one of the executives named wesker who is standing and obstructing way");
            Program.Display("When you tell him that you bought need to get out of hear, he smiles and says that no one will leave this place and that the truth will be buried with you in hear");
            Program.Display("Exhausted you find strength for the final battle to get out of this hell");
            Console.ReadKey();
            
            CombatBoss(false, "Wesker", 15, 50);
            sp.Stop();
        }

        public static void Combat(bool random, string name, int power, int health)
        {
            string n = "";
            int p = 0;
            int h = 0;

            if (random)
            {
                n = GetName();
                p = Program.currentPlayer.GetPower();
                h = Program.currentPlayer.GetHealth();
            }
            else
            {
                n = name;
                p = power;
                h = health;
            }

            while (h > 0)
            {
                Console.Clear();
                Console.WriteLine(n);
                Console.WriteLine("Attack:" + p + "/" + h + ":Health");
                Console.WriteLine("--------------------");
                Console.WriteLine("Attack | Run | Push Back");
                Console.WriteLine("         Heal");
                Console.WriteLine("--------------------");
                Console.WriteLine(Program.currentPlayer.name + " Status:");
                if (Program.currentPlayer.currentClass == PlayerClass.GameClass.USBC)
                    Program.currentPlayer.healthSpray = 3;
                Console.WriteLine("Health: " + Program.currentPlayer.health + " Health Spray: " + Program.currentPlayer.healthSpray);
                string input = Console.ReadLine();

                if (input.ToLower() == "a" || input.ToLower() == "attack")
                {
                    Program.Display("As the " + n + " gets close to you and grabs you, you swing your knife and hit him in the head making the " + n + " move back");

                    int damage = p - Program.currentPlayer.health;
                    damage = rand.Next(0, 6);
                    if (damage < 0)
                        damage = 0;

                    int attack = rand.Next(0, Program.currentPlayer.knifeValue) + rand.Next(1, 5) + ((Program.currentPlayer.currentClass == PlayerClass.GameClass.STARS) ? +5 : 0);

                    Program.Display("Before you hit the " + n + " he scratched you dealing " + damage + " health and the knife hit dealt " + attack + " damage");
                    Program.currentPlayer.health -= damage;
                    h -= attack;

                }
                else if (input.ToLower() == "p" || input.ToLower() == "pushback")
                {
                    Program.Display("As the " + n + " gets close to you and tries to grab you, you try and push him back");

                    int damage = (p / 4) - Program.currentPlayer.health;
                    damage = rand.Next(0, 6);
                    if (damage < 0)
                        damage = 0;

                    Program.Display("As you try to push him back he is to strong so he bites you as you stumble him back and he deals" + damage + " damage");
                    Program.currentPlayer.health -= damage;

                }
                else if (input.ToLower() == "r" || input.ToLower() == "run")
                {
                    if (Program.currentPlayer.currentClass != PlayerClass.GameClass.RPD && rand.Next(0, 2) == 0)
                    {
                        Program.Display("As you try to run past the " + n + " he swings his arm hitting your face and making you stumble backwards");
                        int damage = p - Program.currentPlayer.health;
                        damage = rand.Next(0, 6);
                        if (damage < 0)
                            damage = 0;

                        Program.Display("This cause your head to spin and you lose " + damage + " health");
                        Program.currentPlayer.health -= damage;
                        Console.ReadKey();
                    }
                    else
                    {
                        Program.Display("As the " + n + " tries to hit you, you evade his attack and make your way out to the hallway");
                        Console.ReadKey();
                        Shop.ShopMenu(Program.currentPlayer);
                    }

                }
                else if (input.ToLower() == "h" || input.ToLower() == "heal")
                {
                    if(Program.currentPlayer.healthSpray == 0)
                    {
                        Program.Display("As you try a spray the air with healing aspects you realize that your spray is empty");
                    }
                    else
                    {
                        Program.Display("When in a hurry you press the cap to spray the health around you instatly feel better and ready for more combat");
                        int healthS = rand.Next(1, 20) + ((Program.currentPlayer.currentClass == PlayerClass.GameClass.USBC) ? +2 : 0);
                        Program.Display("You gain " + healthS + " health thanks to the spray");
                        Program.currentPlayer.health += healthS;
                        Program.currentPlayer.healthSpray--;
                    }
                    Console.ReadKey();
                }

                if(Program.currentPlayer.health <= 0)
                {
                    Program.Display("As the " + n + " Strikes you you can sense the coldness in your body and you see the " + n + " Tear you apart");
                    Console.ReadKey();
                    System.Environment.Exit(0); 
                }
                Console.ReadKey();
            }

            int pts = Program.currentPlayer.GetCoins();
            Program.Display("For killing the monster" + n + " you recieve: " + pts);
            Program.currentPlayer.money += pts;
            Console.ReadKey();
        }

        public static void CombatBoss(bool random, string name, int power, int health)
        {
            string n = "";
            int p = 0;
            int h = 0;

            if (random)
            {
                n = GetName();
                p = Program.currentPlayer.GetPower();
                h = Program.currentPlayer.GetHealth();
            }
            else
            {
                n = name;
                p = power;
                h = health;
            }

            while (h > 0)
            {
                Console.Clear();
                Console.WriteLine(n);
                Console.WriteLine("Attack:" + p + "/" + h + ":Health");
                Console.WriteLine("--------------------");
                Console.WriteLine("Attack | Run | Push Back");
                Console.WriteLine("         Heal");
                Console.WriteLine("--------------------");
                Console.WriteLine(Program.currentPlayer.name + " Status:");
                Console.WriteLine("Health: " + Program.currentPlayer.health + " Health Spray: " + Program.currentPlayer.healthSpray);
                string input = Console.ReadLine();

                if (input.ToLower() == "a" || input.ToLower() == "attack")
                {
                    Program.Display("As the " + n + " gets close to you and grabs you, you swing your knife and hit him in the head making the" + n + " move back");

                    int damage = p - Program.currentPlayer.health;
                    damage = rand.Next(0, 6);
                    if (damage < 0)
                        damage = 0;

                    int attack = rand.Next(0, Program.currentPlayer.knifeValue) + rand.Next(1, 5)+((Program.currentPlayer.currentClass == PlayerClass.GameClass.STARS) ? +5 : 0);

                    Program.Display("Before you hit the " + n + " he scratched you dealing " + damage + " health and the knife hit dealt " + attack + " damage");
                    Program.currentPlayer.health -= damage;
                    h -= attack;

                }
                else if (input.ToLower() == "p" || input.ToLower() == "pushback")
                {
                    Program.Display("As the " + n + " gets close to you and tries to grab you, you try and push him back");

                    int damage = (p / 4) - Program.currentPlayer.health;
                    damage = rand.Next(0, 6);
                    if (damage < 0)
                        damage = 0;

                    Program.Display("As you try to push him back he is to strong so he bites you as you stumble him back and he deals" + damage + " damage");
                    Program.currentPlayer.health -= damage;

                }
                else if (input.ToLower() == "r" || input.ToLower() == "run")
                {
                    if (Program.currentPlayer.currentClass != PlayerClass.GameClass.RPD && rand.Next(0, 2) == 0)
                    {
                        Program.Display("As you try to run past the " + n + " he swings his arm hitting your face and making you stumble backwards");
                        int damage = p - Program.currentPlayer.health;
                        damage = rand.Next(0, 6);
                        if (damage < 0)
                            damage = 0;

                        Program.Display("This cause your head to spin and you lose " + damage + " health");
                        Program.currentPlayer.health -= damage;
                        Console.ReadKey();
                        
                    }
                    else
                    {
                        Program.Display("As the " + n + " tries to hit you, you evade his attack and make your way out to the hallway");
                        Console.ReadKey();
                        Shop.ShopMenu(Program.currentPlayer);
                    }

                }
                else if (input.ToLower() == "h" || input.ToLower() == "heal")
                {
                    if (Program.currentPlayer.healthSpray == 0)
                    {
                        Program.Display("As you try a spray the air with healing aspects you realize that your spray is empty");
                    }
                    else
                    {
                        Program.Display("When in a hurry you press the cap to spray the health around you instatly feel better and ready for more combat");
                        int healthS = rand.Next(1, 20) + ((Program.currentPlayer.currentClass == PlayerClass.GameClass.USBC) ? + 2 : 0);
                        Program.Display("You gain " + healthS + " health thanks to the spray");
                        Program.currentPlayer.health += healthS;
                        Program.currentPlayer.healthSpray--;
                    }
                    Console.ReadKey();
                }

                if (Program.currentPlayer.health <= 0)
                {
                    Program.Display("As the " + n + " Strikes you you can sense the coldness in your body and you see the " + n + " Tear you apart");
                    Console.ReadKey();
                    System.Environment.Exit(0);
                }
                Console.ReadKey();
            }

            int pts = Program.currentPlayer.GetCoinsBoss() * 5;
            Program.Display("For killing the monster" + n + " you recieve: " + pts);
            Program.currentPlayer.money += pts;
            Console.ReadKey();
        }

        public static string GetName()
        {
            switch (rand.Next(0, 3))
            {
                case 0:
                    return "Fat Zombie";
                case 1:
                    return "Dog";
                case 2:
                    return "Brain Sucker";
            }
            return "Zombie";
        }
    }
}
