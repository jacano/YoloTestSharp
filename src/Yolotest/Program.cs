using Alturos.Yolo;
using System.IO;

namespace Yolotest
{
    class Program
    {
        static void Main(string[] args)
        {
            var data = File.ReadAllBytes(@"Motorbike1.jpg");

            using (var yoloWrapper = new YoloWrapper("yolov2-tiny-voc.cfg", "yolov2-tiny-voc.weights", "voc.names"))
            {
                var items = yoloWrapper.Detect(data);
                //items[0].Type -> "Person, Car, ..."
                //items[0].Confidence -> 0.0 (low) -> 1.0 (high)
                //items[0].X -> bounding box
                //items[0].Y -> bounding box
                //items[0].Width -> bounding box
                //items[0].Height -> bounding box
            }
        }
    }
}
