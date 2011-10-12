using System.Web.Mvc;

namespace XCaptcha.Web.Mvc
{
	public class CaptchaImageResult : ActionResult
	{
		private readonly string _solution;
		private readonly ICanvas _canvas;
		private readonly ITextStyle _textStyle;
		private readonly IDistort _distort;
		private readonly INoise _noise;
		private readonly int _solutionSize;
		private readonly bool? _showNoise;

		public CaptchaImageResult(string solution, ICanvas canvas = null,
				ITextStyle textStyle = null, IDistort distort = null,
				 INoise noise = null, int solutionSize = 0, bool? showNoise = null)
		{
			_solution = solution;
			_canvas = canvas;
			_textStyle = textStyle;
			_distort = distort;
			_noise = noise;
			_solutionSize = solutionSize;
			_showNoise = showNoise;
		}

		public override void ExecuteResult(ControllerContext context)
		{
			var builder = new ImageBuilder(_canvas, _textStyle, _distort, _noise, _solutionSize, _showNoise);
			var result = builder.Create(_solution);

			var fileResult = new FileContentResult(result.Image, result.ContentType);
			fileResult.ExecuteResult(context);
		}
	}
}