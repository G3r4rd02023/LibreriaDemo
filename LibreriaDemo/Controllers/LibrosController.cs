using LibreriaDemo.Helpers;
using LibreriaDemo.Models;
using LibreriaDemo.Models.Entidades;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace LibreriaDemo.Controllers
{
    public class LibrosController : Controller
    {

        private readonly LibreriaContext _context;
        private readonly IServicioLista _servicioLista;
        private readonly IServicioImagen _servicioImagen;

        public LibrosController(LibreriaContext context, IServicioLista servicioLista, IServicioImagen servicioImagen)
        {
            _context = context;
            _servicioLista = servicioLista;
            _servicioImagen = servicioImagen;
        }
        public async Task<IActionResult> Lista()
        {
            return View(await _context.Libros
                .Include(l => l.Categoria)
                .Include(l => l.Autor)
                .ToListAsync());
        }

        public async Task<IActionResult> Crear()
        {
            Libro libro = new()
            {
                Categorias = await _servicioLista.GetListaCategorias(),
                Autores = await _servicioLista.GetListaAutores(),
            };

            return View(libro);
        }


        [HttpPost]
        public async Task<IActionResult> Crear(Libro libro, IFormFile Imagen)
        {
            if (ModelState.IsValid)
            {

                Stream image = Imagen.OpenReadStream();
                string urlimagen = await _servicioImagen.SubirImagen(image, Imagen.FileName);

                try
                {

                    libro.URLImagen = urlimagen;

                    _context.Add(libro);
                    await _context.SaveChangesAsync();
                    return RedirectToAction("Lista");
                }
                catch
                {
                    ModelState.AddModelError(String.Empty, "Ha ocurrido un error");
                }
            }
            libro.Categorias = await _servicioLista.GetListaCategorias();
            libro.Autores = await _servicioLista.GetListaAutores();
            return View(libro);
        }

        public async Task<IActionResult> Editar(int? id)
        {
            if (id == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros.FindAsync(id);
            if (libro == null)
            {
                return NotFound();
            }

            libro.Categorias = await _servicioLista.GetListaCategorias();
            libro.Autores = await _servicioLista.GetListaAutores();

            return View(libro);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Libro libro, IFormFile Imagen)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    var libroExistente = await _context.Libros.FindAsync(libro.Id);
                    if (libroExistente == null)
                    {
                        return NotFound();
                    }

                    if (Imagen != null)
                    {
                        Stream image = Imagen.OpenReadStream();
                        string urlimagen = await _servicioImagen.SubirImagen(image, Imagen.FileName);
                        libroExistente.URLImagen = urlimagen;
                    }

                    libroExistente.Titulo = libro.Titulo;
                    libroExistente.Autor = await _context.Autores.FindAsync(libro.AutorId);
                    libroExistente.Categoria = await _context.Categorias.FindAsync(libro.CategoriaId);
                    libroExistente.Precio = libro.Precio;

                    _context.Update(libroExistente);
                    await _context.SaveChangesAsync();

                    return RedirectToAction("Lista");
                }
                catch
                {
                    ModelState.AddModelError(string.Empty, "Ha ocurrido un error");
                }
            }

            libro.Categorias = await _servicioLista.GetListaCategorias();
            libro.Autores = await _servicioLista.GetListaAutores();

            return View(libro);
        }


        public async Task<IActionResult> Eliminar(int? id)
        {
            if (id == null || _context.Libros == null)
            {
                return NotFound();
            }

            var libro = await _context.Libros
                .FirstOrDefaultAsync(m => m.Id == id);

            if (libro == null)
            {
                return NotFound();
            }

            try
            {
                _context.Libros.Remove(libro);
                await _context.SaveChangesAsync();

            }
            catch (Exception ex)
            {
                ModelState.AddModelError(ex.Message, "Ocurrio un error, no se pudo eliminar el registro");
            }

            return RedirectToAction(nameof(Lista));
        }
    }
}
