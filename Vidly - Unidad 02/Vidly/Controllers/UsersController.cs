using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication.Facebook;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Vidly.DAL;
using Vidly.Models;
using Vidly.ViewModels;

namespace Vidly.Controllers
{
    public class UsersController : Controller
    {
        private readonly MyDbContext _context;

        public UsersController(MyDbContext context)
        {
            _context = context;
        }

        // GET: Users       
        [Authorize(Roles = RoleName.CanManageMovies)]
        public async Task<IActionResult> Index()
        {
            var myDbContext = _context.Users.Include(u => u.Role);
            return View(await myDbContext.ToListAsync());
        }

        // GET: Users/Details/5
        [Authorize(Roles = RoleName.CanManageMovies)]
        public async Task<IActionResult> Details(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // GET: Users/Create
        public IActionResult Create()
        {
            var roles = _context.Roles.ToList();
            UserFormViewModel uservm = new UserFormViewModel();
            uservm.Roles = roles;
            return View("UserForm", uservm);
        }

        // POST: Users/Create        
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create(User user)
        {
            if (!ModelState.IsValid)
            {
                var vm = new UserFormViewModel
                {
                    User = user,
                    Roles = _context.Roles.ToList()
                };
                return View("UserForm", vm);
            }
            else
            {
                user.RoleId = 2;
                _context.Add(user);
                await _context.SaveChangesAsync();
                return RedirectToAction(nameof(Index));
            }
        }

        // GET: Users/Edit/5
        [Authorize(Roles = RoleName.CanManageMovies)]
        public async Task<IActionResult> Edit(int id)
        {
            var user = await _context.Users.FindAsync(id);
            if (user == null)
            {
                return NotFound();
            }

            var vm = new UserFormViewModel
            {
                User = user,
                Roles = _context.Roles.ToList()
            };
            return View(vm);
        }

        // POST: Users/Edit/5        
        [HttpPost]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public async Task<IActionResult> Edit(int id, [Bind("Id,Name,PhoneNumber,DriverLicense,Email,Password,RoleId")] User user)
        {
            if (id != user.Id)
            {
                return NotFound();
            }

            if (ModelState.IsValid)
            {
                try
                {
                    _context.Update(user);
                    await _context.SaveChangesAsync();
                }
                catch (DbUpdateConcurrencyException)
                {
                    if (!UserExists(user.Id))
                    {
                        return NotFound();
                    }
                    else
                    {
                        throw;
                    }
                }
                return RedirectToAction(nameof(Index));
            }
            ViewData["RoleId"] = new SelectList(_context.Roles, "Id", "Id", user.RoleId);
            return View(user);
        }

        // GET: Users/Delete/5
        [Authorize(Roles = RoleName.CanManageMovies)]
        public async Task<IActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Id == id);
            if (user == null)
            {
                return NotFound();
            }

            return View(user);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        [Authorize(Roles = RoleName.CanManageMovies)]
        public async Task<IActionResult> DeleteConfirmed(int id)
        {
            var user = await _context.Users.FindAsync(id);
            _context.Users.Remove(user);
            await _context.SaveChangesAsync();
            return RedirectToAction(nameof(Index));
        }

        private bool UserExists(int id)
        {
            return _context.Users.Any(e => e.Id == id);
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email, string password)
        {
            var user = await _context.Users
                .Include(u => u.Role)
                .FirstOrDefaultAsync(m => m.Email == email && m.Password == password);

            if (user != null)
            {
                string rol = user.Role.Name;
                var claims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.Name),
                    new Claim("Email", user.Email),
                    new Claim("Id", user.Id.ToString()),
                    new Claim(ClaimTypes.Role, rol)
                };

                var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);

                await HttpContext.SignInAsync(CookieAuthenticationDefaults.AuthenticationScheme, new ClaimsPrincipal(claimsIdentity));

                return RedirectToAction("List", "Movies");
            }
            ViewBag.Msg = "Wrong email or password";
            return View();
        }

        [Route("users/facebook-login")]
        public IActionResult FacebookLogin()
        {
            var properties = new AuthenticationProperties
            {
                RedirectUri = Url.Action("FacebookResponse")
            };

            return Challenge(properties, FacebookDefaults.AuthenticationScheme);
        }

        [Route("users/facebook-response")]
        public async Task<IActionResult> FacebookResponse()
        {
            var result = await HttpContext.AuthenticateAsync(CookieAuthenticationDefaults.AuthenticationScheme);

            var claims = result.Principal.Identities
                .FirstOrDefault().Claims.Select(claim => new
                {
                    claim.Issuer,
                    claim.OriginalIssuer,
                    claim.Type,
                    claim.Value
                });
            
            return Json(claims);
        }

        public async Task<IActionResult> Logout()
        {
            await HttpContext.SignOutAsync(CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction(nameof(Login));
        }

        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}
