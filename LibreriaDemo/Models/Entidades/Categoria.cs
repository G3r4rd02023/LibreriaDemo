﻿using System.ComponentModel.DataAnnotations;

namespace LibreriaDemo.Models.Entidades
{
    public class Categoria
    {
        public int Id { get; set; }

        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public string Nombre { get; set; }

        [DataType(DataType.MultilineText)]
        public string Descripcion { get; set;}

        public ICollection<Libro> Libros { get; set; }
    }
}
