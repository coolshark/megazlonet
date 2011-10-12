using System.Linq;

namespace megazlo.Models
{
	public class DAO
	{
		public static void InsertUser(User user)
		{
			using (ZloContext cont = new ZloContext())
			{
				cont.Users.Add(user);
				cont.SaveChanges();
			}
		}

		public static bool AutoriseUser(User user)
		{
			bool rez = false;
			using (ZloContext cont = new ZloContext())
			{
				rez = cont.Users.Where(u => u.NickName == user.NickName).Where(u => u.PassWord == user.PassWord).Count() > 0;
			}
			return rez;
		}

		public static void EditUser(User user)
		{
			using (ZloContext cont = new ZloContext())
			{
				User usr2 = new User() { NickName = user.NickName };
				cont.Users.Attach(usr2);
				usr2.Avatar = user.Avatar;
				usr2.DateBorn = user.DateBorn;
				usr2.Email = user.Email;
				usr2.PassWord = user.PassWord;
				usr2.SecondName = user.SecondName;
				cont.SaveChanges();
			}
		}

		public static void DeleteUser(User user)
		{
			using (ZloContext cont = new ZloContext())
			{
				cont.Users.Attach(user);
				cont.Users.Remove(user);
				cont.SaveChanges();
			}
		}

		public static void InsertPost(Post post, User user)
		{
			using (ZloContext cont = new ZloContext())
			{
				User usr = cont.Users.Where(u => u.NickName == user.NickName).First();
				post.UserId = usr != null ? usr.Id : 0;
				cont.Posts.Add(post);
				cont.SaveChanges();
			}
		}

	}
}