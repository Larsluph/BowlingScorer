using System;
using System.Collections.Generic;
using System.Linq;

namespace BowlingScorer
{
    class MainApp {
        /// <summary>
        /// Takes a string array and joins them into one
        /// </summary>
        /// <param name="data">string array containing values to join</param>
        /// <param name="join">separator to use to join strings</param>
        /// <returns>a new joined string</returns>
        public static string Join(string[] data, string join)
        {
            string result = data[0];
            for (int i = 1; i < data.Length; i++)
            {
                result += join + data[i];
            }
            return result;
        }

        /// <summary>
        /// Returns whether the given set contains at least one target
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="target"></param>
        /// <param name="set"></param>
        /// <returns></returns>
        public static bool In<T>(T target, T[] set)
        {
            foreach (T item in set)
                if (item.Equals(target)) return true;
            
            return false;
        }

        /// <summary>
        /// Returns a centered string
        /// </summary>
        /// <param name="data"></param>
        /// <param name="length"></param>
        /// <returns></returns>
        public static string Center(string data, int length)
        {
            if (data.Length == 0) return StringMult(" ", length);
            if (length <= 0) return "";

            int diff = length - data.Length;
            int offset = diff / 2;

            if (diff % 2 == 0) return StringMult(" ", offset) + data + StringMult(" ", offset);
            else return StringMult(" ", offset+1) + data + StringMult(" ", offset);
        }

        /// <summary>
        /// Returns a pattern which has been multiplied by a certain factor
        /// </summary>
        /// <example>
        /// StringMult("*", 5) == "*****";
        /// StringMult("abc", 3) == "abcabcabc";
        /// </example>
        /// <param name="pattern"></param>
        /// <param name="factor"></param>
        /// <returns></returns>
        public static string StringMult(string pattern, int factor)
        {
            string result = "";
            for (int i = 0; i < factor; i++)
            {
                result += pattern;
            }
            return result;
        }

        /// <summary>
        /// Generates a border line
        /// </summary>
        /// <param name="maxLength"></param>
        /// <returns></returns>
        public static string GenTableLine(int maxLength)
        {
            string head = $"+{StringMult("-", maxLength + 2)}+"; // name field

            string frame = $"{StringMult("-", 5)}+"; // frames 1-9
            string frame10 = $"{StringMult("-", 7)}+"; // frame 10 / total

            return head + StringMult(frame, 9) + StringMult(frame10, 2); // name field + 9 frames + frame10 + total(same as frame10)
        }
        /// <summary>
        /// Generates a header line
        /// </summary>
        /// <param name="maxLength"></param>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string GenTableLine(int maxLength, string[] data)
        {
            string frames = $"| {data[0].PadRight(maxLength)} |"; // name field

            int i = 1;

            // frames 1-9
            for (; i < 10; i++)
            {
                frames += $"{Center(data[i], 5)}|";
            }

            // frame 10 / total
            for (; i < data.Length; i++)
            {
                frames += $"{Center(data[i], 7)}|";
            }

            return frames;
        }
        /// <summary>
        /// Generate a score line
        /// </summary>
        /// <param name="maxLength"></param>
        /// <param name="player"></param>
        /// <returns></returns>
        public static string GenTableLine(int maxLength, Player player)
        {
            string frames = $"| {player.Name.PadRight(maxLength)} |"; // name field

            foreach (Frame frame in player.Frames)
            {
                frames += $" {frame} |"; // frames
            }

            frames += $" {Center(player.GetTotal().ToString(), 5)} |"; // total

            // 2nd line
            frames += "\n";

            frames += $"| {StringMult(" ", maxLength)} |";
            int[] vs = player.GetTotalEnum().ToArray();

            // frame 1-9
            int i = 0;
            string data;
            for (; i < 9; i++)
            {
                if (i >= vs.Length) data = " ";
                else data = vs[i].ToString();

                frames += $" {data,3} |";
            }

            // frame 10
            if (i >= vs.Length) data = " ";
            else data = vs[i].ToString();
            frames += $" {Center(data, 5)} | {StringMult(" ", 5)} |";

            return frames;
        }

        /// <summary>
        /// Display a table in the correct format
        /// </summary>
        /// <param name="players"></param>
        public static void DispTable(List<Player> players)
        {
            List<string> playerNames = players.Select((Player player, int x) => player.Name).ToList();
            int maxLength = playerNames.Select((string data, int x) => data.Length).Max();

            Console.WriteLine(GenTableLine(maxLength));
            Console.WriteLine(GenTableLine(maxLength, new string[] { "", "1", "2", "3", "4", "5", "6", "7", "8", "9", "1 0", "Total" }));
            Console.WriteLine(GenTableLine(maxLength));
            for (int i = 0; i < players.Count; i++)
            {
                Console.WriteLine(GenTableLine(maxLength, players[i]));
                Console.WriteLine(GenTableLine(maxLength));
            }
            Console.WriteLine();
        }

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

        public static void Main()
        {
            // Phase 1
            List<Player> players = AskPlayerNames();
            // Phase 1

            // Phase 2
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
                } catch (FormatException)
                {
                    string value = userInput.Trim();
                    if (value.Equals("X")) userValue = 10;
                    else if (value.Equals("/"))
                    {
                        if (currentTake == 3) userValue = 10 - frame.Shot2;
                        else userValue = 10 - frame.Shot1;
                    }
                    else if (value.Equals("-")) userValue = 0;
                    else continue;
                }

                try
                {
                    if (currentTake == 1) frame.Shot1 = userValue;
                    else if (currentTake == 2) frame.Shot2 = userValue;
                    else frame.Shot3 = userValue;
                } catch (Exception e) when (e is InvalidOperationException ||
                                            e is ArgumentOutOfRangeException)
                {
                    Console.WriteLine("Invalid input. try again...");
                    Console.ReadKey();
                    continue;
                }

                //players[currentPlayer].Frames[currentFrame] = frame;

                currentTake++;
                if (!frame.IsComplete) continue;

                currentTake = 1;
                if (++currentPlayer >= players.Count)
                {
                    currentPlayer = 0;
                    currentFrame++;
                }
            }
            // Phase 2

            // Phase 3
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
                if (player.GetTotal() < min) min = total;
            }
            Console.Write($"The winner is {winner}! ");
            if (players.Count == 1) Console.WriteLine("Well played! (Even if you're alone)");
            else Console.WriteLine("Congratulations!");

            if (max == 0) Console.WriteLine("Looks like someone need to train their aim skills :/");
            // Phase 3
        }
    }
}
