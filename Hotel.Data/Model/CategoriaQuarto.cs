using Hotel.Data.Enums;
using System.ComponentModel.DataAnnotations;

namespace Hotel.Data.Model
{
    public class CategoriaQuarto
    {
        [Key]
        public TipoQuarto Id { get; set; }
        public string Descricao { get; set; }
        public double ValorDiariaAdulto { get; set; }
        public double ValorDiariaCrianca { get; set; }
    }
}
