using OpenCvSharp;
using System;

namespace Yolotest
{
    public static class ImageUtilHelper
    {
        private static Point border = new Point(0, 0);

        private static int fontSizeHeight = 10;
        private static Scalar red = new Scalar(255, 0, 0);

        public static void AddBoxToImage(Mat image, string text, float left, float top, float right, float bottom)
        {
            var xP1 = Math.Min(Math.Max(left - border.X, 0), image.Width - 1);
            var yP1 = Math.Min(Math.Max(top - border.Y, 0), image.Height - 1);

            var xP2 = Math.Min(Math.Max(right + border.X, 0), image.Width - 1);
            var yP2 = Math.Min(Math.Max(bottom + border.Y, 0), image.Height - 1);

            var pt1 = new Point(xP1, yP1);
            var pt2 = new Point(xP2, yP2);

            Cv2.Rectangle(image, pt1, pt2, red);

            var org = new Point(left, top - fontSizeHeight);
            Cv2.PutText(image, text, org, HersheyFonts.HersheyPlain, 1.0, red);
        }
    }
}
