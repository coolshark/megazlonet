using System;
using System.Drawing;
using System.Runtime.CompilerServices;

namespace XCaptcha
{
	public abstract class Canvas : ICanvas
	{
		protected Canvas(int width, int height, System.Drawing.Brush brush)
		{
			if (brush == null)
			{
				throw new ArgumentNullException("brush");
			}
			this.Width = width;
			this.Height = height;
			this.Brush = brush;
		}

		public void Create(Graphics graphics)
		{
			Rectangle rect = new Rectangle(0, 0, this.Width, this.Height);
			graphics.FillRectangle(this.Brush, rect);
		}

		public System.Drawing.Brush Brush { get; set; }

		public int Height { get; set; }

		public int Width { get; set; }
	}
}

