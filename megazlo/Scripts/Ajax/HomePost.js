/// <reference path="../jquery-1.7.min.js" />
/// <reference path="../jquery-ui-1.8.16.custom.min.js" />
/// <reference path="../jquery.validate.min.js" />
/// <reference path="../jquery.validate.unobtrusive.min.js" />

$(function () {
	$(document).on('click', 'a.del_post', function () {
		showDelDial($(this));
	});

	function showDelDial(pst) {
		var dlg = $("#notedial");
		dlg.dialog();
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
			data: JSON.stringify(post),
			dataType: "json",
			contentType: "application/json; charset=utf-8",
			success: showCompleteDialog
		});
	}

	function showCompleteDialog(data) {
		var arr = data.split(';');
		var msg = arr[0];
		$('post_hentry-' + arr[1]).remove();
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

});