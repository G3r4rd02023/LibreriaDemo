using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace LibreriaDemo.Models.Entidades
{
    public class Venta
    {
        public int Id { get; set; }

        public Libro Libro { get; set; }

        public Usuario Usuario { get; set; }


       
        [Required(ErrorMessage = "El campo {0} es obligatorio.")]
        public int Cantidad {  get; set; }

        [DisplayFormat(DataFormatString = "{0:yyyy/MM/dd hh:mm tt}")]
        public DateTime Fecha { get; set; }
      
        [Column(TypeName = "decimal(18,2)")]
        [DisplayFormat(DataFormatString = "{0:C2}")]
        public decimal Total { get; set; }


    }
}
