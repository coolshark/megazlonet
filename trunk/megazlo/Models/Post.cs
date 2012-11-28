using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Web.Mvc;
using R = megazlo.Res.Model.Post;

namespace megazlo.Models {
	public class Post {
		public Post() {
			DatePost = DateTime.Now;
			WebLink = string.Empty;
			Tags = new List<Tag>();
		}
		[Key]
		public int Id { get; set; }
		[Required(ErrorMessageResourceName = "R_Title_E", ErrorMessageResourceType = typeof(R))]
		[Display(Name = "Title", ResourceType = typeof(R))]
		public string Title { get; set; }
		[Required(ErrorMessageResourceName = "R_Text_E", ErrorMessageResourceType = typeof(R))]
		[MaxLength]
		[AllowHtml]
		[DataType(DataType.MultilineText)]
		[Display(Name = "Text", ResourceType = typeof(R))]
		public string Text { get; set; }
		[NotMapped]
		public string TextPreview {
			get {
				if (string.IsNullOrEmpty(Text))
					return string.Empty;
				int from = Text.IndexOf("<p>");
				return Text.Substring(from, Text.IndexOf("</p>") - from + 4);
			}
		}
		[MaxLength(200)]
		public string WebLink { get; set; }
		[Display(Name = "DatePost", ResourceType = typeof(R))]
		public DateTime DatePost { get; set; }
		[Display(Name = "IsCommentable", ResourceType = typeof(R))]
		public bool IsCommentable { get; set; }
		public virtual ICollection<Comment> Comment { get; set; }
		public int UserId { get; set; }
		public virtual User User { get; set; }
		public int? CategoryId { get; set; }
		public virtual Category Category { get; set; }
		[Display(Name = "Cat", ResourceType = typeof(R))]
		[NotMapped]
		public SelectList Cat {
			get {
				using (ZloContext cn = new ZloContext())
					return new SelectList(cn.Categorys.ToList(), "Id", "Title");
			}
		}
		[Display(Name = "InCatMenu", ResourceType = typeof(R))]
		public bool InCatMenu { get; set; }
		[Display(Name = "IsShowInfo", ResourceType = typeof(R))]
		public bool IsShowInfo { get; set; }
		public virtual ICollection<Tag> Tags { get; set; }
		[NotMapped]
		public string TagList { get; set; }
	}
}