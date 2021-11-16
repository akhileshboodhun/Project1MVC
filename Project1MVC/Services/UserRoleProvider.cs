using Project1MVC.DAL;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Project1MVC.Services
{
    public class UserRoleProvider : RoleProvider
    {
        public UserRoleProvider() { }

        public override string ApplicationName { get => throw new NotImplementedException(); set => throw new NotImplementedException(); }

        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override bool DeleteRole(string roleName, bool throwOnPopulatedRole)
        {
            throw new NotImplementedException();
        }

        public override string[] FindUsersInRole(string roleName, string usernameToMatch)
        {
            throw new NotImplementedException();
        }

        public override string[] GetAllRoles()
        {
            var userRoleDB = UserRoleDAL.Instance;
            var userRoles = userRoleDB.GetAll();
            string[] roleNames = userRoles.Select(el => el.RoleName).ToArray();
            return roleNames;
        }

        public override string[] GetRolesForUser(string username)
        {
            var userDB = UserDAL.Instance;
            var user = userDB.Get(username);
            List<string> roles = new List<string>() { };
            //roles.Add(user.RoleName);
            roles.Add("Admin");
            return roles.ToArray();

        }

        public override string[] GetUsersInRole(string roleName)
        {
            var userDB = UserDAL.Instance;
            var users = userDB.GetAll();
            var usersInRole = users.Where(el => el.RoleName == roleName);
            string[] emails = usersInRole.Select(el => el.Email).ToArray();
            return emails;
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            string[] roles = GetRolesForUser(username);
            return roles.Any(role => roleName.Equals(role, StringComparison.OrdinalIgnoreCase));
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            var userRoleDB = UserRoleDAL.Instance;
            var userRole = userRoleDB.GetRoleByName(roleName);
            return (userRole != null);
        }
    }
}