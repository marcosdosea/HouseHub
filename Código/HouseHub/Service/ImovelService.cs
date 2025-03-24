using Core;
using Core.DTOs;
using Core.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service {
    public class ImovelService : IImovelService {

        private readonly HouseHubContext houseHubContext;

        public ImovelService(HouseHubContext houseHubContext)
        {
            this.houseHubContext = houseHubContext;
        }
        /// <summary>
        /// cria um imovel
        /// </summary>
        /// <param name="imovel"></param>
        /// <returns>id do imovel</returns>
        public uint Create(Imovel imovel) {
            houseHubContext.Imovels.Add(imovel);
            houseHubContext.SaveChanges();
            return imovel.Id;
        }
        /// <summary>
        /// remove um imovel
        /// </summary>
        /// <param name="imovel"></param>
        public void Delete(uint imovel) {
            var imovelToDelete = houseHubContext.Imovels.Find(imovel);
            if (imovelToDelete != null)
            {
                houseHubContext.Remove(imovelToDelete);
                houseHubContext.SaveChanges();
            }
        }
        /// <summary>
        /// pega um imovel
        /// </summary>
        /// <param name="id"></param>
        /// <returns> retorna o imovel </returns>
        public Imovel ? Get(uint id) {
            return houseHubContext.Imovels.FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// pega todos os imoveis
        /// </summary>
        /// <returns> vai retornar os imoveis</returns>
        public IEnumerable<Imovel> GetAll() {
            return houseHubContext.Imovels.AsNoTracking();
        }

        /// <summary>
        /// Pega todos os imóveis de acordo com os parametros definidos na busca
        /// </summary>
        /// <param name="busca"></param>
        /// <returns>retorna os imoveis</returns>
        public IEnumerable<Imovel> GetAll(BuscarImovelDto busca)
        {
            var imoveis = houseHubContext.Imovels.AsNoTracking();

            if(busca.Cidade != null)
            {
                imoveis = imoveis.Where(x => x.Cidade.ToLower() == busca.Cidade.ToLower());
            }
            if (busca.Bairro != null)
            {
                imoveis = imoveis.Where(x => x.Bairro.ToLower() == busca.Bairro.ToLower());
            }
            if (busca.Quartos != null)
            {
                imoveis = imoveis.Where(x => x.Quartos == busca.Quartos);
            }
            if (busca.ValorMaximo != null)
            {
                imoveis = imoveis.Where(x => x.PrecoVenda < busca.ValorMaximo);
            }
            if (busca.Modalidade != null)
            {

                imoveis = imoveis.Where(x => x.Modalidade.ToLower() == busca.Modalidade.ToLower() || x.Modalidade.ToLower() == "ambos");
            }

            return imoveis;
        }

        public ImovelDto? GetImovelDto(uint id)
        {
            return houseHubContext.Imovels.Select(x => new ImovelDto
            { 
                Iptu = x.Iptu,
                IdImovel = x.Id,
                PrecoAluguel = (decimal)x.PrecoAluguel,
                Status = x.Status,
            }).FirstOrDefault(x => x.IdImovel == id);
        }

        /// <summary>
        /// atualiza um imovel
        /// </summary>
        /// <param name="imovel"></param>
        public void Update(Imovel imovel) {
            houseHubContext.Imovels.Update(imovel);
            houseHubContext.SaveChanges();
        }



        public List<ImovelDto> GetImoveisDtoByPessoa(uint idPessoa)
        {
            return houseHubContext.Locacaos
                .Where(locacao => locacao.IdPessoa == idPessoa)
                .Join(
                    houseHubContext.Imovels,
                    locacao => locacao.IdImovel,
                    imovel => imovel.Id,
                    (locacao, imovel) => new ImovelDto
                    {
                        Iptu = imovel.Iptu == null ? 0 : imovel.Iptu,
                        IdImovel = imovel.Id,
                        PrecoAluguel = imovel.PrecoAluguel.HasValue ? (decimal)imovel.PrecoAluguel.Value : 0m,
                        Status = imovel.Status ?? string.Empty,
                    }
                )
                .ToList();
        }
    }
}
