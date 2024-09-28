using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class BuscarImovelDto
    {
        public string Cidade { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public decimal ValorMaximo { get; set; }
        public byte Quartos { get; set; }
        public string Modalidade { get; set; } = string.Empty;
    }
}
