using System;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace TiendaMusica.Models
{
    public class Vinilo
    {
        [Key]
        public Guid ID { get; set; }

        [Required(ErrorMessage = "Ingrese el sello discografico.")]
        public string Sello { get; set; }

        public string CodigoSello { get; set; }
        
        [Required(ErrorMessage = "Ingrese el nombre del artista.")]
        public string Artista { get; set; }

        [Required(ErrorMessage = "Ingrese el titulo del vinilo.")]
        public string Titulo { get; set; }

        public string Pais { get; set; }

        public string Tipo { get; set; }

        public string Pulgadas { get; set; }

        public int RPM { get; set; }

        public DateTime FechaPublicacion { get; set; }

        public string Genero { get; set; }

        public string Estilo { get; set; }

        [Required(ErrorMessage = "Ingrese el precio del producto.")]
        public decimal Precio { get; set; }

        [Required(ErrorMessage = "Ingrese el stock del producto.")]
        public int Stock { get; set; }

        [NotMapped]
        public string Formato
        {
            get 
            {
                return Tipo + ", " + Pulgadas + "; " + RPM;
            }
        }
    }
}