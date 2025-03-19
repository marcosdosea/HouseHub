using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class MeusImoveisDto
    {
        public uint IdImovel { get; set; }
        public string UrlImagem { get; set; } = string.Empty;
        public string Cidade { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public decimal ValorAluguel { get; set; }
        public DateTime DataProximoPagamento { get; set; }
    }
}
