using Firebase.Auth;
using LibreriaDemo.Models.Entidades;
using Microsoft.EntityFrameworkCore;

namespace LibreriaDemo.Helpers
{
    public interface IServicioUsuario
    {
        Task<Usuario> GetUsuario(string correo, string clave);
        Task<Usuario> SaveUsuario(Usuario usuario);
        Task<Usuario> GetUsuario(string nombreUsuario);
        
    }
}
