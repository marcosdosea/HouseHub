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
    public class AvaliacaoService : IAvalicaoService
    {
        private readonly HouseHubContext houseHubContext;

        public AvaliacaoService(HouseHubContext houseHubContext)
        {
            this.houseHubContext = houseHubContext;
        }

        /// <summary>
        /// Cria uma nova Avaliação.
        /// </summary>
        /// <param name="avaliacao">O objeto Avaliação a ser criado.</param>
        /// <returns>O ID da Avaliação criada.</returns>
        public uint Create(Avaliacao avaliacao)
        {
            houseHubContext.Add(avaliacao);
            houseHubContext.SaveChanges();
            return avaliacao.Id;
        }

        /// <summary>
        /// Exclui uma Avaliação existente.
        /// </summary>
        /// <param name="avaliacao">O objeto Avaliação a ser excluído.</param>
        public void Delete(Avaliacao avaliacao)
        {
            var _avaliacao = houseHubContext.Avaliacaos.Find(avaliacao.Id);
            if (_avaliacao != null)
            {
                houseHubContext.Remove(_avaliacao);
                houseHubContext.SaveChanges();
            }
        }

        /// <summary>
        /// Recupera uma Avaliação pelo seu ID.
        /// </summary>
        /// <param name="id">O ID da Avaliação a ser recuperada.</param>
        /// <returns>A Avaliação recuperada, ou nulo se não encontrada.</returns>
        public Avaliacao? Get(int id)
        {
            return houseHubContext.Avaliacaos.Find(id);
        }

        /// <summary>
        /// Recupera todas as Avaliações.
        /// </summary>
        /// <returns>Um IEnumerable de objetos Avaliação.</returns>
        public IEnumerable<Avaliacao> GetAll()
        {
            return houseHubContext.Avaliacaos.AsNoTracking();
        }

        /// <summary>
        /// Atualiza uma Avaliação já existente.
        /// </summary>
        /// <param name="avaliacao">Objeto Avaliação que será atualizado.</param>
        public void Update(Avaliacao avaliacao)
        {
            houseHubContext.Update(avaliacao);
            houseHubContext.SaveChanges();
        }
    }
}
