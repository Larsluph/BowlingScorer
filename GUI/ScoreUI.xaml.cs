using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for ScoreUI.xaml
    /// </summary>
    public partial class ScoreUI : Window
    {
        /// <summary>
        /// <para>columnCount is a constant which is composed of</para>
        /// <para>1 for the player names</para>
        /// <para>2*9 = 18 for the 9 first frames (2 shots per frame)</para>
        /// <para>3 for the 10th frame</para>
        /// <para>1 for the total</para>
        /// </summary>
        private readonly int columnCount = 1 + 2 * 9 + 3 + 1;
        private readonly int rowCount;

        private readonly List<string> players;

        public ScoreUI()
        {
            InitializeComponent();
            players = new();
        }

        public ScoreUI(List<string> listPlayers) : this()
        {
            players = listPlayers;

            rowCount = 1 + (2 * players.Count); // Header + 2 lines per player (frame values + frame total)

            for (int i = 0; i < rowCount; i++)
                grid.RowDefinitions.Add(new());
            for (int i = 0; i < columnCount; i++)
                grid.ColumnDefinitions.Add(new());

            // Header
            GenHeaderRow();

            // Players
            GenPlayerRows();
        }

        private void GenCell(string text, int row, int column, int rowSpan = 1, int columnSpan = 1)
        {
            TextBlock txt = new()
            {
                Text = text,
                TextAlignment = TextAlignment.Center
            };

            Grid.SetRow(txt, row);
            Grid.SetRowSpan(txt, rowSpan);

            Grid.SetColumn(txt, column);
            Grid.SetColumnSpan(txt, columnSpan);

            grid.Children.Add(txt);
        }

        private void GenHeaderRow()
        {
            //Player name
            GenCell("Player Name", 0, 0);

            //Frame 1 to 9
            for (int i = 1; i <= 9; i++)
                GenCell(i.ToString(), 0, 1 + (2 * (i - 1)), columnSpan: 2);

            //Frame 10
            GenCell("10", 0, 19, columnSpan: 3);

            //Total
            GenCell("Total", 0, 22);
        }

        private void GenPlayerRows()
        {
            for (int i = 0; i < players.Count; i++)
            {
                int row = 1 + (i * 2);

                // Name
                GenCell(players[i], row, 0, rowSpan: 2);

                for (int j = 0; j <= 9; j++)
                {
                    GenCell(" ", row, 1 + (2 * (i - 1)));
                }
            }
        }
    }
}
