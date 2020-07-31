using System.Diagnostics;
using System.Collections.Generic;
using BiliWeb2.Models;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using BiliWeb2.Models.ViewModels;
using BiliWeb2.Backend.IdentityUser;
using System.Threading.Tasks;

namespace BiliWeb2.Controllers
{
    /// <summary>
    /// Base Controller
    /// This controller supports 
    ///     Langauge Switching
    ///     User Manager for Identity
    /// </summary>
    public class BaseController : Controller
    {  
        // Hold the local current user
        public TechnicianModel _currentUser;

        // Hold the local Role
        public IList<string> _roles;

        // The User Manager
        public UserManager<TechnicianModel> _userManager;

        // The Signin manger
        public SignInManager<TechnicianModel> _signInManager;

        // The context of the request
        public HttpContext _context;

        // The Session Key for Language
        public const string SessionKeyLanguage = "_language";

        // Allowed List of Languages
        public readonly List<string> LanguageList = new List<string> { "zh", "vi", "sw", "ru", "id", "fr", "ar", "en" };

        /// <summary>
        /// Base Constructor takes the context and the identity
        /// </summary>
        /// <param name="httpContextAccessor"></param>
        /// <param name="userManager"></param>
        /// <param name="signInManager"></param>
        public BaseController(
            IHttpContextAccessor httpContextAccessor,
            UserManager<TechnicianModel> userManager,
            SignInManager<TechnicianModel> signInManager)
        {
            _context = httpContextAccessor.HttpContext;

            SystemGlobalsModel.SetHostPath(_context.Request.Host.ToString());

            _userManager = userManager;

            _signInManager = signInManager;

            _currentUser = GetCurrentUser();

            _roles = GetCurrentUserRoles();
        }

        #region CurrentUser

        /// <summary>
        /// Set the current user as user
        /// </summary>
        /// <param name="user"></param>
        public bool SetCurrentUser(TechnicianModel user = null)
        {
            _currentUser = user;

            return true;
        }

        /// <summary>
        /// Get the current user
        /// </summary>
        /// <param name="user"></param>
        public TechnicianModel GetCurrentUser()
        {
            // Use the current one if set
            if (_currentUser != null)
            {
                return _currentUser;
            }

            // TODO:  Mike Need to work out the below code, as the UserHelper stops being static.  How will this work?

            // set logged in user in local cache only if local cache is null but user is authenticated.
            var helperUser = UserHelper.GetCurrentUser();
            if (helperUser != null)
            {
                _currentUser = helperUser;
                return _currentUser;
            }

            if (User == null)
            {
                _currentUser = helperUser;
                return _currentUser;
            }

            // Reached when running UTs, but not when running code coverage
            if (!User.Identity.IsAuthenticated)
            {
                _currentUser = helperUser;
                return _currentUser;
            }

            // Got to here, try setting again.
            UserHelper.SetCurrentUser(_userManager.GetUserAsync(User).Result);
            _currentUser = UserHelper.GetCurrentUser();
            return _currentUser;
        }

        /// <summary>
        /// Get Current User Roles
        /// </summary>
        public IList<string> GetCurrentUserRoles()
        {
            var user = GetCurrentUser();
            if (user == null)
            {
                return new List<string>(0);
            }

            return _userManager.GetRolesAsync(user).Result;
        }

        /// <summary>
        /// Return the user language from their cookie
        /// </summary>
        /// <returns></returns>
        public string GetUserLanguage()
        {
            // Line 140 in HttpContextHelper.cs always returns "System.Byte[]"
            // Better if returned "en", "fr", or "bogus"
            var language = _context.Session.GetString(SessionKeyLanguage);
            if (string.IsNullOrEmpty(language))
            {
                language = "en";
                _context.Session.SetString(SessionKeyLanguage, language);

            }

            return language;
        }

        #endregion CurrentUser

        #region Language

        /// <summary>
        /// language pages
        /// </summary>
        /// <returns></returns>
        public IActionResult Language(string id)
        {
            if (string.IsNullOrEmpty(id))
            {
                return RedirectToAction("Index");
            }

            id = id.ToLower();

            // Check the langugae list, only allowed languages
            if (!LanguageList.Contains(id))
            {
                // Not in the list, assume English
                id = "en";
            }

            _context.Session.SetString(SessionKeyLanguage, id);

            var jump = _context.Request.Headers["Referer"].ToString();

            if (string.IsNullOrEmpty(jump))
            {
                // UTs not reaching this line. Need to give jump a value, if possible..
                return RedirectToAction("Index", "Home");
            }

            return Redirect(jump);
        }

        #endregion Language

        /// <summary>
        /// Error Page
        /// </summary>
        /// <returns></returns>
        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public virtual IActionResult Error()
        {
            // In UTs use var client = _factory to populate TraceIdentifier
            // TODO Set/reach Activity.Current.Id to test first ?? condition
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? _context.TraceIdentifier });
        }
    }
}