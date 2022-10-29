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
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace GUI
{
    /// <summary>
    /// Interaction logic for MainWindow.xaml
    /// </summary>
    public partial class MainWindow : Window
    {
        public MainWindow()
        {
            InitializeComponent();

            Loaded += (sender, e) => {
                _ = listPlayers.Items.Add("Rémi");
                _ = listPlayers.Items.Add("Vincent");
                _ = listPlayers.Items.Add("Théo");
                _ = listPlayers.Items.Add("Paul");
                _ = listPlayers.Items.Add("Florian");

                OnClick_Validate(sender, e);
            };
        }

        private void OnClick_Add(object sender, RoutedEventArgs e)
        {
            if (inputBox.Text == "")
            {
                return;
            }

            _ = listPlayers.Items.Add(inputBox.Text);
            inputBox.Clear();
        }

        private void InputBox_KeyDown(object sender, KeyEventArgs e)
        {
            if (e.Key == Key.Enter) OnClick_Add(sender, e);
        }

        private void OnClick_Remove(object sender, RoutedEventArgs e)
        {
            object selection = listPlayers.SelectedItem;
            listPlayers.Items.Remove(selection);
        }

        private void OnClick_Validate(object sender, RoutedEventArgs e)
        {
            ScoreUI scoreUI = new(listPlayers);
            scoreUI.Show();
            Close();
        }
    }
}
