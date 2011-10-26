/// <reference path="../jquery-1.6.4.min.js" />
/// <reference path="../jquery.validate.min.js" />
/// <reference path="../jquery.validate.unobtrusive.min.js" />
/// <reference path="../jquery-ui-1.8.16.custom.min.js" />

$(function () {
	var dlg = $('#dialogResult');
	dlg.dialog({
		autoOpen: false,
		resizable: false,
		buttons: {
			"OK": function () {
				dlg.dialog("close");
			}
		}
	});

	$('form:first').submit(function (e) {
		if (!$(this).valid())
			return false;
		e.preventDefault();
		var restorePass = {
			Name: $('#Name').val()
		};
		$.ajax({
			url: $(this).attr("action"),
			type: "POST",
			data: JSON.stringify(restorePass),
			dataType: "json",
			contentType: "application/json; charset=utf-8",
			success: function (data) {
				$('#restoreRez').html(data);
				dlg.dialog('open');
			}
		});
	});
});