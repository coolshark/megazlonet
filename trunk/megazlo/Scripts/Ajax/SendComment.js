/// <reference path="../jquery-1.6.4.min.js" />
/// <reference path="../jquery.validate.min.js" />
/// <reference path="../jquery.validate.unobtrusive.min.js" />

$(function () {
	$("form:first").submit(function (e) {
		//$.validator.unobtrusive.parse(this)
		if (!$(this).valid())
			return false;
		e.preventDefault();
		$("form:first").validate().form();
		var comment = {
			FirstName: $("#NewComment_FirstName").val(),
			Email: $("#NewComment_Email").val(),
			Text: $("#NewComment_Text").val(),
			PostId: $("#NewComment_PostID").val()
		};

		$.ajax({
			url: $(this).attr("action"),
			type: "POST",
			data: JSON.stringify(comment),
			dataType: "json",
			contentType: "application/json; charset=utf-8",
			success: function (data) {
				if (data == true) {
					$('form:first').each(function () {
						insertComment(comment);
						this.reset();
					});
				} else
					alert(data);
			}
		});
	});
});

function insertComment(cm) {
	$('ol.commentlist').append(
	'<li id="li-comment-@item.Id" class="comment even thread-even depth-1"><div id="comment-@item.Id"><div class="line"></div>' +
	'<img class="avatar avatar-35 photo" width="35" height="35" src="/Content/styles/images/usr_logo.jpg" alt="" />' +
	'<div class="comment-author vcard"><cite class="fn"><a class="url" rel="external nofollow" ' +
	'href="@Url.Action("Post", new { id = Model.WebLink })/#comment-@item.Id">' + cm.FirstName + '</a></cite><span class="says">says:</span>' +
	'</div><div class="comment-meta commentmetadata"><a href="@Url.Action("Post", new { id = Model.WebLink })/#comment-@item.Id">' + new Date().toString() +
	'</a></div><div class="comment-body">' + cm.Text + '</div><br /></div></li>');;
}