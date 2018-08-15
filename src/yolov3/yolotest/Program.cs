using System;
using System.Linq;
using Darknet;
using OpenCvSharp;
using Tensorflow;

namespace yolotest
{
    class Program
    {
        static void Main(string[] args)
        {
            var yolo = new YoloWrapper("yolov3.cfg", "yolov3.weights", 0);

            var windowCapture = new Window("windowCapture");
            var capture = new VideoCapture(0);
            var image = new Mat();

             while (true)
            {
                capture.Read(image);
                if (image.Empty())
                    break;

                var bytes = image.ToBytes(".png");
                var bbox = yolo.Detect(bytes);
                if (bbox == null)
                {
                    continue;
                }

                var items = bbox.ToArray();

                foreach (var item in items)
                {
                    var value = (item.prob * 100).ToString("0");
                    var text = $"{item.obj_id} - {value}%";

                    var error = item.x > item.y;
                    if (error)
                    {
                        continue;
                    }

                    ImageUtilHelper.AddBoxToImage(image, text, (float)item.x, (float)item.y, (float)item.y + item.w, (float)item.y + item.h, new Scalar(255,0,0), 10);
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
