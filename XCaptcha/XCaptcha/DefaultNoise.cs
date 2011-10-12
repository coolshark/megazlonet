namespace XCaptcha
{
    using System;
    using System.Drawing;
    using System.Drawing.Drawing2D;

    public class DefaultNoise : Noise
    {
        public DefaultNoise() : base(new SolidBrush(Color.Red))
        {
        }

        public override void Create(Graphics graphics, ICanvas canvas)
        {
            Random ran = new Random();
            PointF[] curvePs = new PointF[10];
            for (int u = 0; u < 5; u++)
            {
                curvePs[u].X = u * (canvas.Width / 5);
                curvePs[u].Y = canvas.Height / 2;
            }
            for (int u = 5; u < 10; u++)
            {
                curvePs[u].X = (u - 5) * (canvas.Width / 5);
                curvePs[u].Y = ran.Next(canvas.Height);
            }
            GraphicsPath path = new GraphicsPath();
            path.AddLines(curvePs);
            graphics.DrawPath(new Pen(base.Brush), path);
        }
    }
}

