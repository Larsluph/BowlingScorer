using System;
using System.Collections.Generic;

namespace BowlingScorer
{
    struct Frame
    {
        public int Index;
        public char take1;
        public char take2;
        public char take3;

        public Frame(int i)
        {
            if (i < 1 || i > 10) throw new ArgumentOutOfRangeException(nameof(i));

            Index = i;
            take1 = ' ';
            take2 = ' ';
            take3 = new();
            if (Is10th) take3 = ' ';
            else take3 = '0';
        }

        public bool Is10th => Index == 10;
        public bool IsComplete => take1 != ' ' && take2 != ' ' && take3 != ' ';
        public bool IsStrike => take1 == 'X';
        public bool IsSpare => take2 == '/';
        public bool IsSpecial => IsStrike || IsSpare;

        public override string ToString()
        {
            if (Is10th)
            {
                //frame 10
                return IsSpecial ? $"{take1} {take2} {take3}" : $"{take1} {take2}  ";
            }
            else
            {
                // frame 1-9
                return IsStrike ? $"  {take1}" : $"{take1} {take2}";
            }
        }
    }

    struct Player
    {
        public string Name;
        public Frame[] Frames;

        public Player(string name)
        {
            Name = name;
            Frames = new Frame[10];
            for (int i = 0; i < Frames.Length; i++)
            {
                Frames[i] = new(i + 1);
            }
        }

        public override string ToString()
        {
            return $"<Player \"{Name}\">";
        }

        public static int ParseTake(char shot)
        {
            if (shot == '-') return 0; // gutter
            else if (shot == 'X' || shot == '/') return 10; // spare / strike
            else return (int)char.GetNumericValue(shot);
        }

        public int GetTotal()
        {
            int total = 0;
            foreach (int i in GetTotalEnum())
            {
                total = i;
            }
            return total;
        }
        public IEnumerable<int> GetTotalEnum()
        {
            int total = 0;
            for (int i = 0; i < Frames.Length; i++)
            {
                Frame frame = Frames[i];
                if (!frame.IsComplete) yield break;
                if (frame.Is10th)
                    if (frame.IsStrike)
                        if (frame.take3 == '/') total += 20;
                        else total += 10 + ParseTake(frame.take2) + ParseTake(frame.take3);
                    else if (frame.IsSpare) total += 10 + ParseTake(frame.take3);
                    else total += ParseTake(frame.take1) + ParseTake(frame.take2);
                else
                {
                    if (frame.IsSpecial)
                    {
                        Frame nextFrame = Frames[i + 1];
                        if (frame.IsStrike)
                            if (nextFrame.Is10th && nextFrame.take2 != ' ') total += 10 + ParseTake(nextFrame.take1) + ParseTake(nextFrame.take2);
                            else
                            {
                                if (nextFrame.IsStrike)
                                {
                                    if (nextFrame.Is10th)
                                        if (nextFrame.take2 == ' ') yield break;
                                        else total += 20 + ParseTake(nextFrame.take2);
                                    else
                                    {
                                        Frame next2Frame = Frames[i + 2];
                                        if (next2Frame.take1 == ' ') yield break;
                                        else total += 20 + ParseTake(next2Frame.take1);
                                    }
                                }
                                else if (nextFrame.IsSpare) total += 20;
                                else if (nextFrame.IsComplete) total += 10 + ParseTake(nextFrame.take1) + ParseTake(nextFrame.take2);
                                else yield break;
                            }
                        else if (frame.IsSpare)
                            if (nextFrame.take1 == ' ') yield break;
                            else total += 10 + ParseTake(nextFrame.take1);
                    }
                    else total += ParseTake(frame.take1) + ParseTake(frame.take2);
                }
                yield return total;
            }
        }
    }
}
