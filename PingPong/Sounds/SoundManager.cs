using System;
using System.IO;
using System.Windows.Media;

namespace PingPong.Sounds
{
    public static class SoundManager
    {
        private static MediaPlayer mediaPlayer = new MediaPlayer();
        private static bool isSoundEnabled = true;

        private static string projectDirectory = Directory.GetParent(AppDomain.CurrentDomain.BaseDirectory).Parent.Parent.Parent.FullName;

        private static string hitSoundFilePath = Path.Combine(projectDirectory, "Resources", "ball_hit.mp3");

        private static bool isSoundLoaded = false;

        public static bool IsSoundEnabled
        {
            get { return isSoundEnabled; }
            set { isSoundEnabled = value; }
        }

        public static void PlayHitSound()
        {
            if (isSoundEnabled)
            {
                if (!isSoundLoaded)
                {
                    mediaPlayer.Open(new Uri(hitSoundFilePath, UriKind.Absolute));
                    isSoundLoaded = true;
                }

                mediaPlayer.Stop();
                mediaPlayer.Play();
            }
        }
    }
}
