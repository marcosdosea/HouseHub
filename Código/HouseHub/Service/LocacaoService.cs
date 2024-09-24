using Core;
using Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service
{
    public class LocacaoService : ILocacaoService
    {
        private readonly HouseHubContext houseHubContext;
        public LocacaoService(HouseHubContext houseHubContext)
        {
            this.houseHubContext = houseHubContext;
        }
        /// <summary>
        /// Cria uma locação no banco de dados
        /// </summary>
        /// <param name="locacao"></param>
        /// <returns>retorna o id da locação criada</returns>

        public uint Create(Locacao locacao)
        {
            houseHubContext.Locacaos.Add(locacao);
            houseHubContext.SaveChanges();
            return locacao.Id;
        }

        /// <summary>
        /// deleta uma locação do banco de dados
        /// </summary>
        /// <param name="id"></param>
        public void Delete(uint id)
        {
            var locacao = houseHubContext.Locacaos.Find(id);
            if (locacao != null)
            {
                houseHubContext.Remove(locacao);
                houseHubContext.SaveChanges();
            }
        }
        /// <summary>
        /// busca uma locação no banco de dados pelo id
        /// </summary>
        /// <param name="id"></param>
        /// <returns>retorna uma locação caso não eista retorna nulo</returns>

        public Locacao? Get(uint id)
        {
            return houseHubContext.Locacaos.Find(id);
        }

        /// <summary>
        /// retorna todas as locações do banco de dados
        /// </summary>
        /// <returns></returns>

        public IEnumerable<Locacao> GetAll()
        {
            return houseHubContext.Locacaos;
        }

        /// <summary>
        /// atualiza uma locação no banco de dados
        /// </summary>
        /// <param name="locacao"></param>
        public void Update(Locacao locacao)
        {
            houseHubContext.Locacaos.Update(locacao);
            houseHubContext.SaveChanges();
        }
    }
}
