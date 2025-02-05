using Core;
using Core.Service;
using Microsoft.EntityFrameworkCore;
using System;

namespace Service
{
    public class AvaliacaoService : IAvalicaoService
    {
        private readonly HouseHubContext _houseHubContext;

        // Definindo a constante para o valor mínimo de renda per capita
        private const decimal RENDA_PER_CAPITA_MINIMA = 1412m;

        public AvaliacaoService(HouseHubContext houseHubContext)
        {
            _houseHubContext = houseHubContext;
        }

        private bool VerificaRendaPerCapita(Avaliacao avaliacao)
        {
            if (avaliacao.NumeroDependentes == 0)
            {
                // Se não há dependentes, a renda per capita é a própria renda mensal
                return avaliacao.RendaMensal >= RENDA_PER_CAPITA_MINIMA;
            }
            else
            {
                // Calcula a renda per capita
                decimal rendaPerCapita = avaliacao.RendaMensal / avaliacao.NumeroDependentes;
                return rendaPerCapita >= RENDA_PER_CAPITA_MINIMA;
            }
        }

        public uint Create(Avaliacao avaliacao)
        {
            if (VerificaRendaPerCapita(avaliacao))
            {
                avaliacao.Status = "Aprovado";
                avaliacao.ValorAprovado = avaliacao.RendaMensal * 0.4m; // 40% da renda mensal
            }
            else
            {
                avaliacao.Status = "Análise";
                avaliacao.ValorAprovado = 0;
            }

            _houseHubContext.Add(avaliacao);
            _houseHubContext.SaveChanges();
            return avaliacao.Id;
        }
    }
}