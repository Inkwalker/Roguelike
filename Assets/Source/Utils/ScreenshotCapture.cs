using System.IO;
using UnityEngine;

namespace Roguelike.Utils
{
    public class ScreenshotCapture : MonoBehaviour
    {
        [SerializeField] private KeyCode screenshotKey;

        private void LateUpdate()
        {
            if (Input.GetKeyDown(screenshotKey))
            {
                string fileName = ScreenShotName();
                ScreenCapture.CaptureScreenshot(fileName);
                Debug.Log("Screenshot saved at path " + fileName);
            }
        }

        public static string ScreenShotName()
        {
            string picturesFolder = System.Environment.GetFolderPath(System.Environment.SpecialFolder.MyPictures);
            string screenshotsDirectory = string.Format("{0}/{1}", picturesFolder, Application.productName);

            Directory.CreateDirectory(screenshotsDirectory);

            return string.Format("{0}/screen_{1}.png",
                                 screenshotsDirectory,
                                 System.DateTime.Now.ToString("yyyy-MM-dd_HH-mm-ss"));
        }
    }
}
