namespace XCaptcha
{
    using System;
    using System.Drawing;

    public interface INoise
    {
        void Create(Graphics graphics, ICanvas canvas);

        System.Drawing.Brush Brush { get; set; }
    }
}

