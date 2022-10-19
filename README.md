# BowlingScorer
.NET console app to generate a bowling scoreboard

## Summary

This app works in 3 phases.

### 1st phase: Player Selection

This phase is straight-forward. Simply enter the name of the players that you want to add to the current game.

When you're finished, enter a blank name in the next input and it will move on to the next phase.

### 2nd phase: Game Points

When prompted, input the player score at the current shot. The app will automatically change the shot / frame number according to the given input.

The entered score must be a number with the exception of 3 characters: X for a strike, / for a space and - for a gutter (0 points).

### 3rd phase: Final leaderboard

When all players have completed their 10 frames. The final scoreboard is displayed with a recap of the game and various statistics.
