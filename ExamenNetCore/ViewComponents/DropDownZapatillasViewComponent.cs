using ExamenNetCore.Models;
using ExamenNetCore.Repositories;
using Microsoft.AspNetCore.Mvc;

namespace ExamenNetCore.ViewComponents
{
    public class DropDownZapatillasViewComponent: ViewComponent
    {
        private RepositoryZapas repo;

        public DropDownZapatillasViewComponent(RepositoryZapas repo)
        {
            this.repo = repo;
        }

        public async Task<IViewComponentResult> InvokeAsync()
        {
            List<Zapatilla> zapas = this.repo.GetZapatillas();
            return View(zapas);
        }
    }
}
