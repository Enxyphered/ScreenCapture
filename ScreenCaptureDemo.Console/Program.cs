using System;
using System.Diagnostics;
using System.Threading.Tasks;
using ScreenCapture.Direct3D;

namespace ScreenCaptureDemo.ConsoleDemo
{
    class Program
    {
        private static Direct3DCapture capture = new Direct3DCapture();
        private static Stopwatch stopwatch = new Stopwatch();

        //PixelPos starts at 0,0
        private static int x = 1919;
        private static int y = 1079;
        private static int pixelStartPos { get => x * 4 + y * capture.Width * 4; }
        private static int frameCount = 0;

        static void Main(string[] args)
        {
            capture.CapturedEvent += NextFrameLoaded;

            stopwatch.Start();
            Next();
            Console.ReadLine();
        }

        private static void Next()
        {
            if (frameCount++ > 1000)
            {
                stopwatch.Stop();
                Console.WriteLine($"Average frame processed in { (stopwatch.ElapsedMilliseconds/1000).ToString("#.##") }ms");
                return;
            }

            capture.NextFrame();
        }

        private static void NextFrameLoaded(Span<byte> buffer)
        {
            Span<byte> pixel = buffer.Slice(pixelStartPos, 4);
            var b = Convert.ToInt32(pixel[0]);
            var g = Convert.ToInt32(pixel[1]);
            var r = Convert.ToInt32(pixel[2]);
            //var a = Convert.ToInt32(pixel[3]); 

            Console.WriteLine($"R:{r} G:{g} B:{b}");
            Task.Factory.StartNew(Next);
        }
    }
}
