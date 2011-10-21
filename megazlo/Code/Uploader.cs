using System;
using System.IO;
using System.Net;
using System.Text;
using System.Xml;
using BinaryAnalysis.UnidecodeSharp;
using System.Web;

namespace megazlo.Code {
	public static class Uploader {
		public static string UploadPath = AppDomain.CurrentDomain.BaseDirectory + "Upload\\img\\";

		public static string ParceLink(string link) {
			StringBuilder bld = new StringBuilder(link.ToLower().Unidecode());
			char tir = '-';
			bld.Replace(' ', tir);
			for (int i = bld.Length - 1; i >= 0; i--) {
				char chr = bld[i];
				chr.ToString();
				if (!char.IsLetterOrDigit(bld[i]) && bld[i] != tir)
					bld.Remove(i, 1);
			}
			bld.Replace("--", "-");
			bld.Replace("--", "-");
			return bld.ToString();
		}

		public static string Parce(string post) {
			if (post.Length == 0)
				return post;
			XmlDocument doc = new XmlDocument();
			string predoc = post.Replace("&", "_+_");
			doc.LoadXml("<root>" + predoc + "</root>");
			SeachChild(doc.ChildNodes[0]);
			return doc.DocumentElement.InnerXml.Replace("_+_", "&");
		}

		private static void SeachChild(XmlNode elm) {
			if (elm.Name == "img")
				CopyImage(elm);
			else if (elm.HasChildNodes)
				foreach (XmlNode itm in elm.ChildNodes)
					SeachChild(itm);
		}

		private static void CopyImage(XmlNode elm) {
			string src = elm.Attributes["src"].Value.ToString();
			if (!src.StartsWith("http"))
				return;
			try {
				HttpWebRequest req = (HttpWebRequest)HttpWebRequest.Create(src);
				req.Credentials = CredentialCache.DefaultCredentials;
				req.UserAgent = "anything";
				string name = Guid.NewGuid().ToString();
				HttpWebResponse resp = (HttpWebResponse)req.GetResponse();
				string nameext = name + GetExtention(src);
				using (Stream stream = resp.GetResponseStream()) {
					using (FileStream fstream = new FileStream(UploadPath + nameext, FileMode.Append, FileAccess.Write)) {
						byte[] buffer = new byte[8192];
						int maxCount = buffer.Length, count;
						while ((count = stream.Read(buffer, 0, maxCount)) > 0)
							fstream.Write(buffer, 0, count);
					}
				}
				resp.Close();
				elm.Attributes["src"].Value = "/Upload/img/" + nameext;
			} catch {
			}
		}

		private static string GetExtention(string src) {
			int ind = src.LastIndexOf('.');
			if (ind == -1)
				return ".jpg";
			string rez = src.Substring(ind).ToLower();
			if (rez != ".jpg" || rez != ".png" || rez != ".gif" || rez != ".jpeg")
				return ".jpg";
			return rez;
		}
	}
}
