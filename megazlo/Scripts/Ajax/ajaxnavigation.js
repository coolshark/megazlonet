/// <reference path="../jquery-1.6.4.min.js" />
/// <reference path="../jquery-ui-1.8.16.custom.min.js" />

$(function () {
	function initDoc() {
		$('.ajax').click(function (e) {
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
		vtip.reinit();
	}

	function ajaxComplete(data) {
		$("#primary").empty();
		if (data != null)
			$("#primary").append(data);
		var url = $('#pageInfo').attr('value').replace("&", "&");
		window.history.pushState("ajax", document.title, url);
		document.title = $("#pageInfo").attr('title');
		initDoc();
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
		initDoc();
	});

})

