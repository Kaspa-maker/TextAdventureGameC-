using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using System.Media;

namespace Text_Adventure_Game
{
    class Program
    {
        public static PlayerClass currentPlayer = new PlayerClass();
        public static bool mainLoop = true;
        static void Main(string[] args)
        {
            if (!Directory.Exists("save"))
            {
                Directory.CreateDirectory("save");
            }

            currentPlayer = load(out bool newP);

            if(newP)
                Choices.FirstEncounter();
               
            //NewStart();
            while (mainLoop)
            {
                Choices.FirstEncounter();
                Choices.RandomEncounters();
            }
        }

        static PlayerClass NewStart(int id)//int id
        {
            SoundPlayer splayer = new SoundPlayer("sounds/theme1.wav");
            splayer.PlayLooping();
            Console.Clear();
            PlayerClass player = currentPlayer;//new PlayerClass();
            Display("Biohazard Chronicles");
            Display("Please enter your name:");
            currentPlayer.name = Console.ReadLine();
            Display("Choose a team: USBC | STARS | BSAA | RPD");
            bool flag = false;
            while (flag == false)
            {
                flag = true;
                string input = Console.ReadLine().ToLower();
                if (input == "usbc")
                    currentPlayer.currentClass = PlayerClass.GameClass.USBC;
                else if (input == "stars")
                    currentPlayer.currentClass = PlayerClass.GameClass.STARS;
                else if (input == "bsaa")
                    currentPlayer.currentClass = PlayerClass.GameClass.BSAA;
                else if (input == "rpd")
                    currentPlayer.currentClass = PlayerClass.GameClass.RPD;
                else
                {
                    Display("No such team is available");
                    flag = false;
                }
            }
            player.ID = id;
            Console.Clear();
            Display("You are fresh out of college student who managed to get internship in the biggest pharmaceutical company name Umbrella, \n" +
                "On your first day at work a deadly virus that turns people into brainless flesh feeding zombies gets out on your eyes and you see the devastating results, \n" +
                "You know you have to get out. As you are runing for the entrance you get cut off by zombies and are forced to retreir towards the living quarters in this underground facility, \n" +
                "As you get to your room you see a Welcome to the Family " + currentPlayer.name + " sign that was set up by your new co-workers and now you must kill them and escape this nightmare");
            Console.ReadKey();
            SoundPlayer splayer2 = new SoundPlayer("sounds/theme1.wav");
            splayer.PlayLooping();
            Console.Clear();
            Display("When you are in your room you see a knife, You run towards it");
            SoundPlayer splayer3 = new SoundPlayer("sounds/theme1.wav");
            splayer.PlayLooping();
            Display("As soon as you grab the knife one of the zombies walks into the room!");
            Console.ReadKey();
            splayer.Stop();
            splayer2.Stop();
            splayer3.Stop();
            return player;
        }

        public static void Quit()
        {
            save();
            Environment.Exit(0);
        }



      public static void save()
        {
            BinaryFormatter binFormat = new BinaryFormatter();
            string path = "save/" + currentPlayer.ID.ToString() + ".level";
            FileStream file = File.Open(path, FileMode.OpenOrCreate);
            binFormat.Serialize(file, currentPlayer);
            file.Close();
        }

        public static PlayerClass load(out bool newP)
        {
            newP = false;
            Console.Clear();
            string[] paths = Directory.GetFiles("save");
            List<PlayerClass> saves = new List<PlayerClass>();
            int IDCount = saves.Count;

            BinaryFormatter binaryForm = new BinaryFormatter();
            foreach (string p in paths)
            {
                FileStream file = File.Open(p, FileMode.Open);
                PlayerClass player = (PlayerClass)binaryForm.Deserialize(file);
                file.Close();
                saves.Add(player);

            }
            IDCount = saves.Count;

            
            while (true)
            {
                Console.Clear();
                Console.WriteLine("Choose your save: ");
                
                foreach (PlayerClass p in saves)
                {
                    Console.WriteLine(p.ID + ":" + p.name);
                }
                Console.WriteLine("Enter player id or type create");
                string[] data = Console.ReadLine().Split(':');

                try
                {
                    /*if(data[0] == pId)
                    {
                        if(int.TryParse(data[1],out int id))
                        {
                            foreach(PlayerClass player in saves)
                            {
                                if(player.ID == id)
                                {
                                    return player;
                                }
                            }
                            Console.WriteLine("Sorry this file doesn't exist");
                            Console.ReadKey();
                        }
                        else
                        {
                            Console.WriteLine("Error occured please try again");
                            Console.ReadKey();
                        }
                    }
                    else */if(data [0] == "create")
                    {
                        PlayerClass newPlayer = NewStart(IDCount);
                        newP = true;
                        return newPlayer;
                    }

                    else
                    {
                        foreach(PlayerClass player in saves)
                        {
                            if (player.ID == Convert.ToInt32(data[0]))
                            {
                                return player;
                            }
                            //else if(!saves.Contains(currentPlayer))
                            //{
                            //    Console.WriteLine("Error occured please try again");
                            //    Console.ReadKey();
                            //}
                                
                        }
                    }
                }
                catch (IndexOutOfRangeException)
                {
                    Console.WriteLine("Error occured please try again");
                    Console.ReadKey();
                }
            }

        }

        public static void Display(string text, int speed = 10)
        {
            /*SoundPlayer sp = new SoundPlayer("sounds/TPwav.wav");
            sp.PlayLooping();*/
            foreach (char c in text)
            {
                Console.Write(c);
                System.Threading.Thread.Sleep(speed);
            }
            //sp.Stop();
            Console.WriteLine();
        }

    }
}
