using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service
{
    public interface IImagemService
    {
        Task<uint> CreateAsync(Imagem imagem);
        Task<bool> AssociarImagemAoImovelAsync(uint imovelId, uint imagemId);
        Task<List<Imagem>> GetImagensByImovelIdAsync(uint imovelId);

    }
}
