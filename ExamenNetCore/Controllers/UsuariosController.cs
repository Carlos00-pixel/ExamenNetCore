using ExamenNetCore.Filters;
using ExamenNetCore.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ExamenNetCore.Controllers
{
    public class UsuariosController : Controller
    {
        private RepositoryManaged repo;

        public UsuariosController(RepositoryManaged repo)
        {
            this.repo = repo;
        }

        [AuthorizeUsuarios]
        public async Task<IActionResult> PerfilUsuario()
        {
            return View();
        }
    }
}
