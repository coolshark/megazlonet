/// <reference path="../jquery-1.6.4.min.js" />
/// <reference path="../jquery-ui-1.8.16.custom.min.js" />
var arr;
$(function () {

	$.ajax({
		url: "/Admin/LoadTags",
		type: "POST",
		dataType: "json",
		data: JSON.stringify(""),
		success: function (data) {
			arr = data;
			$("input.fortags").autocomplete({
				delay: 0,
				minLength: 0,
				source: arr,
				autoFill: true
			});
		}
	});

	$("input.fortags").focus(function (e) {
		$(this).autocomplete('search', '');
	});

});
