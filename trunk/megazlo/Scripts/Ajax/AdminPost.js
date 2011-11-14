/// <reference path="../jquery-1.7.min.js" />
/// <reference path="../jquery-ui-1.8.16.custom.min.js" />
/// <reference path="ajaxnavigation.js" />

var arr;
$(function () {

	var contr = $('#tag-container');
	var fort;
	//fort.attr('name', 'Tags_Title');
	$.ajax({
		url: "/Admin/LoadTags",
		type: "POST",
		dataType: "json",
		data: JSON.stringify(""),
		success: function (data) {
			arr = data;
			bind();
		}
	});

	function bind() {
		fort = $("input.fortags");
		fort.autocomplete({
			delay: 0,
			minLength: 0,
			source: arr,
			autoFill: true
		});

		fort.focus(function (e) {
			$(this).autocomplete('search', '');
		});

		fort.bind("autocompleteclose", function (e) {
			if (fort.size() < 4) {
				if ($(this).val().length > 0) {
					contr.append('<input type="text" class="fortags" />');
					bind();
				}
			} else
				fort = $("input.fortags");
			var emp = false;
			for (var i = 0; i < fort.size(); i++) {
				if (fort.eq(i).val().length == 0) {
					if (emp)
						fort.eq(i).remove();
					else
						emp = true;
				}
			}
		});
	}

	$('#sbmtform').click(function (e) {
		e.preventDefault();
		$.validator.unobtrusive.parse('form:first');
		if (!$('form:first').valid())
			return false;
		var post = {
			Id: $("#Id").val(),
			Title: $("#Title").val(),
			CategoryId: $("#CategoryId").val(),
			InCatMenu: $("#InCatMenu").val(),
			Text: $("#Text").val(),
			IsCommentable: $("#IsCommentable").val(),
			IsShowInfo: $("#IsShowInfo").val()
		}
		$.ajax({
			url: $('form:first').attr('action'),
			type: "POST",
			data: JSON.stringify(post),
			dataType: "json",
			contentType: "application/json; charset=utf-8",
			success: showSaveDialog
		});
		return false;
	});

	function showSaveDialog(data) {
		var arr = data.split(';');
		var msg = arr[0];
		$("#Id").val(arr[1]);
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
