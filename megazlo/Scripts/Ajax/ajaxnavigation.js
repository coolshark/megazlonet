/// <reference path="../jquery-1.7.min.js" />
/// <reference path="../jquery-ui-1.8.16.custom.min.js" />

$(function () {
	var labl = '';
	var lin = '';
	var prim = $("#primary");
	$(document).on('click', 'a.ajax', function (e) {
		labl = $(this).attr('href');
		setlin(labl);
		$.ajax({
			url: $(this).attr('href'),
			type: "POST",
			data: JSON.stringify(""),
			dataType: "json",
			contentType: "application/json; charset=utf-8",
			success: function (data) {
				ajaxComplete(data);
			}
		});
		return false;
	});

	function setlin(tm) {
		if (tm.lastIndexOf("#") > 0)
			lin = tm.substring(tm.lastIndexOf("#"), tm.length);
		else
			lin = '';
	}

	function ajaxComplete(data) {
		if (data != null)
			prim.html(data);
		var url = $('#pageInfo').attr('value').replace("&", "&");
		window.history.pushState("ajax", document.title, url);
		if (lin.length > 0)
			$('html, body').scrollTop($(lin).offset().top);
		$('#qrcode').attr('src', '/Help/QR?data=' + document.location);
		document.title = $("#pageInfo").attr('title');
	}

	// Метод выполняется при загрузке документа
	$(document).ready(function () {
		// Подписываемся на навигацию браузера по страницам
		window.onpopstate = function (event) {
			if (event.state == "ajax")
				window.location.reload();
			else
				window.history.replaceState("ajax", document.title, window.location.href);
			event.preventDefault();
		};
		// Устанавливаем новый заголовок страницы
		document.title = $("#pageInfo").attr('title');
	});

})