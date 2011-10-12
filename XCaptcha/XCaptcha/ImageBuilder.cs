using System;
using System.Drawing;
using System.Drawing.Drawing2D;
using System.Drawing.Imaging;
using System.IO;

namespace XCaptcha
{


	public class ImageBuilder
	{
		public ImageBuilder(ICanvas canvas, ITextStyle textStyle, IDistort distort, INoise noise, int solutionSize = 0, bool? showNoise = new bool?())
		{
			this.SolutionSize = solutionSize;
			this.Canvas = canvas;
			this.TextStyle = textStyle;
			this.Distort = distort;
			this.Noise = noise;
			this.ShowNoise = !showNoise.HasValue || Convert.ToBoolean(showNoise);
			this.ShowDistort = true;
		}

		public CaptchaResult Create(string text)
		{
			CaptchaResult crez;
			this.SolutionSize = (this.SolutionSize == 0) ? 5 : this.SolutionSize;
			this.TextStyle = this.TextStyle ?? new DefaultTextStyle();
			this.Canvas = this.Canvas ?? new DefaultCanvas();
			this.Distort = this.Distort ?? new DefaultDistort();
			this.Noise = this.Noise ?? new DefaultNoise();
			using (Bitmap bmp = new Bitmap(this.Canvas.Width, this.Canvas.Height))
			{
				using (Graphics g = Graphics.FromImage(bmp))
				{
					this.Canvas.Create(g);
					if (this.ShowNoise)
					{
						this.Noise.Create(g, this.Canvas);
					}
					GraphicsPath path = new GraphicsPath();
					PointF startPoint = this.TextStyle.CalcStartPoint(g, this.Canvas, text);
					g.PageUnit = GraphicsUnit.Point;
					path.AddString(text, this.TextStyle.Font.FontFamily, (int)this.TextStyle.Font.Style, this.TextStyle.Font.Size, startPoint, StringFormat.GenericDefault);
					if (this.ShowDistort)
					{
						path = this.Distort.Create(path, this.Canvas);
					}
					g.FillPath(this.TextStyle.Brush, path);
					g.SmoothingMode = SmoothingMode.HighQuality;
					g.Flush();
					MemoryStream stream = new MemoryStream();
					bmp.Save(stream, ImageFormat.Gif);
					crez = new CaptchaResult(text, stream.ToArray());
				}
			}
			return crez;
		}

		public ICanvas Canvas { get; set; }

		public IDistort Distort { get; set; }

		public INoise Noise { get; set; }

		public bool ShowDistort { get; set; }

		public bool ShowNoise { get; set; }

		public int SolutionSize { get; set; }

		public ITextStyle TextStyle { get; set; }
	}
}

