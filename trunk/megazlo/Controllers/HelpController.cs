using System;
using System.Collections;
using System.Drawing;
using System.Drawing.Imaging;
using System.IO;
using System.Web.Mvc;
using com.google.zxing;
using com.google.zxing.common;
using com.google.zxing.qrcode;

namespace megazlo.Controllers {
	public class HelpController : Controller {
		private static QRCodeWriter wrt = new QRCodeWriter();
		private static Hashtable hints = new Hashtable();
		private const int size = 150;
		private static Bitmap bmp = new Bitmap(size, size);
		private const String imgType = "image/png";
		private Color clr = Color.FromArgb(0xBEC3C6);

		static HelpController() {
			hints.Add(EncodeHintType.CHARACTER_SET, "Shift_JIS");
		}

		public ActionResult QR(string data) {
			ByteMatrix mtr = wrt.encode(data, BarcodeFormat.QR_CODE, size, size, hints);
			for (int h = 0; h < mtr.Height; h++)
				for (int w = 0; w < mtr.Width; w++)
					bmp.SetPixel(w, h, mtr.Array[h][w] == 0 ? Color.Black : clr);
			MemoryStream ms = new MemoryStream();
			bmp.Save(ms, ImageFormat.Png);
			byte[] bitmapData = ms.ToArray();
			ms.Close();
			return File(bitmapData, imgType);
		}
	}
}
