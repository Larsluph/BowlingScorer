using System.Collections.Generic;
using System.Linq;

namespace BowlingScorer
{
    /// <summary>
    /// This class represents a Player
    /// </summary>
    internal class Player
    {
        public string Name;
        public Frame[] Frames;
        public Player(string name)
        {
            Name = name;
            Frames = new Frame[10];
            for (short i = 1; i <= Frames.Length; i++)
            {
                Frames[i-1] = new(i);
            }
        }

        public override string ToString()
        {
            return $"<Player \"{Name}\">";
        }

        /// <summary>
        /// Returns the current total of the player
        /// </summary>
        public int GetTotal()
        {
            int total = 0;
            foreach (int i in GetTotalEnum())
            {
                total = i;
            }
            return total;
        }

        /// <summary>
        /// Returns an Enumerable which returns the cumulative total for each completed frame
        /// </summary>
        public IEnumerable<int> GetTotalEnum()
        {
            int total = 0;
            for (int i = 0; i < Frames.Length; i++)
            {
                Frame frame = Frames[i];
                if (!frame.IsComplete) yield break;
                else if (frame.Is10th)
                    if (frame.IsStrike) total += 10 + frame.Shot2 + frame.Shot3;
                    else if (frame.IsSpare) total += 10 + frame.Shot3;
                    else total += frame.Shot1 + frame.Shot2;
                else if (frame.IsSpecial)
                {
                    Frame nextFrame = Frames[i + 1];
                    if (frame.IsStrike)
                        if (nextFrame.Is10th && nextFrame.Shot2 != -1) total += 10 + nextFrame.Shot1 + nextFrame.Shot2;
                        else if (nextFrame.IsStrike)
                        {
                            if (nextFrame.Is10th)
                                if (nextFrame.Shot2 == -1) yield break;
                                else total += 20 + nextFrame.Shot2;
                            else
                            {
                                Frame next2Frame = Frames[i + 2];
                                if (next2Frame.Shot1 == -1) yield break;
                                else total += 20 + next2Frame.Shot1;
                            }
                        }
                        else if (nextFrame.IsSpare) total += 20;
                        else if (nextFrame.IsComplete) total += 10 + nextFrame.Shot1 + nextFrame.Shot2;
                        else yield break;
                    else if (frame.IsSpare)
                        if (nextFrame.Shot1 == -1) yield break;
                        else total += 10 + nextFrame.Shot1;
                }
                else total += frame.Shot1 + frame.Shot2;
                yield return total;
            }
        }
    }
}
