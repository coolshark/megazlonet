/// <reference path="../jquery-1.7.min.js" />
/// <reference path="../jquery-ui-1.8.16.custom.min.js" />
/// <reference path="../jquery.validate.min.js" />
/// <reference path="../jquery.validate.unobtrusive.min.js" />

$(function () {

	$('#login_link').on('click', function (e) {
		$.ajax({
			url: $(this).attr('href'),
			type: 'POST',
			dataType: 'json',
			data: null,
			success: function (data) {
				$('#login_dial').html(data);
				showLoginDial();
			}
		});
		return false;
	});

	function showLoginDial() {
		var dlg = $("#login_dial");
		dlg.dialog({
			resizable: false,
			height: 215,
			width: 400,
			modal: true,
			position: 'center',
			draggable: false,
			buttons: {
				" OK ": function () {
					var frm = $(this).find('form:first');
					$.validator.unobtrusive.parse(frm);
					if (frm.valid()) {
						$(this).dialog("close");
						sendLogin($(this));
					}
				},
				Cancel: function () {
					$(this).dialog("close");
				}
			}
		});
		dlg.dialog("open");
	}

	function sendLogin(dlg) {
		var login = {
			Login: $("#Login", dlg).val(),
			Password: $("#Password", dlg).val(),
			IsRemember: $("#IsRemember", dlg).attr('checked') == 'checked'
		};
		$.ajax({
			url: $('#login_href', dlg).val(),
			type: "POST",
			data: JSON.stringify(login),
			dataType: "json",
			contentType: "application/json; charset=utf-8",
			success: function (data) {
				if (data == true)
					location.reload(true);
			}
		});
	}

});
