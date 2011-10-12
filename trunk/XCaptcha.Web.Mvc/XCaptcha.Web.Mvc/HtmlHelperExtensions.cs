using System;
using System.Web.Mvc;
using System.Web.UI;

namespace XCaptcha.Web.Mvc
{

	public class XCaptchaHtmlHelper<T>
	{
		public XCaptchaHtmlHelper(UrlHelper urlHelper)
		{
			_urlHelper = urlHelper;
		}

		private readonly UrlHelper _urlHelper;

		public MvcHtmlString Image(System.Linq.Expressions.Expression<Func<T, string>>
			encryptedSolutionExpression, string tooltip = "refresh captcha")
		{

			var tagBuilder = new HtmlTextWriter(new System.IO.StringWriter());
			tagBuilder.AddAttribute(HtmlTextWriterAttribute.Id, "-xcaptcha-image");
			tagBuilder.RenderBeginTag(HtmlTextWriterTag.Img);
			tagBuilder.RenderEndTag();//image
			tagBuilder.AddAttribute(HtmlTextWriterAttribute.Id, "-xcaptcha-refresh");
			tagBuilder.AddAttribute(HtmlTextWriterAttribute.Type, "button");
			tagBuilder.AddAttribute(HtmlTextWriterAttribute.Value, tooltip);
			tagBuilder.RenderBeginTag(HtmlTextWriterTag.Input);
			tagBuilder.RenderEndTag();//button
			tagBuilder.AddAttribute(HtmlTextWriterAttribute.Name, ExpressionHelper.GetExpressionText(encryptedSolutionExpression));
			tagBuilder.AddAttribute(HtmlTextWriterAttribute.Id, "-xcaptcha-hidden");
			tagBuilder.AddAttribute(HtmlTextWriterAttribute.Type, "hidden");
			tagBuilder.RenderBeginTag(HtmlTextWriterTag.Input);
			tagBuilder.RenderEndTag();//hidden

			return MvcHtmlString.Create(tagBuilder.InnerWriter.ToString());
		}

		public MvcHtmlString Script()
		{
			var tagBuilder = new HtmlTextWriter(new System.IO.StringWriter());
			tagBuilder.AddAttribute(HtmlTextWriterAttribute.Type, "text/javascript");
			tagBuilder.RenderBeginTag(HtmlTextWriterTag.Script);
			tagBuilder.WriteLine("function xcaptchaChangeCaptchaImage(){");
			tagBuilder.WriteLine("var s = '" + _urlHelper.RouteUrl(RouteNames.EncryptedSolution) + "';");
			tagBuilder.WriteLine("var i = '" + _urlHelper.RouteUrl(RouteNames.Image) + string.Format("?{0}=';", "encryptedSolution"));
			tagBuilder.WriteLine("xcaptchaSetCaptchaImage(s,i);");
			tagBuilder.WriteLine("};");
			tagBuilder.WriteLine("$(document).ready(function () {xcaptchaChangeCaptchaImage();});");
			tagBuilder.WriteLine("$('#-xcaptcha-refresh').bind('click', function (e) {");
			tagBuilder.WriteLine("xcaptchaChangeCaptchaImage();});");
			tagBuilder.RenderEndTag();//script
			return MvcHtmlString.Create(tagBuilder.InnerWriter.ToString());
		}
	}

	public static partial class HtmlHelperExtensions
	{
		public static XCaptchaHtmlHelper<T> XCaptcha<T>(this HtmlHelper<T> helper)
		{
			return new XCaptchaHtmlHelper<T>(new UrlHelper(helper.ViewContext.RequestContext));
		}
	}


}