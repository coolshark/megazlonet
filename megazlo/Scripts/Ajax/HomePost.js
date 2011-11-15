/// <reference path="../jquery-1.7.min.js" />
/// <reference path="../jquery-ui-1.8.16.custom.min.js" />
/// <reference path="../jquery.validate.min.js" />
/// <reference path="../jquery.validate.unobtrusive.min.js" />

$(function () {
	var link;
	$(document).on('click', 'a.del_item', function (e) {
		e.preventDefault();
		link = $(this);
		showDelDial($(this));
	});

	function showDelDial(pst) {
		var dlg = $("#deldialg");
		dlg.dialog({
			resizable: false,
			width: 400,
			buttons: {
				"OK": function () {
					$(this).dialog("close");
					deletePost(pst);
				},
				Cancel: function () {
					$(this).dialog("close");
				}
			}
		});
		dlg.dialog("open");
	}

	function deletePost(pst) {
		$.ajax({
			url: $(pst).attr('href'),
			type: "POST",
			data: JSON.stringify(""),
			dataType: "json",
			contentType: "application/json; charset=utf-8",
			success: showCompleteDialog
		});
	}

	function showCompleteDialog(data) {
		var arr = data.split(';');
		var msg = arr[0];
		if (arr[1] == 'true')
			$(link).parents('.parent-container').first().remove();
		var dlg = $("#notedial");
		dlg.html(msg);
		dlg.dialog({
			resizable: false,
			width: 400,
			buttons: {
				"OK": function () {
					$(this).dialog("close");
				}
			}
		});
		dlg.dialog("open");
	}

	$(document).on('click', '#Submit_Comment', function (e) {
		e.preventDefault();
		var frm = $("form:first");
		$.validator.unobtrusive.parse('form:first');
		if (!frm.valid())
			return false;
		var comment = {
			FirstName: $("#FirstName").val(),
			Email: $("#Email").val(),
			IsAutor: $("#IsAutor").val(),
			Text: $("#Text").val(),
			PostId: $("#PostId").val(),
			CaptchaDeText: $("#CaptchaDeText").val(),
			CaptchaInputText: $("#CaptchaInputText").val()
		};
		sendCmnt(comment, frm.attr("action"));
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

});