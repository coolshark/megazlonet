/// <reference path="../jquery-1.6.4.min.js" />
/// <reference path="../jquery.validate.min.js" />
/// <reference path="../jquery.validate.unobtrusive.min.js" />

$(function () {
	$("#confirmDialog").dialog({
		autoOpen: false,
		resizable: false,
		height: 180,
		width: 400,
		buttons: {
			"OK": function () {
				$(this).dialog("close");
				deleteComment();
			},
			Cancel: function () {
				$(this).dialog("close");
			}
		}
	});

	$("form:first").submit(function (e) {
		//$.validator.unobtrusive.parse(this)
		if (!$(this).valid())
			return false;
		e.preventDefault();
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
				if (data.indexOf('<') > -1) {
					$('form:first').each(function () {
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
		$("#confirmDialog").dialog('open');
		return false;
	});
}

function deleteComment() {
	$.ajax({
		url: btn.attr("href"),
		type: "POST",
		//data: JSON.stringify(comment),
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