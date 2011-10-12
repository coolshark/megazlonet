function xcaptchaSetCaptchaImage(solutionUrl, imageUrl) {
	$.ajaxSetup({ cache: false });
	$.get(solutionUrl, null,
		function (data) {
			var src = imageUrl + data;
			$('#-xcaptcha-hidden').val(data);
			$('#-xcaptcha-image').attr("src", src);
		});
}

$('#-xcaptcha-refresh').click(function () {
	xcaptchaChangeCaptchaImage();
	return false;
});
