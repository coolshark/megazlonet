using System;
using System.ComponentModel;
using System.ComponentModel.DataAnnotations;
using System.Globalization;
using System.Web;

namespace XCaptcha.Web.Mvc {
	[AttributeUsage(AttributeTargets.Class, AllowMultiple = true, Inherited = true)]
	public sealed class AttemptMustMatchEncryptedSolutionAttribute : ValidationAttribute {
		private readonly string _secretKey;
		private readonly object _typeId = new object();

		public AttemptMustMatchEncryptedSolutionAttribute(string attemptProperty, string encrypedSolutionProperty, string secretKey) {
			AttemptProperty = attemptProperty;
			EncrypedSolutionProperty = encrypedSolutionProperty;
			_secretKey = secretKey;
			EncryptionProvider = new EncryptionProvider();
		}

		public string AttemptProperty { get; private set; }
		public string EncrypedSolutionProperty { get; private set; }

		public EncryptionProvider EncryptionProvider { get; set; }
		public override object TypeId { get { return _typeId; } }

		public override string FormatErrorMessage(string name) {
			return String.Format(CultureInfo.CurrentUICulture, ErrorMessageString, AttemptProperty, EncrypedSolutionProperty);
		}

		public override bool IsValid(object value) {
			var properties = TypeDescriptor.GetProperties(value);
			var attempt = properties.Find(AttemptProperty, true /* ignoreCase */).GetValue(value);
			var encrypedSolution = properties.Find(EncrypedSolutionProperty, true /* ignoreCase */).GetValue(value);

			if (encrypedSolution != null && attempt != null) {
				var solution = EncryptionProvider.Decrypt(HttpUtility.UrlDecode(encrypedSolution.ToString()), _secretKey);
				return String.Equals(attempt.ToString(), solution, StringComparison.CurrentCultureIgnoreCase);
			}
			return false;
		}
	}
}