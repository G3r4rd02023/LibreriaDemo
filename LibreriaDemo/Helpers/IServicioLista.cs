using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibreriaDemo.Helpers
{
    public interface IServicioLista
    {
        Task<IEnumerable<SelectListItem>> GetListaAutores();
        Task<IEnumerable<SelectListItem>> GetListaCategorias();

    }
}

