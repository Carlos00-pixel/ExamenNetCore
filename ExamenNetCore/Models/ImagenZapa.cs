using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace ExamenNetCore.Models
{
    [Table("IMAGENESZAPASPRACTICA")]
    public class ImagenZapa
    {
        [Key]
        [Column("IDIMAGEN")]
        public int IdImagen { get; set; }

        [Column("IDPRODUCTO")]
        public int IdProducto { get; set; }

        [Column("IMAGEN")]
        public string Imagen { get; set; }
    }
}
