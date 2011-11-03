/** Vertigo Tip Requires jQuery */
this.vtip = function () {
	this.yOffset = this.xOffset = 10;



	$('body').append('<p id="vtip"><input id="vp_in" type=text style="width: 100%;" /></p>');

	$("input#vp_in").bind('click', function (e) {
		this.select();
	});

	$("input#vp_in").bind('blur', function (e) {
		$("p#vtip").hide();
	});

};

vtip.reinit = function (n) {
	$("a.vtip").bind('click', function (e) {
		$("input#vp_in").attr('value', this.href);
		this.top = (e.pageY + yOffset);
		this.left = (e.pageX + xOffset);
		$('p#vtip').text(this.t);
		$('p#vtip').css("width", 400 + "px");
		$('p#vtip').css("top", this.top + "px").css("left", this.left + "px").show();
		$("input#vp_in").focus();
		$("input#vp_in").select();
		return false;
	});
};
jQuery(document).ready(function ($) { vtip(); vtip.reinit(); });