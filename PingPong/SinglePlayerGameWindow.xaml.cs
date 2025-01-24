using System;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Shapes;
using System.Windows.Threading;
using MaterialDesignThemes.Wpf;
using PingPong.Sounds;
using System.IO;
using System.Globalization;
using System.Windows.Data;
using static MaterialDesignThemes.Wpf.Theme.ToolBar;

namespace PingPong
{
    public partial class SinglePlayerGameWindow : Window
    {
        private DispatcherTimer gameTimer;
        private Random random;
        private double ballSpeedX, ballSpeedY;
        private int playerScore, computerScore;
        private DateTime gameStartTime;
        private bool playerUp, playerDown;
        private double PaddleSpeed = 5;
        private const double InitialBallSpeed = 5;
        private bool isGamePaused = false;
        private const double ComputerDifficulty = 0.76;

        public SinglePlayerGameWindow()
        {
            InitializeComponent();
            InitializeGame();
            ApplyTheme();
            UpdateGameColors();
            SizeChanged += SinglePlayerGameWindow_SizeChanged;
        }

        private void InitializeGame()
        {
            random = new Random();
            SetInitialPositions();
            gameTimer = new DispatcherTimer();
            gameTimer.Tick += GameTimer_Tick;
            gameTimer.Interval = TimeSpan.FromMilliseconds(16); 
            gameTimer.Start();
            gameStartTime = DateTime.Now;

            KeyDown += SinglePlayerGameWindow_KeyDown;
            KeyUp += SinglePlayerGameWindow_KeyUp;
        }

        private void SetInitialPositions()
        {
            Canvas.SetLeft(PlayerPaddle, 20);
            Canvas.SetTop(PlayerPaddle, GameCanvas.ActualHeight / 2 - PlayerPaddle.Height / 2);
            Canvas.SetLeft(ComputerPaddle, GameCanvas.ActualWidth - 20 - ComputerPaddle.Width);
            Canvas.SetTop(ComputerPaddle, GameCanvas.ActualHeight / 2 - ComputerPaddle.Height / 2);

            ResetBall();
        }

        private void ResetBall()
        {
            Canvas.SetLeft(Ball, GameCanvas.ActualWidth / 2 - Ball.Width / 2);
            Canvas.SetTop(Ball, GameCanvas.ActualHeight / 2 - Ball.Height / 2);
            ballSpeedX = (random.Next(2) == 0 ? -1 : 1) * InitialBallSpeed;
            ballSpeedY = (random.Next(2) == 0 ? -1 : 1) * InitialBallSpeed;
            PaddleSpeed = 5;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (!isGamePaused)
            {
                MoveBall();
                MovePlayerPaddle();
                MoveComputerPaddle();
                CheckCollision();
                UpdateScoreAndTime();
            }
        }

        private void MoveBall()
        {
            Canvas.SetLeft(Ball, Canvas.GetLeft(Ball) + ballSpeedX);
            Canvas.SetTop(Ball, Canvas.GetTop(Ball) + ballSpeedY);

            if (DateTime.Now - gameStartTime > TimeSpan.FromSeconds(10))
            {
                ballSpeedX *= 1.0005;
                ballSpeedY *= 1.0005;
                PaddleSpeed *= 1.0005; 
            }
        }

        private void MovePlayerPaddle()
        {
            if (playerUp && Canvas.GetTop(PlayerPaddle) > 0)
            {
                Canvas.SetTop(PlayerPaddle, Canvas.GetTop(PlayerPaddle) - PaddleSpeed);
            }
            if (playerDown && Canvas.GetTop(PlayerPaddle) + PlayerPaddle.Height < GameCanvas.ActualHeight)
            {
                Canvas.SetTop(PlayerPaddle, Canvas.GetTop(PlayerPaddle) + PaddleSpeed);
            }
        }

        private void MoveComputerPaddle()
        {
            double ballCenter = Canvas.GetTop(Ball) + Ball.Height / 2;
            double paddleCenter = Canvas.GetTop(ComputerPaddle) + ComputerPaddle.Height / 2;

            if (random.NextDouble() < ComputerDifficulty)
            {
                if (ballCenter < paddleCenter && Canvas.GetTop(ComputerPaddle) > 0)
                {
                    Canvas.SetTop(ComputerPaddle, Canvas.GetTop(ComputerPaddle) - PaddleSpeed);
                }
                else if (ballCenter > paddleCenter && Canvas.GetTop(ComputerPaddle) + ComputerPaddle.Height < GameCanvas.ActualHeight)
                {
                    Canvas.SetTop(ComputerPaddle, Canvas.GetTop(ComputerPaddle) + PaddleSpeed);
                }
            }
        }

        private void CheckCollision()
        {
            if (Canvas.GetTop(Ball) <= 0 || Canvas.GetTop(Ball) + Ball.Height >= GameCanvas.ActualHeight)
            {
                ballSpeedY = -ballSpeedY;
            }

            if (Ball.IntersectsWith(PlayerPaddle))
            {
                ballSpeedX = Math.Abs(ballSpeedX);
                SoundManager.PlayHitSound();
                AddRandomness();
            }

            if (Ball.IntersectsWith(ComputerPaddle))
            {
                ballSpeedX = -Math.Abs(ballSpeedX); 
                SoundManager.PlayHitSound();
                AddRandomness();
            }

            if (Canvas.GetLeft(Ball) <= 0)
            {
                computerScore++;
                ResetBall();
            }
            else if (Canvas.GetLeft(Ball) + Ball.Width >= GameCanvas.ActualWidth)
            {
                playerScore++;
                ResetBall();
            }
        }

        private void AddRandomness()
        {
            ballSpeedY += (random.NextDouble() - 0.5) * 2;
        }

        private void UpdateScoreAndTime()
        {
            PlayerScore.Text = $"Player: {playerScore}";
            ComputerScore.Text = $"Computer: {computerScore}";
            GameTime.Text = $"Time: {(DateTime.Now - gameStartTime):mm\\:ss}";
        }

        private void SinglePlayerGameWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                case Key.Up:
                    playerUp = true;
                    break;
                case Key.S:
                case Key.Down:
                    playerDown = true;
                    break;
            }
        }

        private void SinglePlayerGameWindow_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W:
                case Key.Up:
                    playerUp = false;
                    break;
                case Key.S:
                case Key.Down:
                    playerDown = false;
                    break;
            }
        }

        private void BackToMainMenu_Click(object sender, RoutedEventArgs e)
        {
            gameTimer.Stop();
            isGamePaused = true;
            SaveGameResults();
            MainWindow mainWindow = new MainWindow();
            mainWindow.Show();
            this.Close();
        }

        private void SaveGameResults()
        {
            string results = $"Date= {DateTime.Now.ToString("dd.MM.yyyy. hh:mm:ss")}, Player= {playerScore}, Computer= {computerScore}, Duration= {(DateTime.Now - gameStartTime):mm\\:ss}";
            File.AppendAllText("game_results.txt", results + Environment.NewLine);
        }

        private void ApplyTheme()
        {
            var paletteHelper = new PaletteHelper();
            var theme = paletteHelper.GetTheme();
            paletteHelper.SetTheme(theme);
        }

        public void UpdateGameColors()
        {
            var paletteHelper = new PaletteHelper();
            var theme = paletteHelper.GetTheme();

            Color paddleColor, ballColor, lineColor;
            if (theme.GetBaseTheme() == BaseTheme.Dark)
            {
                paddleColor = Colors.White;
                ballColor = Colors.LightGray;
                lineColor = Colors.White;
            }
            else
            {
                paddleColor = Colors.Black;
                ballColor = Colors.DarkGray;
                lineColor = Colors.Black;
            }

            Resources["PaddleColor"] = new SolidColorBrush(paddleColor);
            Resources["BallColor"] = new SolidColorBrush(ballColor);
            FieldBorder.Stroke = new SolidColorBrush(lineColor);
            CenterLine.Stroke = new SolidColorBrush(lineColor);
        }

        private void PauseResume_Click(object sender, RoutedEventArgs e)
        {
            isGamePaused = !isGamePaused;
            PauseResumeButton.Content = isGamePaused ? "Resume" : "Pause";
        }

        private void SinglePlayerGameWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SetInitialPositions();
        }
    }

}