using System;
using System.Web;
using System.Drawing;

namespace megazlo.Code {
	public class AvatarUploader {

		public void LoadAvatar(object data) {
			object[] ar = (object[])data;
			HttpPostedFileBase ava = (HttpPostedFileBase)ar[0];
			string userName = (string)ar[1];
			if (ava == null || ava.FileName.Length == 0)
				return;
			if (!ava.ContentType.Contains("image"))
				return;
			string file = AppDomain.CurrentDomain.BaseDirectory + "Upload\\Avatars\\" + userName + GetExt(ava.FileName);
			ava.SaveAs(file);
			ResizeImage(file, 100, 100, true);
		}

		private string GetExt(string file) {
			int ind = file.LastIndexOf('.');
			return file.Substring(ind, file.Length - ind);
		}

		public void ResizeImage(string OriginalFile, int NewWidth, int MaxHeight, bool OnlyResizeIfWider) {
			Image FullsizeImage = Image.FromFile(OriginalFile);
			if (FullsizeImage.Width < 100 && FullsizeImage.Height < 100) {
				FullsizeImage.Dispose();
				return;
			}
			// Prevent using images internal thumbnail
			FullsizeImage.RotateFlip(RotateFlipType.Rotate180FlipNone);
			FullsizeImage.RotateFlip(RotateFlipType.Rotate180FlipNone);

			if (OnlyResizeIfWider)
				if (FullsizeImage.Width <= NewWidth)
					NewWidth = FullsizeImage.Width;

			int NewHeight = FullsizeImage.Height * NewWidth / FullsizeImage.Width;
			if (NewHeight > MaxHeight) {// Resize with height instead				
				NewWidth = FullsizeImage.Width * MaxHeight / FullsizeImage.Height;
				NewHeight = MaxHeight;
			}
			Image NewImage = FullsizeImage.GetThumbnailImage(NewWidth, NewHeight, null, IntPtr.Zero);
			// Clear handle to original file so that we can overwrite it if necessary
			FullsizeImage.Dispose();
			// Save resized picture
			NewImage.Save(OriginalFile);
		}
	}
}