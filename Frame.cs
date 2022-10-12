using System;

namespace BowlingScorer
{
    internal class Frame
    {
        public int Index;
        private int take1;
        private int take2;
        private int take3;

        public int Shot1
        {
            get { return take1; }
            set
            {
                if (value < 0 || value > 10) throw new ArgumentOutOfRangeException(nameof(value), "Value must be between 0 and 10");
                else take1 = value;

                if (value == 10 && !Is10th)
                {
                    take2 = 0;
                }
            }
        }
        public int Shot2
        {
            get { return take2; }
            set
            {
                if (take1 == -1) throw new InvalidOperationException("First shot must be specified before specifying second shot");
                else if (value < 0 || value > 10 - take1) throw new ArgumentOutOfRangeException(nameof(value), $"Value must be between 0 and ${10 - take1}");
                else take2 = value;
            }
        }
        public int Shot3
        {
            get {
                if (!Is10th) throw new InvalidOperationException("Third shot isn't readable when not in 10th frame");
                else return take2;
            }
            set
            {
                if (!Is10th) throw new InvalidOperationException("Third shot is not writable outside 10th frame");
                else if (take1 == -1 || take2 == -1) throw new InvalidOperationException("Can't specify third shot if previous shots have not been specified");
                else if (value < 0 || value > 10) throw new ArgumentOutOfRangeException(nameof(value), "Value must be between 0 and 10");
                else if (IsStrike && value > 10 - take2 && take2 != 10) throw new ArgumentOutOfRangeException(nameof(value), $"Value must be between 0 and ${10 - take2}");
                else take3 = value;
            }
        }

        public Frame(int frame_index)
        {
            if (frame_index < 1 || frame_index > 10) throw new ArgumentOutOfRangeException(nameof(frame_index));

            Index = frame_index;
            take1 = -1;
            take2 = -1;
            take3 = (Is10th) ? -1 : 0;
        }

        /// <summary>
        /// Return true if the frame is the 10th of a player
        /// </summary>
        public bool Is10th => Index == 10;
        /// <summary>
        /// Return true if the frame is fully completed
        /// </summary>
        public bool IsComplete => take1 != -1 && take2 != -1 && take3 != -1;
        /// <summary>
        /// Return true if the frame is a strike
        /// </summary>
        public bool IsStrike => take1 == 10;
        /// <summary>
        /// Return true if the frame is a spare
        /// </summary>
        public bool IsSpare => !IsStrike && take1 + take2 == 10;
        /// <summary>
        /// Return true if the frame is a special shot (strike or spare)
        /// </summary>
        public bool IsSpecial => IsStrike || IsSpare;

        public override string ToString()
        {
            char res1;
            if (take1 == -1) res1 = ' ';
            else if (IsStrike) res1 = 'X';
            else if (take1 == 0) res1 = '-';
            else res1 = take1.ToString().ToCharArray()[0];

            char res2;
            if (take2 == -1) res2 = ' ';
            else if (IsSpare) res2 = '/';
            else if (take2 == 0) res2 = '-';
            else if (Is10th && take2 == 10) res2 = 'X';
            else res2 = take2.ToString().ToCharArray()[0];

            if (Is10th)
            {
                //frame 10
                char res3;
                if (take3 == -1) res3 = ' ';
                else if (take3 == 10) res3 = 'X';
                else if (take2 + take3 == 10) res3 = '/';
                else if (take3 == 0) res3 = '-';
                else res3 = take3.ToString().ToCharArray()[0];

                return IsSpecial ? $"{res1} {res2} {res3}" : $"{res1}   {res2}";
            }
            else
            {
                // frame 1-9
                return IsStrike ? $"  {res1}" : $"{res1} {res2}";
            }
        }
    }
}
