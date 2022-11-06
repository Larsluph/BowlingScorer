using Base;
using System.Collections.Generic;
using System;
using System.Linq;

namespace CLI
{
    partial class MainApp
    {
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
    }
}