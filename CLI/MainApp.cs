using Base;
using System;
using System.Collections.Generic;
using System.Linq;

namespace CLI
{
    partial class MainApp {
        public static List<Player> AskPlayerNames()
        {
            List<Player> players = new();
            while (true)
            {
                Console.WriteLine($"Entrer le nom du joueur n°{players.Count + 1} : ");
                string name = Console.ReadLine();
                if (name == null) break;
                Player player = new(name);
                if (player.Name == "")
                    if (players.Count != 0) break;
                    else continue;
                else players.Add(player);
            }
            return players;
        }

        public static void Mainloop(List<Player> players)
        {
            int currentFrame = 0;
            int currentPlayer = 0;
            int currentTake = 1;
            while (!players.Last().Frames.Last().IsComplete)
            {
                Console.Clear();
                DispTable(players);

                Player player = players[currentPlayer];
                Frame frame = player.Frames[currentFrame];
                Console.WriteLine($"Frame n°{frame.Index}");
                Console.Write($"Score de {player.Name}, take {currentTake}: ");
                string userInput = Console.ReadLine().ToUpper();

                //data validation
                int userValue;
                try
                {
                    userValue = Convert.ToInt32(userInput);
                }
                catch (FormatException)
                {
                    string value = userInput.Trim();
                    if (value.Equals("X")) userValue = 10;
                    else if (value.Equals("/"))
                    {
                        if (currentTake == 3)
                            if (frame.IsStrike) userValue = 10 - frame.Shot2;
                            else if (frame.IsSpare) userValue = 10;
                            else throw new InvalidOperationException("3rd shot is unusable when frame isn't special");
                        else userValue = 10 - frame.Shot1;
                    }
                    else if (value.Equals("-")) userValue = 0;
                    else
                    {
                        Console.Write("Invalid format. try again...");
                        Console.ReadKey();
                        continue;
                    }
                }

                try
                {
                    if (currentTake == 1) frame.Shot1 = userValue;
                    else if (currentTake == 2) frame.Shot2 = userValue;
                    else frame.Shot3 = userValue;
                }
                catch (Exception e) when (e is InvalidOperationException or
                                          ArgumentOutOfRangeException)
                {
                    Console.Write("{0}: {1}", e.GetType().Name, e.Message);
                    Console.ReadKey();
                    continue;
                }

                currentTake++;
                if (!frame.IsComplete) continue;

                currentTake = 1;
                if (++currentPlayer >= players.Count)
                {
                    currentPlayer = 0;
                    currentFrame++;
                }
            }
        }

        public static void DispEndGame(List<Player> players)
        {
            Console.Clear();
            DispTable(players);

            Console.WriteLine("GAME OVER!");
            string winner = players[0].Name;
            int min = players[0].GetTotal();
            int max = 0;
            foreach (Player player in players)
            {
                int total = player.GetTotal();
                if (total > max)
                {
                    winner = player.Name;
                    max = total;
                }
                if (total < min) min = total;
            }
            Console.Write($"The winner is {winner} with {max} points! ");
            if (players.Count == 1) Console.WriteLine("Well played! (Even if you're alone)");
            else Console.WriteLine("Congratulations!");

            if (min == 0) Console.WriteLine("Looks like someone need to train their aim skills :/");
        }

        public static void Main()
        {
            // Phase 1
            List<Player> players = AskPlayerNames();
            // Phase 1

            // Phase 2
            Mainloop(players);
            // Phase 2

            // Phase 3
            DispEndGame(players);
            // Phase 3
        }
    }
}
