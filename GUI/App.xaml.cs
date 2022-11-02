using System;
using System.Collections.Generic;
using System.Linq;
using System.Windows;

namespace GUI
{
    /// <summary>
    /// Interaction logic for App.xaml
    /// </summary>
    public partial class App : Application
    {
        App()
        {
            // Phase 1: Ask player names
            PlayerSelectorUI playerSelector = new();
            playerSelector.ShowDialog();
            List<string> playerNames = playerSelector.listPlayers.Items.Cast<string>().ToList();
            // Phase 1

            // Phase 2: Game loop
            ScoreUI scoreUI = new(playerNames);
            scoreUI.ShowDialog();
            // Phase 2

            // Phase 3: Scoreboard
            throw new NotImplementedException();
            // Phase 3
        }
    }
}
