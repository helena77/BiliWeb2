using BiliWeb2.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace BiliWeb2.Backend.IdentityUser
{
    public class UserHelper
    {
        private static TechnicianModel CURRENT_USER = null;

        #region CurrentUser

        /// <summary>
        /// Set the current user as user
        /// </summary>
        /// <param name="user"></param>
        public static void SetCurrentUser(TechnicianModel user = null)
        {
            CURRENT_USER = user;
        }

        /// <summary>
        /// Get the current user
        /// </summary>
        /// <param name="user"></param>
        public static TechnicianModel GetCurrentUser()
        {
            return CURRENT_USER;
        }

        #endregion CurrentUser
    }
}
