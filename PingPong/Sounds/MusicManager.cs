using System.IO;
using System.Windows.Media;

namespace PingPong.Sounds
{
    public static class MusicManager
    {
        private static MediaPlayer mediaPlayer = new MediaPlayer();
        private static bool isPaused = false;
        private static bool isPlaying = false;

        private static string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;

        public static string musicFilePath = Path.Combine(projectDirectory, "Resources", "music.mp3");

        public static void PlayMusic()
        {
            if (!isPaused)
            {
                mediaPlayer.Open(new Uri(musicFilePath, UriKind.RelativeOrAbsolute));
                mediaPlayer.MediaEnded += (s, e) => mediaPlayer.Position = TimeSpan.Zero;
                mediaPlayer.Play();
                mediaPlayer.Volume = 0.3;
                isPlaying = true;
            }
            else
            {
                mediaPlayer.Play();
                isPaused = false;
                isPlaying = true;
            }
        }

        public static void PauseMusic()
        {
            mediaPlayer.Pause();
            isPaused = true;
            isPlaying = false;
        }

        public static void StopMusic()
        {
            mediaPlayer.Stop();
            isPaused = false;
            isPlaying = false;
        }

        public static bool IsMusicPlaying()
        {
            return isPlaying;
        }
    }
}
