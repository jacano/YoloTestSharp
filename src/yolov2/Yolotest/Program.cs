using Alturos.Yolo;
using OpenCvSharp;
using System.Linq;

namespace Yolotest
{
    class Program
    {
        static void Main(string[] args)
        {
            var windowCapture = new Window("windowCapture");
            var capture = new VideoCapture(0);
            var image = new Mat();
            var yoloWrapper = new YoloWrapper("yolov2-tiny-voc.cfg", "yolov2-tiny-voc.weights", "voc.names", DetectionSystem.GPU);

            while (true)
            {
                capture.Read(image);
                if (image.Empty())
                    break;

                var bytes = image.ToBytes(".png");
                var items = yoloWrapper.Detect(bytes).ToArray();

                foreach (var item in items)
                {
                    var value = (item.Confidence * 100).ToString("0");
                    var text = $"{item.Type} - {value}%";
                    ImageUtilHelper.AddBoxToImage(image, text, item.X, item.Y, item.X + item.Width, item.Y + item.Height);
                }

                windowCapture.ShowImage(image);

                if ((Cv2.WaitKey(25) & 0xFF) == 'q')
                {
                    Cv2.DestroyAllWindows();
                    break;
                }
            }
        }
    }
}
