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
		private const String imgType = "image/png";

		public ActionResult QR(string data) {
			int wid = 150, heg = 150;
			QRCodeWriter wrt = new QRCodeWriter();
			Hashtable hints = new Hashtable();
			hints.Add(EncodeHintType.CHARACTER_SET, "Shift_JIS");
			ByteMatrix mtr = wrt.encode(data, BarcodeFormat.QR_CODE, wid, heg, hints);
			Bitmap bmp = new Bitmap(wid, heg);
			for (int h = 0; h < mtr.Height; h++)
				for (int w = 0; w < mtr.Width; w++)
					bmp.SetPixel(w, h, mtr.Array[h][w] == 0 ? Color.Black : Color.FromArgb(0xBEC3C6));
			MemoryStream ms = new MemoryStream();
			bmp.Save(ms, ImageFormat.Png);
			byte[] bitmapData = ms.ToArray();
			ms.Close();
			return File(bitmapData, imgType);
		}
	}
}

