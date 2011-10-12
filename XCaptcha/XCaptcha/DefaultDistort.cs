using System;
using System.Drawing;
using System.Drawing.Drawing2D;

namespace XCaptcha
{
	public class DefaultDistort : Distort
	{
		public override GraphicsPath Create(GraphicsPath path, ICanvas canvas)
		{
			if (path == null)
			{
				throw new ArgumentNullException("path");
			}
			if (canvas == null)
			{
				throw new ArgumentNullException("canvas");
			}
			Random rnd = new Random();
			int width = canvas.Width;
			double amp = (2 * canvas.Height) / 0x55;
			double size = (rnd.NextDouble() * (width / 4)) + (width / 8);
			PointF[] pn = new PointF[path.PointCount];
			byte[] pt = new byte[path.PointCount];
			GraphicsPath np2 = new GraphicsPath();
			GraphicsPathIterator iter = new GraphicsPathIterator(path);
			for (int i = 0; i < iter.SubpathCount; i++)
			{
				bool closed;
				GraphicsPath sp = new GraphicsPath();
				iter.NextSubpath(sp, out closed);
				Matrix m = new Matrix();
				m.RotateAt(Convert.ToSingle((double)((rnd.NextDouble() * 30.0) - 15.0)), sp.PathPoints[0]);
				m.Translate((float)(-1 * i), 0f);
				sp.Transform(m);
				np2.AddPath(sp, true);
			}
			for (int i = 0; i < np2.PointCount; i++)
			{
				pn[i] = Wave(np2.PathPoints[i], amp, size);
				pt[i] = np2.PathTypes[i];
			}
			return new GraphicsPath(pn, pt);
		}

		private static PointF Wave(PointF p, double amp, double size)
		{
			p.Y = Convert.ToSingle((double)(Math.Sin(((double)p.X) / size) * amp)) + p.Y;
			p.X = Convert.ToSingle((double)(Math.Sin(((double)p.X) / size) * amp)) + p.X;
			return p;
		}
	}
}

