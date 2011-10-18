using System.Data.Entity;

namespace megazlo.Models {
	public class ZloContext : DbContext {
		/// <summary>Категории(т.е. меню)</summary>
		public DbSet<Category> Categorys { get; set; }
		/// <summary>Список комментариев</summary>
		public DbSet<Comment> Comments { get; set; }
		/// <summary>Список сообщений</summary>
		public DbSet<Post> Posts { get; set; }
		/// <summary>Список пользователей</summary>
		public DbSet<User> Users { get; set; }

		protected override void OnModelCreating(DbModelBuilder modelBuilder) {
			base.OnModelCreating(modelBuilder);

			// место для вызовов Entity Framework Fluent API
			modelBuilder.Entity<Category>().HasMany(c => c.Posts).WithRequired().WillCascadeOnDelete();
		}
	}
}