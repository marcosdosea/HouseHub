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
    }
}
