using System.ComponentModel.DataAnnotations;

namespace Hotel_Maui.Model
{
    public class CadastroHospede
    {
        [Key]
        public Guid Id { get; set; }
        public string Nome { get; set; }
        public string CPF { get; set; }
        public string Cep { get; set; }
        public string Endereco { get; set; }
        public string NumeroEndereco { get; set; }
    }
}
