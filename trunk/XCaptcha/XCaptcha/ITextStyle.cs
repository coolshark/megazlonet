namespace XCaptcha
{
    using System;
    using System.Drawing;

    public interface ITextStyle
    {
        PointF CalcStartPoint(Graphics graphics, ICanvas canvas, string text);

        System.Drawing.Brush Brush { get; set; }

        System.Drawing.Font Font { get; set; }
    }
}

