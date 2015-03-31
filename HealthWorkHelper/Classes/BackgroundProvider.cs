using System;
using System.IO;
using System.Windows.Media.Imaging;

namespace HealthWorkHelper.Classes
{
    public class BackgroundProvider
    {
        public static readonly Random rndGenerator = new Random((int)DateTime.Now.Ticks);

        public BitmapImage GetBackground()
        {
            var images = Directory.GetFiles(Directory.GetCurrentDirectory() + @"\Images\", "*.jpg");
            if (images.Length == 0) return null;

            var file = images[rndGenerator.Next(0, images.Length)];
            return new BitmapImage(new Uri(file));
        }
    }
}
