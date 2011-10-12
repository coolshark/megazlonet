namespace XCaptcha
{
    using System.Drawing.Drawing2D;

    public interface IDistort
    {
        GraphicsPath Create(GraphicsPath path, ICanvas canvas);
    }
}

