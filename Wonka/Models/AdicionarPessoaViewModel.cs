using System.Collections.Generic;

namespace Wonka.Models
{
    public class AdicionarPessoaViewModel
    {
        public Pessoa Pessoa { get; set; }
        public Endereco Endereco { get; set; }
        public List<Documento> Documento { get; set; }
        public List<Telefone> Telefone { get; set; }        
    }
}
