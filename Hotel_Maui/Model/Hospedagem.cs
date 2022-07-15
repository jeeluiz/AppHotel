using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Hotel_Maui.Model
{
    public class Hospedagem
    {

        public CategoriaQuarto Quarto { get; set; }
        public int QuantidadeAdultos { get; set; }
        public int QuantidadeCrianca { get; set; }
        public int QuantidadeDias { get; set; }
        public DateTime DataCheckIn { get; set; }
        public DateTime DataCheckOut { get; set; }
        public TimeSpan HoraCheckIn { get; set; }
        public TimeSpan HoraCheckOut { get; set; }
        public double ValorTotal { get; set; }

        public static int CalcularTempoEstadia(DateTime checkin,DateTime checkout)
        {
            int total_dias = checkout.Subtract(checkin).Days;

            //if (total_dias <= total_dias)
            //    throw new Exception("saida de dias nao pode ser inferior a entrada");
            return total_dias;
        }

        public double CalcularValorEstadia()
        {
            double valor_adulto =(QuantidadeAdultos * Quarto.ValorDiariaAdulto) * QuantidadeDias;
            double valor_crianca = (QuantidadeCrianca * Quarto.ValorDiariaCrianca) * QuantidadeDias;
            double valor_hospedagem = valor_crianca + valor_adulto;
            
            return valor_hospedagem;
        }

    }
}
