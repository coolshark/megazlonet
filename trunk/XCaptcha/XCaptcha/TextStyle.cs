namespace XCaptcha
{
    using System;
    using System.Drawing;
    using System.Runtime.CompilerServices;

    public abstract class TextStyle : ITextStyle
    {
        protected TextStyle(System.Drawing.Font font, System.Drawing.Brush brush)
        {
            if (font == null)
            {
                throw new ArgumentNullException("font");
            }
            if (brush == null)
            {
                throw new ArgumentNullException("brush");
            }
            this.Font = font;
            this.Brush = brush;
        }

        public virtual PointF CalcStartPoint(Graphics graphics, ICanvas canvas, string text)
        {
            SizeF fontSize = graphics.MeasureString(text, this.Font);
            float x = (canvas.Width - fontSize.Width) / 2.5f;
            int y = 0;
            return new PointF(x, (float) y);
        }

        public System.Drawing.Brush Brush { get; set; }

        public System.Drawing.Font Font { get; set; }
    }
}

