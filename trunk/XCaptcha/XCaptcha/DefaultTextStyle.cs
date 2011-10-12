using System;
using System.Drawing;

namespace XCaptcha
{
	public class DefaultTextStyle : TextStyle
	{
		public DefaultTextStyle()
			: base(new Font("Verdana", 26f, FontStyle.Regular), Brushes.Red)
		{
		}
	}
}

