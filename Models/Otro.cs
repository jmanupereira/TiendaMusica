using System;
using System.ComponentModel.DataAnnotations;

namespace TiendaMusica.Models
{
    public class Otro
    {
        [Key]
        public Guid ID { get; set; }

        [Required(ErrorMessage = "Ingrese la marca del producto.")]
        public string Marca { get; set; }

        [Required(ErrorMessage = "Ingrese el modelo del producto.")]
        public string Modelo { get; set; }
        
        [MaxLength(150)]
        public string Descripcion { get; set; }

        [Required(ErrorMessage = "Ingrese el precio del producto.")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "Ingrese el stock del producto.")]
        public int Stock { get; set; }
    }
}