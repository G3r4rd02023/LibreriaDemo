using System.ComponentModel.DataAnnotations;

namespace LibreriaDemo.Models.Entidades
{
    public class Venta
    {
        public int Id { get; set; }

        public Libro Libro { get; set; }

        [DisplayFormat(DataFormatString = "{0:N2}")]
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public float Cantidad {  get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}")]
        public DateTime Fecha { get; set; }

    }
}
