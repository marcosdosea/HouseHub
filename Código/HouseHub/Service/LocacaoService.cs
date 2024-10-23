using Core;
using Core.Service;

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
        /// Cria uma locação no banco de dados caso nao tenha nenhuma locacao ativa para determinado imovel
        /// </summary>
        /// <param name="locacao"></param>
        /// <returns>retorna o id da locação criada</returns>

        public uint Create(Locacao locacao)
        {
            var locacoes = houseHubContext.Locacaos
                .Where(l => l.IdImovel == locacao.IdImovel && l.Status == "Ativo");
            if (locacoes == null || locacoes.Count() == 0)
            {
                houseHubContext.Locacaos.Add(locacao);
                houseHubContext.SaveChanges();
                return locacao.Id;
            }
            throw new Exception("Imóvel já alugado");
        }

        /// <summary>
        /// Inativa uma locação do banco de dados
        /// </summary>
        /// <param name="id"></param>
        public void Delete(uint id)
        {
            var locacao = houseHubContext.Locacaos.Find(id);
            if (locacao != null)
            {
                locacao.DataFim = DateTime.Now;
                locacao.Status = "Inativo";
                houseHubContext.Locacaos.Update(locacao);
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
