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

        // Definindo a constante para o valor mínimo de renda per capita
        private const decimal RENDA_PER_CAPITA_MINIMA = 1.412m;

        public AvaliacaoService(HouseHubContext houseHubContext)
        {
            this.houseHubContext = houseHubContext;
        }

        private bool VerificaRendaPerCapita(Avaliacao avaliacao)
        {
            return avaliacao.NumeroDependentes == 0 && avaliacao.NumeroDependentes >= RENDA_PER_CAPITA_MINIMA || avaliacao.RendaMensal / avaliacao.NumeroDependentes >= RENDA_PER_CAPITA_MINIMA;
        }

        /// <summary>
        /// Cria uma nova Avaliação.
        /// </summary>
        /// <param name="avaliacao">O objeto Avaliação a ser criado.</param>
        /// <returns>O ID da Avaliação criada.</returns>
        public uint Create(Avaliacao avaliacao)
        {
            if (VerificaRendaPerCapita(avaliacao))
            {
                avaliacao.Status = "Aprovado";
                avaliacao.ValorAprovado = avaliacao.RendaMensal * 0.4m;
            }
            else
            {
                avaliacao.Status = "Análise";
                avaliacao.ValorAprovado = 0;
            }

            houseHubContext.Add(avaliacao);
            houseHubContext.SaveChanges();
            return avaliacao.Id;
        }
    }
}
