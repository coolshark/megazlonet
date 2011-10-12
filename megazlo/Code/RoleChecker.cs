using System;
using System.Collections.Generic;
using System.Web.Security;

namespace megazlo.Code {
	public class RoleChecker : RoleProvider {
		private static List<string> roles = new List<string>();
		private static Dictionary<string, List<string>> userRoles = new Dictionary<string, List<string>>();

		public override void AddUsersToRoles(string[] usernames, string[] roleNames) {
			for (int i = 0; i < usernames.Length; i++)
				if (!userRoles.ContainsKey(usernames[i]))
					userRoles.Add(usernames[i], new List<string>(roleNames));
		}

		public override string ApplicationName { get; set; }

		public override void CreateRole(string roleName) {
			if (!roles.Contains(roleName))
				roles.Add(roleName);
		}

		public override bool DeleteRole(string roleName, bool throwOnPopulatedRole) {
			return roles.Remove(roleName);
		}

		public override string[] FindUsersInRole(string roleName, string usernameToMatch) {
			throw new NotImplementedException();
		}

		public override string[] GetAllRoles() {
			return roles.ToArray();
		}

		public override string[] GetRolesForUser(string username) {
			if (userRoles.ContainsKey(username))
				return userRoles[username].ToArray();
			return new string[0];
		}

		public override string[] GetUsersInRole(string roleName) {
			List<string> rez = new List<string>();
			foreach (string key in userRoles.Keys)
				if (userRoles[key].Contains(roleName))
					rez.Add(key);
			return rez.ToArray();
		}

		public override bool IsUserInRole(string username, string roleName) {
			if (userRoles.ContainsKey(username))
				return userRoles[username].Contains(roleName);
			return false;
		}

		public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames) {
			throw new NotImplementedException();
		}

		public override bool RoleExists(string roleName) {
			return roles.Contains(roleName);
		}
	}
}