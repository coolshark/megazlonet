using System;
using System.Drawing;

namespace XCaptcha
{


	public class DefaultCanvas : Canvas
	{
		public DefaultCanvas()
			: base(0xaf, 50, new SolidBrush(Color.White))
		{
		}
	}
}

