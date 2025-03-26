using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core
{
    public partial class ImovelImagem
    {
        public uint Imovel_id { get; set; }
        public uint Imagem_id { get; set; }

        public virtual Imovel Imovel { get; set; } = null!;
        public virtual Imagem Imagem { get; set; } = null!;
    }
}
