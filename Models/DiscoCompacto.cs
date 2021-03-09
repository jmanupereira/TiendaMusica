using System;
using System.ComponentModel.DataAnnotations;

namespace TiendaMusica.Models
{
    public class DiscoCompacto
    {
        [Key]
        public Guid ID { get; set; }

        [Required(ErrorMessage = "Ingrese el sello discografico.")]
        public string Sello { get; set; }

        public string CodigoSello { get; set; }
        
        [Required(ErrorMessage = "Ingrese el nombre del artista.")]
        public string Artista { get; set; }

        [Required(ErrorMessage = "Ingrese el titulo del CD.")]
        public string Titulo { get; set; }

        public DateTime FechaPublicacion { get; set; }

        public string Genero { get; set; }

        public string Estilo { get; set; }

        [Required(ErrorMessage = "Ingrese el precio del producto.")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "Ingrese el stock del producto.")]
        public int Stock { get; set; }
    }
}