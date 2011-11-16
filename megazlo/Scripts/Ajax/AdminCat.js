/// <reference path="../jquery-1.7.min.js" />
/// <reference path="../jquery-ui-1.8.16.custom.min.js" />
/// <reference path="../jquery.validate.min.js" />
/// <reference path="../jquery.validate.unobtrusive.min.js" />

$(function () {
	var type;
	$('a.edit_cat').on('click', function (e) {
		e.preventDefault();
		var itm = $(this).parent('td');
		var cat = {
			Id: $('#Id', itm).val(),
			Title: $('#Title', itm).val(),
			Por: $('#Por', itm).val()
		};
		$.ajax({
			url: $(this).attr('href'),
			type: "POST",
			data: JSON.stringify(cat),
			dataType: "json",
			contentType: "application/json; charset=utf-8",
			success: showCatDial
		});
	});

	function showCatDial(data) {
		var dlg = $("#new-cat-dail");
		dlg.html(data);
		dlg.dialog({
			resizable: false,
			modal: true,
			position: 'center',
			draggable: false,
			buttons: {
				'OK': function () {
					var frm = $(this).find('form:first');
					$.validator.unobtrusive.parse(frm);
					if (frm.valid()) {
						$(this).dialog('close');
						sendCat($(this));
					}
				},
				Cancel: function () {
					$(this).dialog('close');
				}
			}
		});
		dlg.dialog('open');
	}

	function sendCat(dlg) {
		var cat = {
			Id: $('#Id', dlg).val(),
			Title: $('#Title', dlg).val(),
			Por: $('#Por', dlg).val()
		};
		$.ajax({
			url: $('form:first', dlg).attr('action'),
			type: "POST",
			data: JSON.stringify(cat),
			dataType: "json",
			contentType: "application/json; charset=utf-8",
			success: function (data) {
				if (data.indexOf('<') > -1) {
					$('#tab-cat').append(data);
				} else
					location.reload(true);
			}
		});
	}

});