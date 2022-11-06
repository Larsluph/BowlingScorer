using System.Windows.Controls;

namespace GUI
{
    class Frame : Base.Frame
    {
        private readonly TextBlock tb1;
        private readonly TextBlock tb2;
        private readonly TextBlock? tb3;

        public new int Shot1
        {
            get
            {
                return base.Shot1;
            }
            set
            {
                base.Shot1 = value;
                tb1.Text = FormattedShot1.ToString();
            }
        }
        public new int Shot2
        {
            get
            {
                return base.Shot2;
            }
            set
            {
                base.Shot2 = value;
                tb2.Text = FormattedShot2.ToString();
            }
        }
        public new int Shot3
        {
            get
            {
                return base.Shot3;
            }
            set
            {
                base.Shot3 = value;
                
                // The above property should throw an Exception if tb3 is null
                #pragma warning disable CS8602 // Dereference of a possibly null reference.
                tb3.Text = FormattedShot3.ToString();
                #pragma warning restore CS8602 // Dereference of a possibly null reference.
            }
        }

        public Frame(int index): base(index)
        {
            tb1 = new();
            tb2 = new();
            tb3 = Is10th ? new() : null;
        }
    }
}