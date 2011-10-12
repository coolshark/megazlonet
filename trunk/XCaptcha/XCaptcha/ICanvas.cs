namespace XCaptcha
{
    using System;
    using System.Drawing;

    public interface ICanvas
    {
        void Create(Graphics graphics);

        System.Drawing.Brush Brush { get; set; }

        int Height { get; set; }

        int Width { get; set; }
    }
}

