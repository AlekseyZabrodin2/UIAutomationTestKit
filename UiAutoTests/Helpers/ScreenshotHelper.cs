using System.Drawing;
using System.Windows.Forms;

namespace UiAutoTests.Helpers
{
    public class ScreenshotHelper
    {
        
        public string TakeScreenshot(string testName)
        {
            var fileName = $"{testName}.png";
            var filePath = GetScreenshotsTestDir(fileName);

            using ( var bitmap = new Bitmap(Screen.PrimaryScreen.Bounds.Width,
                                            Screen.PrimaryScreen.Bounds.Height))
            {
                using (var graphics = Graphics.FromImage(bitmap))
                {
                    graphics.CopyFromScreen(Point.Empty, Point.Empty, Screen.PrimaryScreen.Bounds.Size);
                }
                bitmap.Save(filePath);
            }

            return filePath;
        }

        public string GetScreenshotsTestDir(string fileName)
        {
            var screenFolderDir = Path.Combine(".\\logs\\ScreenShots\\", $"ScreenError_{DateTime.Now:_dd.MM_HH.mm.ss}");
            var screenshotsDir = string.Empty;

            if (!Directory.Exists(screenFolderDir))
            {
                Directory.CreateDirectory(screenFolderDir);
                screenshotsDir = Path.Combine(screenFolderDir, fileName);
            }

            return screenshotsDir;
        }
    }
}
