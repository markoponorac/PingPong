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
    public partial class TwoPlayerGameWindow : Window
    {
        private DispatcherTimer gameTimer;
        private Random random;
        private double ballSpeedX, ballSpeedY;
        private int player1Score, player2Score;
        private DateTime gameStartTime;
        private bool player1Up, player1Down, player2Up, player2Down;
        private double PaddleSpeed = 5;
        private const double InitialBallSpeed = 5;
        private bool isGamePaused = false;

        public TwoPlayerGameWindow()
        {
            InitializeComponent();
            InitializeGame();
            ApplyTheme();
            UpdateGameColors();
            SizeChanged += TwoPlayerGameWindow_SizeChanged;
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

            KeyDown += TwoPlayerGameWindow_KeyDown;
            KeyUp += TwoPlayerGameWindow_KeyUp;
        }

        private void SetInitialPositions()
        {
            Canvas.SetLeft(LeftPaddle, 20);
            Canvas.SetTop(LeftPaddle, GameCanvas.ActualHeight / 2 - LeftPaddle.Height / 2);
            Canvas.SetLeft(RightPaddle, GameCanvas.ActualWidth - 20 - RightPaddle.Width);
            Canvas.SetTop(RightPaddle, GameCanvas.ActualHeight / 2 - RightPaddle.Height / 2);

            ResetBall();
        }

        private void ResetBall()
        {
            Canvas.SetLeft(Ball, GameCanvas.ActualWidth / 2 - Ball.Width / 2);
            Canvas.SetTop(Ball, GameCanvas.ActualHeight / 2 - Ball.Height / 2);
            ballSpeedX = (random.Next(2) == 0 ? -1 : 1) * InitialBallSpeed;
            ballSpeedY = (random.Next(2) == 0 ? -1 : 1) * InitialBallSpeed;
        }

        private void GameTimer_Tick(object sender, EventArgs e)
        {
            if (!isGamePaused)
            {
                MoveBall();
                MovePaddles();
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

        private void MovePaddles()
        {
            if (player1Up && Canvas.GetTop(LeftPaddle) > 0)
            {
                Canvas.SetTop(LeftPaddle, Canvas.GetTop(LeftPaddle) - PaddleSpeed);
            }
            if (player1Down && Canvas.GetTop(LeftPaddle) + LeftPaddle.Height < GameCanvas.ActualHeight)
            {
                Canvas.SetTop(LeftPaddle, Canvas.GetTop(LeftPaddle) + PaddleSpeed);
            }
            if (player2Up && Canvas.GetTop(RightPaddle) > 0)
            {
                Canvas.SetTop(RightPaddle, Canvas.GetTop(RightPaddle) - PaddleSpeed);
            }
            if (player2Down && Canvas.GetTop(RightPaddle) + RightPaddle.Height < GameCanvas.ActualHeight)
            {
                Canvas.SetTop(RightPaddle, Canvas.GetTop(RightPaddle) + PaddleSpeed);
            }
        }

        private void CheckCollision()
        {
            if (Canvas.GetTop(Ball) <= 0 || Canvas.GetTop(Ball) + Ball.Height >= GameCanvas.ActualHeight)
            {
                ballSpeedY = -ballSpeedY;
            }

            if (Ball.IntersectsWith(LeftPaddle))
            {
                ballSpeedX = Math.Abs(ballSpeedX);
                SoundManager.PlayHitSound();
                AddRandomness();
            }

            if (Ball.IntersectsWith(RightPaddle))
            {
                ballSpeedX = -Math.Abs(ballSpeedX);
                SoundManager.PlayHitSound();
                AddRandomness();
            }

            if (Canvas.GetLeft(Ball) <= 0)
            {
                player2Score++;
                ResetBall();
            }
            else if (Canvas.GetLeft(Ball) + Ball.Width >= GameCanvas.ActualWidth)
            {
                player1Score++;
                ResetBall();
            }
        }

        private void AddRandomness()
        {
            ballSpeedY += (random.NextDouble() - 0.5) * 2;
        }

        private void UpdateScoreAndTime()
        {
            Player1Score.Text = $"Player 1: {player1Score}";
            Player2Score.Text = $"Player 2: {player2Score}";
            GameTime.Text = $"Time: {(DateTime.Now - gameStartTime):mm\\:ss}";
        }

        private void TwoPlayerGameWindow_KeyDown(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W: player1Up = true; break;
                case Key.S: player1Down = true; break;
                case Key.Up: player2Up = true; break;
                case Key.Down: player2Down = true; break;
            }
        }

        private void TwoPlayerGameWindow_KeyUp(object sender, KeyEventArgs e)
        {
            switch (e.Key)
            {
                case Key.W: player1Up = false; break;
                case Key.S: player1Down = false; break;
                case Key.Up: player2Up = false; break;
                case Key.Down: player2Down = false; break;
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
            string results = $"Date= {DateTime.Now}, Player 1: {player1Score}, Player 2: {player2Score}, Duration= {(DateTime.Now - gameStartTime):mm\\:ss}";
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

        private void TwoPlayerGameWindow_SizeChanged(object sender, SizeChangedEventArgs e)
        {
            SetInitialPositions();
        }
    }

    public static class ShapeExtensions
    {
        public static bool IntersectsWith(this Shape shape1, Shape shape2)
        {

            Rect rect1 = new Rect(Canvas.GetLeft(shape1), Canvas.GetTop(shape1), shape1.Width, shape1.Height);
            Rect rect2 = new Rect(Canvas.GetLeft(shape2), Canvas.GetTop(shape2), shape2.Width, shape2.Height);
            return rect1.IntersectsWith(rect2);

        }
    }

    public class HalfValueConverter : IValueConverter
    {
        public object Convert(object value, Type targetType, object parameter, CultureInfo culture)
        {
            if (value is double doubleValue)
            {
                return doubleValue / 2;
            }
            return value;
        }

        public object ConvertBack(object value, Type targetType, object parameter, CultureInfo culture)
        {
            throw new NotImplementedException();
        }
    }
}