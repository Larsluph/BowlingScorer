namespace Base
{
    public class Frame
    {
        public int Index;
        private int take1;
        private int take2;
        private int take3;

        public bool IsShot1Filled => take1 != -1;
        public int Shot1
        {
            get
            {
                if (take1 == -1) throw new InvalidOperationException("Can't read null property");
                else return take1;
            }
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
        public char FormattedShot1
        {
            get
            {
                if (take1 == -1) return ' ';
                else if (IsStrike) return 'X';
                else if (take1 == 0) return '-';
                else return take1.ToString().ToCharArray()[0];
            }
        }

        public bool IsShot2Filled => take2 != -1;
        public int Shot2
        {
            get
            {
                if (take2 == -1) throw new InvalidOperationException("Can't read null property");
                else return take2;
            }
            set
            {
                if (take1 == -1) throw new InvalidOperationException("First shot must be specified before specifying second shot");
                else if (Is10th && IsStrike) take2 = value;
                else if (value < 0 || value > 10 - take1) throw new ArgumentOutOfRangeException(nameof(value), $"Value must be between 0 and {10 - take1}");
                else take2 = value;

                if (Is10th && take1 + take2 != 10)
                    take3 = 0;
            }
        }
        public char FormattedShot2
        {
            get
            {
                if (take2 == -1) return ' ';
                else if (IsSpare) return '/';
                else if (take2 == 0) return '-';
                else if (Is10th && take2 == 10) return 'X';
                else return take2.ToString().ToCharArray()[0];
            }
        }

        public bool IsShot3Filled => take3 != -1;
        public int Shot3
        {
            get
            {
                if (!Is10th) throw new InvalidOperationException("Third shot isn't readable when not in 10th frame");
                else if (take3 == -1) throw new InvalidOperationException("Can't read null property");
                else return take3;
            }
            set
            {
                if (!Is10th) throw new InvalidOperationException("Third shot is not writable outside 10th frame");
                else if (take1 == -1 || take2 == -1) throw new InvalidOperationException("Can't specify third shot if previous shots have not been specified");
                else if (IsStrike && value > 10 - take2 && take2 != 10) throw new ArgumentOutOfRangeException(nameof(value), $"Value must be between 0 and {10 - take2}");
                else if (value < 0 || value > 10) throw new ArgumentOutOfRangeException(nameof(value), "Value must be between 0 and 10");
                else take3 = value;
            }
        }
        public char FormattedShot3
        {
            get
            {
                if (!Is10th) throw new InvalidOperationException("Third shot isn't readable when not in 10th frame");
                else if (take3 == -1) return ' ';
                else if (take3 == 10) return 'X';
                else if (take2 + take3 == 10) return '/';
                else if (take3 == 0) return '-';
                else return take3.ToString().ToCharArray()[0];
            }
        }

        public Frame(int index)
        {
            if (index < 1 || index > 10) throw new ArgumentOutOfRangeException(nameof(index));

            Index = index;
            take1 = -1;
            take2 = -1;
            take3 = Is10th ? -1 : 0;
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
            if (Is10th)
                //frame 10
                return IsSpecial ? $"{FormattedShot1} {FormattedShot2} {FormattedShot3}" : $"{FormattedShot1}   {FormattedShot2}";
            else
                // frame 1-9
                return IsStrike ? $"  {FormattedShot1}" : $"{FormattedShot1} {FormattedShot2}";
        }
    }
}
