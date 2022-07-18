using Hotel_Maui.Enums;
using System.ComponentModel.DataAnnotations;

namespace Hotel_Maui.Model
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
