using System;
using System.Collections.Generic;
using System.Linq;

namespace BowlingScorer
{
    class MainApp {
        public static string Join(string[] data, string join)
        {
            string result = data[0];
            for (int i = 1; i < data.Length; i++)
            {
                result += join + data[i];
            }
            return result;
        }

        public static bool In<T>(T target, T[] set)
        {
            foreach (T item in set)
                if (item.Equals(target)) return true;
            
            return false;
        }

        public static string Center(string data, int maxWidth)
        {
            if (data.Length == 0) return StringMult(" ", maxWidth);
            if (maxWidth <= 0) return "";

            int diff = maxWidth - data.Length;
            int offset = diff / 2;

            if (diff % 2 == 0) return StringMult(" ", offset) + data + StringMult(" ", offset);
            else return StringMult(" ", offset+1) + data + StringMult(" ", offset);
        }

        public static string StringMult(string pattern, int factor)
        {
            string result = "";
            for (int i = 0; i < factor; i++)
            {
                result += pattern;
            }
            return result;
        }

        public static string GenTableLine(int maxLength)
        {
            string head = $"+-{StringMult("-", maxLength)}-+"; // name field

            string frame = $"-{StringMult("-", 3)}-+"; // frames 1-9
            string frame10 = $"-{StringMult("-", 5)}-+"; // frame 10 / total

            return head + StringMult(frame, 9) + StringMult(frame10, 2); // name field + 9 frames + frame10 + total(same as frame10)
        }
        public static string GenTableLine(int maxLength, string[] data)
        {
            string frames = $"| {data[0].PadRight(maxLength)} |"; // name field

            int i = 1;

            // frames 1-9
            for (; i < 10; i++)
            {
                frames += $" {Center(data[i], 3)} |";
            }

            // frame 10 / total
            for (; i < data.Length; i++)
            {
                frames += $" {Center(data[i], 5)} |";
            }

            return frames;
        }
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

        public static void Main()
        {
            char[] allowedAlpha = "-123456789X/".ToCharArray();

            List<Player> players = new();
            while (true)
            {
                Console.WriteLine($"Entrer le nom du joueur n°{players.Count+1} : ");
                string name = Console.ReadLine();
                if (name == null) return;
                Player player = new(name);
                if (player.Name == "")
                    if (players.Count != 0) break;
                    else continue;
                else players.Add(player);
            }

            int currentFrame = 0;
            int currentPlayer = 0;
            int currentTake = 1;
            while (!players.Last().Frames.Last().IsComplete)
            {
                Console.Clear();
                DispTable(players);

                Player player = players[currentPlayer];
                Frame frame = player.Frames[currentFrame];
                Console.WriteLine($"Frame n°{currentFrame+1}");
                Console.Write($"Score de {player.Name}, take {currentTake}: ");
                string userInput = Console.ReadLine().ToUpper();

                //data validation
                if (userInput.Length != 1) continue;

                char input = userInput.ToCharArray()[0];
                if (!In(input, allowedAlpha) || currentTake == 1 && input == '/') continue;
                //TODO: frame 10

                //data registration => input is considered valid at this point
                if (currentTake == 1 && input == 'X' && !frame.Is10th)
                {
                    frame.take2 = '0';
                }
                else if (currentTake == 2 && frame.take1 == '-' && input == 'X')
                {
                    input = '/';
                }

                if (currentTake == 1) frame.take1 = input;
                else if (currentTake == 2) frame.take2 = input;
                else frame.take3 = input;

                players[currentPlayer].Frames[currentFrame] = frame;

                currentTake++;
                if (!frame.IsComplete) continue;

                currentTake = 1;
                if (++currentPlayer >= players.Count)
                {
                    currentPlayer = 0;
                    currentFrame++;
                }
            }

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
        }
    }
}
