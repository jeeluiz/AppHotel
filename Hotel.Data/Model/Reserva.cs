using System.ComponentModel.DataAnnotations;

namespace Hotel.Data.Model
{
    public class Reserva 
    {
        [Key]
        public Guid Id { get; set; }
        public CategoriaQuarto Quarto { get; set; }
        public CadastroHospede Hospede { get; set; }
        public int QuantidadeAdultos { get; set; }
        public int QuantidadeCrianca { get; set; }
        public int QuantidadeDias { get; set; }
        public DateTime DataCheckIn { get; set; }
        public DateTime DataCheckOut { get; set; }
        public TimeSpan HoraCheckIn { get; set; }
        public TimeSpan HoraCheckOut { get; set; }
        public double ValorTotal { get; set; }

  

    }
}
