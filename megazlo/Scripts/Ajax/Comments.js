/// <reference path="../jquery-1.6.4.min.js" />
/// <reference path="../jquery.validate.min.js" />
/// <reference path="../jquery.validate.unobtrusive.min.js" />

$(function () {
	var dlg = $("#confirmDialog");
	dlg.dialog({
		autoOpen: false,
		resizable: false,
		height: 180,
		width: 400,
		buttons: {
			"OK": function () {
				dlg.dialog("close");
				deleteComment();
			},
			Cancel: function () {
				dlg.dialog("close");
			}
		}
	});
	var frm = $("form:first");
	frm.submit(function (e) {
		if (!frm.valid())
			return false;
		e.preventDefault();
		var comment = {
			FirstName: $("#FirstName").val(),
			Email: $("#Email").val(),
			Text: $("#Text").val(),
			PostId: $("#PostID").val()
		};

		$.ajax({
			url: frm.attr("action"),
			type: "POST",
			data: JSON.stringify(comment),
			dataType: "json",
			contentType: "application/json; charset=utf-8",
			success: function (data) {
				if (data.indexOf('<') > -1) {
					frm.each(function () {
						$('ol.commentlist').append(data);
						bindA();
						this.reset();
					});
				} else
					alert(data);
			}
		});
	});

	bindA();
});

var btn;

function bindA() {
	$('a.del_comment').click(function (e) {
		btn = $(this);
		dlg.dialog('open');
		return false;
	});
}

function deleteComment() {
	$.ajax({
		url: btn.attr("href"),
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