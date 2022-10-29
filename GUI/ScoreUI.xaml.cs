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
        private int columnCount = 23;
        private int rowCount = 3;

        private List<string> players;

        public ScoreUI()
        {
            InitializeComponent();
            players = new();
        }

        public ScoreUI(ListBox listPlayers) : this()
        {
            players = listPlayers.Items.Cast<string>().ToList();

            rowCount = 1 + (2 * players.Count);

            for (int i = 0; i < columnCount; i++)
            {
                grid.ColumnDefinitions.Add(new());
            }

            //Header

            //Player name
            TextBlock txt = new()
            {
                Text = "Player Name",
                TextAlignment = TextAlignment.Center
            };
            Grid.SetRow(txt, 0);
            Grid.SetColumn(txt, 0);
            grid.Children.Add(txt);

            //Frame 1 to 9
            for (int i = 1; i < 10; i++)
            {
                txt = new()
                {
                    Text = i.ToString(),
                    TextAlignment = TextAlignment.Center
                };
                Grid.SetRow(txt, 0);
                Grid.SetColumn(txt, 1 + (2 * (i - 1)));
                Grid.SetColumnSpan(txt, 2);

                grid.Children.Add(txt);
            }

            //Frame 10
            txt = new()
            {
                Text = "10",
                TextAlignment = TextAlignment.Center
            };
            Grid.SetRow(txt, 0);
            Grid.SetColumn(txt, 19);
            Grid.SetColumnSpan(txt, 3);
            grid.Children.Add(txt);

            //Total
            txt = new()
            {
                Text = "Total",
                TextAlignment = TextAlignment.Center
            };
            Grid.SetRow(txt, 0);
            Grid.SetColumn(txt, 22);
            grid.Children.Add(txt);

            for (int i = 0; i < players.Count; i++)
            {
                grid.RowDefinitions.Add(new RowDefinition());
                grid.RowDefinitions.Add(new RowDefinition());
                txt = new()
                {
                    Text = players[i],
                    TextAlignment = TextAlignment.Center
                };
                Grid.SetRow(txt, 1 + (i * 2));
                Grid.SetRowSpan(txt, 2);
                Grid.SetColumn(txt, 0);

                grid.Children.Add(txt);
            }
        }
    }
}
