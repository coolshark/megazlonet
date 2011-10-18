/// <reference path="../jquery-1.6.4.min.js" />
/// <reference path="../jquery.validate.min.js" />
/// <reference path="../jquery.validate.unobtrusive.min.js" />

$(function () {
	$("form:first").submit(function (e) {
		//$.validator.unobtrusive.parse(this)
		if (!$(this).valid())
			return false;
		e.preventDefault();
		$("form:first").validate().form();
		var comment = {
			FirstName: $("#NewComment_FirstName").val(),
			Email: $("#NewComment_Email").val(),
			Text: $("#NewComment_Text").val(),
			PostId: $("#NewComment_PostID").val()
		};

		$.ajax({
			url: $(this).attr("action"),
			type: "POST",
			data: JSON.stringify(comment),
			dataType: "json",
			contentType: "application/json; charset=utf-8",
			success: function (data) {
				if (data == true) {
					$('form:first').each(function () {
						this.reset();
					});
				} else
					alert(data);
			}
		});
	});
});
