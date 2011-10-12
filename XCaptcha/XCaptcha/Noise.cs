namespace XCaptcha
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public abstract class Noise : INoise
    {
        protected Noise(System.Drawing.Brush brush)
        {
            if (brush == null)
            {
                throw new ArgumentNullException("brush");
            }
            this.Brush = brush;
        }

        public abstract void Create(Graphics graphics, ICanvas canvas);

        public System.Drawing.Brush Brush { get; set; }
    }
}

