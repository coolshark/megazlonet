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
			FirstName: $("#FirstName").val(),
			Email: $("#Email").val(),
			Text: $("#Text").val(),
			PostId: $("#PostID").val()
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
						$('ol.commentlist').append(data);
						this.reset();
					});
				} else
					alert(data);
			}
		});
	});
});
