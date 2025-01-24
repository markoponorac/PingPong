using System.Windows;
using MaterialDesignThemes.Wpf;
using PingPong.Sounds;
using System.Linq;

namespace PingPong
{
    public partial class MainWindow : Window
    {
        public static bool flag = false;
        public MainWindow()
        {
            InitializeComponent();
            if (!flag)
            {
                ApplyInitialTheme();
                MusicManager.PlayMusic();
                flag = true;
            }
        }

        private void ApplyInitialTheme()
        {
            var paletteHelper = new PaletteHelper();
            var theme = paletteHelper.GetTheme();
            theme.SetBaseTheme(BaseTheme.Light); 
            paletteHelper.SetTheme(theme);
        }

        private void changeTheme()
        {
            var paletteHelper = new PaletteHelper();
            var theme = paletteHelper.GetTheme();
            if (theme.GetBaseTheme() == BaseTheme.Dark)
            {
                theme.SetBaseTheme(BaseTheme.Light);
            }
            else
            {
                theme.SetBaseTheme(BaseTheme.Dark);
            }
            paletteHelper.SetTheme(theme);
        }



        private void ToggleTheme_Click(object sender, RoutedEventArgs e)
        {
            changeTheme();
        }

        private void ToggleGameSound_Click(object sender, RoutedEventArgs e)
        {
            SoundManager.IsSoundEnabled = !SoundManager.IsSoundEnabled;
        }

        private void ToggleMusic_Click(object sender, RoutedEventArgs e)
        {
            if (MusicManager.IsMusicPlaying())
                MusicManager.PauseMusic();
            else
                MusicManager.PlayMusic();
        }

        private void ViewHighScores_Click(object sender, RoutedEventArgs e)
        {
            HighScoresWindow highScoresWindow = new HighScoresWindow();
            highScoresWindow.Show();
            this.Close();
        }

        private void SinglePlayer_Click(object sender, RoutedEventArgs e)
        {
            SinglePlayerGameWindow singlePlayerGameWindow = new SinglePlayerGameWindow();
            singlePlayerGameWindow.Show();
            this.Close();
        }

        private void TwoPlayers_Click(object sender, RoutedEventArgs e)
        {
            TwoPlayerGameWindow twoPlayerGameWindow = new TwoPlayerGameWindow();
            twoPlayerGameWindow.Show();
            this.Close();
        }

        private void ExitGame_Click(object sender, RoutedEventArgs e)
        {
            Application.Current.Shutdown();
        }
    }
}