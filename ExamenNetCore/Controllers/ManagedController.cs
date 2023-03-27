using ExamenNetCore.Repositories;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using ExamenNetCore.Models;

namespace ExamenNetCore.Controllers
{
    public class ManagedController : Controller
    {
        private RepositoryManaged repo;

        public ManagedController(RepositoryManaged repo)
        {
            this.repo = repo;
        }

        public IActionResult RegistroUsuario()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> RegistroUsuario
            (string nombre, string apellidos, string email, string password, string imagen)
        {
            await this.repo.RegisterUserAsync
                (nombre, apellidos, email, password, imagen);
            ViewData["MENSAJE"] = "Usuario registrado correctamente";
            return View();
        }

        public IActionResult Login()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Login(string email
            , string password)
        {
            Usuario usuario =
                await this.repo.ExisteUsuario(email, password);
            if (usuario != null)
            {
                ClaimsIdentity identity =
               new ClaimsIdentity
               (CookieAuthenticationDefaults.AuthenticationScheme
               , ClaimTypes.Name, ClaimTypes.Role);
                identity.AddClaim
                    (new Claim(ClaimTypes.Name, usuario.Email));
                identity.AddClaim
                    (new Claim(ClaimTypes.NameIdentifier, usuario.Password.ToString()));
                identity.AddClaim
                    (new Claim("Nombre", usuario.Nombre));
                identity.AddClaim
                    (new Claim("Apellidos", usuario.Apellidos));
                identity.AddClaim
                    (new Claim("Imagen", usuario.Imagen));

                ClaimsPrincipal user = new ClaimsPrincipal(identity);
                await HttpContext.SignInAsync
                    (CookieAuthenticationDefaults.AuthenticationScheme
                    , user);
                return RedirectToAction("Index", "Home");
            }
            else
            {
                ViewData["MENSAJE"] = "Usuario/Password incorrectos";
                return View();
            }
        }

        public async Task<IActionResult> LogOut()
        {
            await HttpContext.SignOutAsync
                (CookieAuthenticationDefaults.AuthenticationScheme);
            return RedirectToAction("Index", "Home");
        }

        public IActionResult ErrorAcceso()
        {
            return View();
        }
    }
}
