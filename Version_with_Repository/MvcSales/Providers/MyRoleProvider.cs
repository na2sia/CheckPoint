using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Security;
using System.Web.Helpers;
using System.Web.WebPages;
using Microsoft.Internal.Web.Utils;
using MvcSales.Models;
using MvcSales.Repository;

namespace MvcSales.Providers
{
    public class MyRoleProvider : RoleProvider
    {
        
        public override string[] GetRolesForUser(string login)
        {
            string[] role = new string[] { };
            using (IModelRepository<User> _user = new UserRepository())
            {
                try
                {
                    IModelRepository<Role> _role = new RoleRepository();
                    // Get User
                    var user = (from u in _user.Items
                                 where u.Login == login
                                 select u).FirstOrDefault();
                    if (user != null)
                    {
                        // Get role
                        var userRole = _role.Items.FirstOrDefault(x=>x.Id==user.RoleId);

                        if (userRole != null)
                        {
                            role = new string[] { userRole.Name };
                        }
                    }
                }
                catch
                {
                    role = new string[] { };
                }
            }
            return role;
        }
        public override bool IsUserInRole(string username, string roleName)
        {
            bool outputResult = false;
            // Find user
            using (IModelRepository<User> _user = new UserRepository())
            {
                try
                {
                    IModelRepository<Role> _role = new RoleRepository();
                    // get user
                    var user = (from u in _user.Items
                                 where u.Login == username
                                 select u).FirstOrDefault();
                    if (user != null)
                    {
                        // get role
                        var userRole = _role.Items.FirstOrDefault(x=>x.Id==user.RoleId);

                        //compare
                        if (userRole != null && userRole.Name == roleName)
                        {
                            outputResult = true;
                        }
                    }
                }
                catch
                {
                    outputResult = false;
                }
            }
            return outputResult;
        }
        public override void AddUsersToRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }
        public override void CreateRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override string ApplicationName
        {
            get
            {
                throw new NotImplementedException();
            }
            set
            {
                throw new NotImplementedException();
            }
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

        public override string[] GetUsersInRole(string roleName)
        {
            throw new NotImplementedException();
        }

        public override void RemoveUsersFromRoles(string[] usernames, string[] roleNames)
        {
            throw new NotImplementedException();
        }

        public override bool RoleExists(string roleName)
        {
            throw new NotImplementedException();
        }
    }
}