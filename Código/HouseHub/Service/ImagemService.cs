using Core;
using Core.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class ImagemService : IImagemService
    {
        private readonly HouseHubContext _dbContext;

        public ImagemService(HouseHubContext context)
        {
            _dbContext = context;
        }

        public async Task<uint> CreateAsync(Imagem imagem)
        {
            _dbContext.Set<Imagem>().Add(imagem);
            await _dbContext.SaveChangesAsync();
            return imagem.Id;
        }

        public async Task<bool> AssociarImagemAoImovelAsync(uint imovelId, uint imagemId)
        {
            try
            {
                var imovelImagem = new ImovelImagem
                {
                    Imovel_id = imovelId,
                    Imagem_id = imagemId
                };

                _dbContext.Set<ImovelImagem>().Add(imovelImagem);
                await _dbContext.SaveChangesAsync();
                return true;
            }
            catch
            {
                return false;
            }
        }

        public async Task<List<Imagem>> GetImagensByImovelIdAsync(uint imovelId)
        {
            return await _dbContext.Set<ImovelImagem>()
                .Where(ii => ii.Imovel_id == imovelId)
                .Select(ii => ii.Imagem)
                .ToListAsync();
        }
    }
}
