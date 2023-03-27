using ExamenNetCore.Models;
using ExamenNetCore.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ExamenNetCore.Controllers
{
    public class ZapatillasController : Controller
    {
        private RepositoryZapas repo;

        public ZapatillasController(RepositoryZapas repo)
        {
            this.repo = repo;
        }

        public IActionResult DetallesZapatilla(int idzapatilla)
        {
            Zapatilla zapa = this.repo.FindZapatilla(idzapatilla);
            return View(zapa);
        }

        public async Task<IActionResult> _VistaImagenZapatillasPartial(int? posicion, int idzapatilla)
        {
            if (posicion == null)
            {
                posicion = 1;
            }
            int numregistros = this.repo.GetNumeroRegistrosVistaImagenZapatillas(idzapatilla);
            int siguiente = posicion.Value + 1;
            if (siguiente > numregistros)
            {
                siguiente = numregistros;
            }
            int anterior = posicion.Value - 1;
            if (anterior < 1)
            {
                anterior = 1;
            }
            VistaImagenZapatilla vistaImagenZapatillas =
                await this.repo.GetVistaImagenZapatillasAsync(posicion.Value, idzapatilla);
            ViewData["ULTIMO"] = numregistros;
            ViewData["SIGUIENTE"] = siguiente;
            ViewData["ANTERIOR"] = anterior;
            return PartialView("_VistaImagenZapatillasPartial", vistaImagenZapatillas);
        }
    }
}
