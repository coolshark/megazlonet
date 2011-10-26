/// <reference path="../jquery-1.6.4.min.js" />
/// <reference path="../jquery-ui-1.8.16.custom.min.js" />
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
		var tgs = $('input.fortags');
		var strtag = '';
		for (var i = 0; i < tgs.size(); i++)
			strtag += tgs.eq(i).val() + ';';
		strtag = strtag.substring(0, strtag.length - 1);
		$('#TagList').val(strtag);
	});

});
