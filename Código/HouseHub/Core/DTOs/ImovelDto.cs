using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.DTOs
{
    public class ImovelDto
    {
        public uint IdImovel { get; set; }
        public decimal Iptu { get; set; }
        public decimal PrecoAluguel { get; set; }
        public decimal PrecoCondominio { get; set; }
        public string NomeUsuario { get; set; } = string.Empty;
        public string Status { get; set; } = string.Empty;
    }
}
