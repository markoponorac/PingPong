using System;
using System.Collections.Generic;
using System.Globalization;
using System.IO;
using System.Linq;
using System.Windows;
using MaterialDesignThemes.Wpf;
using static MaterialDesignThemes.Wpf.Theme.ToolBar;

namespace PingPong
{
    public partial class HighScoresWindow : Window
    {
        private const string ResultsFilePath = "game_results.txt";

        public HighScoresWindow()
        {
            InitializeComponent();
            ApplyTheme();
            LoadScores();
        }

        private void LoadScores()
        {
            if (File.Exists(ResultsFilePath))
            {
                var twoPlayerScores = new List<TwoPlayerScoreEntry>();
                var singlePlayerScores = new List<SinglePlayerScoreEntry>();
                var lines = File.ReadAllLines(ResultsFilePath);
                foreach (var line in lines)
                {
                    try
                    {
                        var parts = line.Split(',');

                        var dateString = parts[0].Split('=')[1].Trim();
                        var date = DateTime.ParseExact(dateString, "dd.MM.yyyy. HH:mm:ss", CultureInfo.InvariantCulture);

                        var durationString = parts[3].Split('=')[1].Trim();
                        var durationParts = durationString.Split(':');
                        var duration = new TimeSpan(0, int.Parse(durationParts[0]), int.Parse(durationParts[1]));

                        if (parts[1].Contains("Player 1"))
                        {
                            twoPlayerScores.Add(new TwoPlayerScoreEntry
                            {
                                Date = date,
                                Player1Score = int.Parse(parts[1].Split(':')[1].Trim()),
                                Player2Score = int.Parse(parts[2].Split(':')[1].Trim()),
                                Duration = duration
                            });
                        }
                        else if (parts[1].Contains("Player"))
                        {
                            singlePlayerScores.Add(new SinglePlayerScoreEntry
                            {
                                Date = date,
                                PlayerScore = int.Parse(parts[1].Split('=')[1].Trim()),
                                ComputerScore = int.Parse(parts[2].Split('=')[1].Trim()),
                                Duration = duration
                            });
                        }
                    }
                    catch (Exception ex)
                    {
                        Console.WriteLine($"Error parsing line: {line}. Error: {ex.Message}");
                    }

                }

                TwoPlayerScoresDataGrid.ItemsSource = twoPlayerScores.OrderByDescending(s => s.Date).ToList();
                SinglePlayerScoresDataGrid.ItemsSource = singlePlayerScores.OrderByDescending(s => s.Date).ToList();
            }
        }

        private void BackToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void RefreshScores_Click(object sender, RoutedEventArgs e)
        {
            LoadScores();
        }

        private void ClearScores_Click(object sender, RoutedEventArgs e)
        {
            var result = MessageBox.Show("Are you sure you want to clear all scores? This action cannot be undone.",
                "Confirm Clear Scores", MessageBoxButton.YesNo, MessageBoxImage.Warning);

            if (result == MessageBoxResult.Yes)
            {
                File.WriteAllText(ResultsFilePath, string.Empty);
                LoadScores();
            }
        }

        private void ApplyTheme()
        {
            var paletteHelper = new PaletteHelper();
            var theme = paletteHelper.GetTheme();
            paletteHelper.SetTheme(theme);
        }
    }

    public class TwoPlayerScoreEntry
    {
        public DateTime Date { get; set; }
        public int Player1Score { get; set; }
        public int Player2Score { get; set; }
        public TimeSpan Duration { get; set; }
    }

    public class SinglePlayerScoreEntry
    {
        public DateTime Date { get; set; }
        public int PlayerScore { get; set; }
        public int ComputerScore { get; set; }
        public TimeSpan Duration { get; set; }
    }
}