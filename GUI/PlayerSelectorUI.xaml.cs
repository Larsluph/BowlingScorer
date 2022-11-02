using System.Windows;
using System.Windows.Input;

namespace GUI
{
    /// <summary>
    /// Interaction logic for PlayerSelectorUI.xaml
    /// </summary>
    public partial class PlayerSelectorUI : Window
    {
        public PlayerSelectorUI()
        {
            InitializeComponent();

            //Loaded += (sender, e) => {
            //    _ = listPlayers.Items.Add("Rémi");
            //    _ = listPlayers.Items.Add("Vincent");
            //    _ = listPlayers.Items.Add("Théo");
            //    _ = listPlayers.Items.Add("Paul");
            //    _ = listPlayers.Items.Add("Florian");

            //    OnClick_Validate(sender, e);
            //};
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
            Close();
        }
    }
}
