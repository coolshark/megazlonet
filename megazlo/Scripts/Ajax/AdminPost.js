/// <reference path="../jquery-1.6.4.min.js" />
/// <reference path="../jquery-ui-1.8.16.custom.min.js" />
var arr;
$(function () {
	var fort = $("input.fortags");
	$.ajax({
		url: "/Admin/LoadTags",
		type: "POST",
		dataType: "json",
		data: JSON.stringify(""),
		success: function (data) {
			arr = data;
			fort.autocomplete({
				delay: 0,
				minLength: 0,
				source: arr,
				autoFill: true
			});
		}
	});

	fort.focus(function (e) {
		$(this).autocomplete('search', '');
	});

});
