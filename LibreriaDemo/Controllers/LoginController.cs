using LibreriaDemo.Helpers;
using LibreriaDemo.Models;
using LibreriaDemo.Models.Entidades;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace LibreriaDemo.Controllers
{
    public class LoginController : Controller
    {

        private readonly IServicioUsuario _servicioUsuario;
        private readonly IServicioImagen _servicioImagen;
        private readonly LibreriaContext _context;

        public LoginController(IServicioUsuario servicioUsuario, IServicioImagen servicioImagen, LibreriaContext context)
        {
            _servicioUsuario = servicioUsuario;
            _servicioImagen = servicioImagen;
            _context = context;
        }

        public IActionResult Registro()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Registro(Usuario usuario, IFormFile Imagen)
        {
            Stream image = Imagen.OpenReadStream();
            string urlImagen = await _servicioImagen.SubirImagen(image, Imagen.FileName);

            usuario.Clave = Utilidades.EncriptarClave(usuario.Clave);
            usuario.URLFotoPerfil = urlImagen;

            Usuario usuarioCreado = await _servicioUsuario.SaveUsuario(usuario);

            if (usuarioCreado.Id > 0)
            {
                return RedirectToAction("IniciarSesion", "Login");
            }

            ViewData["Mensaje"] = "No se pudo crear el usuario";
            return View();
        }

        public IActionResult IniciarSesion()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> IniciarSesion(string correo, string clave)
        {
            Usuario usuarioEncontrado = await _servicioUsuario.GetUsuario(correo, Utilidades.EncriptarClave(clave));

            if (usuarioEncontrado == null)
            {
                ViewData["Mensaje"] = "Usuaio no encontrado";
                return View();
            }

            List<Claim> claims = new List<Claim>()
            {
                new Claim(ClaimTypes.Name, usuarioEncontrado.NombreUsuario),
                new Claim("FotoPerfil", usuarioEncontrado.URLFotoPerfil),
            };

            ClaimsIdentity claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
            AuthenticationProperties properties = new AuthenticationProperties()
            {
                AllowRefresh = true,
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                properties
                );

            return RedirectToAction("Index", "Home");

        }
    }
}
