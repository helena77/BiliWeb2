using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using BiliWeb2.Models;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;

/// <summary>
/// This controller to manage users. All the operationa such as creating, reading, updating and, deleting users 
/// are handled. Special features like Assigning roles to users are included. 
/// Only logged in users can access any of the feature present here.
/// Few operations are only for user with Super_User Role. 
/// we use UserManager, SignInManager from Identity framework to check for users, roles, login status, etc.
/// Check out Links below for various references in adding Identity to our project.
/// Pleae read <b>IdentityReadme.md</b> under project root folder.
/// </summary>
namespace BiliWeb2.Controllers
{
    public class UserController : BaseController
    {
        public UserController(
            Microsoft.AspNetCore.Http.IHttpContextAccessor httpContextAccessor,
            UserManager<TechnicianModel> userManager,
            SignInManager<TechnicianModel> signInManager
            ) : base
                (
                    httpContextAccessor,
                    userManager,
                    signInManager
                )
        {
        }
    }
}