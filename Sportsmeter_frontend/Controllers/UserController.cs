using CRUD_Design;
using CRUD_Design_Contracts;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Sportsmeter_frontend.Model.Services;
using Sportsmeter_frontend.Models;

namespace Sportsmeter_frontend.Controllers
{
    public class UserController: Controller
    {
        private readonly IAuthManager _authManager;
        private readonly DataContext _context;
        private readonly UserManager<ApplicationUser> _userManager;

        public UserController(IAuthManager authManager, DataContext context, UserManager<ApplicationUser> userManager)
        {
            _authManager = authManager;
            _context = context;

            _userManager = userManager;
        }
      
        public IActionResult LoginView()
        {
            return View("Login");
        }

        public  IActionResult Index()
        {
            ViewBag.Users = _context.Users.Select(u => new
            {
                FirstName = u.FirstName,
                LastName = u.LastName,
                Email = u.Email,
                Role = (from r in _context.Roles join r2 in _context.UserRoles on r.Id equals r2.RoleId
                        where r2.UserId == u.Id
                        select r.Name).First()}
            );

            return View();
        }

        public IActionResult Register()
        {
            ViewBag.Roles = _context.Roles.Select(r => r.NormalizedName).AsEnumerable();
            return View();
        }

        [HttpPost]
        public async Task<ActionResult> Register([Bind("Email,Password,FirstName,LastName,Role")] ApiUserDto apiUserDto)
        {
            var errors = await _authManager.Register(apiUserDto);
            if (errors.Any())
            {
                foreach (var error in errors)
                {
                    ModelState.AddModelError(error.Code, error.Description);
                }
                return BadRequest(ModelState);
            }
            return Ok("Success");
        }

        [HttpPost]
        public async Task<ActionResult> Login([Bind("Email,Password")] LoginDto loginDto)
        {
            AuthResponseDto authResponse = await _authManager.Login(loginDto);

            if (authResponse == null)
                return Unauthorized();

            Response.Cookies.Append("X-Access-Token", authResponse.Token, new CookieOptions() { Expires = DateTimeOffset.Now.AddDays(7), Path ="/" });

            return RedirectToAction("Index", "Home");
        }

        public async Task<ActionResult> Logout()
        {
            Response.Cookies.Delete("X-Access-Token");
            return RedirectToAction("Index", "Home");
        }
    }
}
