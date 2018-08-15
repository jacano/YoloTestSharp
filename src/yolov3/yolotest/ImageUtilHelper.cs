using OpenCvSharp;
using System;

namespace Tensorflow
{
    public static class ImageUtilHelper
    {
        public static void AddBoxToImage(Mat image, string text, float left, float top, float right, float bottom, Scalar color, int fontSizeHeight)
        {
            var xP1 = Math.Min(Math.Max(left, 0), image.Width - 1);
            var yP1 = Math.Min(Math.Max(top, 0), image.Height - 1);

            var xP2 = Math.Min(Math.Max(right, 0), image.Width - 1);
            var yP2 = Math.Min(Math.Max(bottom, 0), image.Height - 1);

            var pt1 = new Point(xP1, yP1);
            var pt2 = new Point(xP2, yP2);

            Cv2.Rectangle(image, pt1, pt2, color);

            var org = new Point(left, top - fontSizeHeight);
            Cv2.PutText(image, text, org, HersheyFonts.HersheyPlain, 1.0, color);
        }

        public static Mat CropImage(Mat image, float left, float top, float right, float bottom)
        {
            var x = left;
            var y = top;

            var width = (right - left);
            var height = (bottom - top);

            var imgRect = new Rect(new Point(0, 0), new Size(image.Width, image.Height));
            var rectCrop = new Rect(new Point(x, y), new Size(width, height));

            var final = rectCrop.Intersect(imgRect);

            var finalImage = new Mat(image, final);
            var croppedImage = finalImage.Clone();

            return croppedImage;
        }
    }
}
