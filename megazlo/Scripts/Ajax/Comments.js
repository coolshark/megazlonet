/// <reference path="../jquery-1.7.min.js" />
/// <reference path="../jquery.validate.min.js" />
/// <reference path="../jquery.validate.unobtrusive.min.js" />
$(function () {

	$(document).on('click', '#Submit_Comment', function (e) {
		var frm = $("form:first");
		e.preventDefault();
		$.validator.unobtrusive.parse('form:first');
		if (!frm.valid())
			return false;
		var comment = {
			FirstName: $("#FirstName").val(),
			Email: $("#Email").val(),
			Text: $("#Text").val(),
			PostId: $("#PostId").val(),
			CaptchaDeText: $("#CaptchaDeText").val(),
			CaptchaInputText: $("#CaptchaInputText").val()
		};
		sendCmnt(comment, frm.attr("action"));
		return false;
	});

	$(document).on('click', '#primary a.del_comment', function (e) {
		var href = $(this).attr('href');
		var dlg = $("#confirmDialog");
		dlg.dialog({
			resizable: false,
			height: 180,
			width: 400,
			buttons: {
				"OK": function () {
					$(this).dialog("close");
					deleteComment(href);
				},
				Cancel: function () {
					$(this).dialog("close");
				}
			}
		});
		dlg.dialog("open");
		return false;
	});

	function sendCmnt(comment, action) {
		$.ajax({
			url: action,
			type: "POST",
			data: JSON.stringify(comment),
			dataType: "json",
			contentType: "application/json; charset=utf-8",
			success: function (data) {
				if (data.indexOf('<') > -1) {
					$("form:first").each(function () { this.reset(); });
					$('ol.commentlist').append(data);
				} else
					alert(data);
			}
		});
	}


	function deleteComment(hrf) {
		$.ajax({
			url: hrf,
			type: "POST",
			dataType: "json",
			contentType: "application/json; charset=utf-8",
			success: function (data) {
				if (data != false)
					$('#li-comment-' + data).remove();
				else
					alert('Ошибка удаления.');
			}
		});
	}
});