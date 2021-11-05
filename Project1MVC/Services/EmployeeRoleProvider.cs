using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;

namespace Project1MVC.Services
{
    public class EmployeeRoleProvider : RoleProvider
    {
        public EmployeeRoleProvider() { }

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
            throw new NotImplementedException();
        }

        public override string[] GetRolesForUser(string username)
        {
            var db = InMemoryEmployees.GetInstance();
            var employees = db.GetAll();
            var employee = employees.FirstOrDefault(el => el.Email == username);
            string role = "Admin"; // TODO: fix this later
            List<string> roles = new List<string>(){ };
            roles.Add(role);
            return roles.ToArray();
            
        }

        public override string[] GetUsersInRole(string roleName)
        {
            var db = InMemoryEmployees.GetInstance();
            var employees = db.GetAll();
            var employeesInRole = employees.Where(el => el.Role == roleName);
            string[] emails = employeesInRole.Select(el => el.Email).ToArray();
            return emails;
        }

        public override bool IsUserInRole(string username, string roleName)
        {
            string[] roles = GetRolesForUser(username);
            foreach (string role in roles)
            {
                if(roleName.Equals(role, StringComparison.OrdinalIgnoreCase))
                {
                    return true;
                }
            }
            return false;
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            var db = InMemoryRoles.GetInstance();
            var roles = db.GetAll();
            return roles.Any(el => el.Name == roleName);
        }
    }
}