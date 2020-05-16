using System;

namespace ScreenCapture
{
    public delegate void ScreenCaptured(Span<byte> buffer);

    public interface IScreenCapture
    {
        event ScreenCaptured CapturedEvent;
        void NextFrame();
    }
}
