using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc.Rendering;

namespace LibreriaDemo.Models.Entidades
{
    public class Libro
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Titulo { get; set; }

        public Autor Autor { get; set; }

        public Categoria Categoria { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Precio { get; set; }

        [Display(Name = "Imagen")]
        public string URLImagen { get; set; }

       
        [Range(1, int.MaxValue, ErrorMessage = "Debes de seleccionar un autor.")]        
        public int AutorId { get; set; }

        [Range(1, int.MaxValue, ErrorMessage = "Debes de seleccionar una categoria.")]
        public int CategoriaId { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> Categorias { get; set; }

        [NotMapped]
        public IEnumerable<SelectListItem> Autores { get; set; }



    }
}
