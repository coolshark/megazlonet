namespace XCaptcha
{
    using System;
    using System.Drawing.Drawing2D;

    public abstract class Distort : IDistort
    {
        protected Distort()
        {
        }

        public abstract GraphicsPath Create(GraphicsPath path, ICanvas canvas);
    }
}

