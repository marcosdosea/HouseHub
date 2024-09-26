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
    public class ResultadoavaliacaoService : IResultadoavaliacaoService
    {
        private readonly HouseHubContext houseHubContext;

        public ResultadoavaliacaoService(HouseHubContext houseHubContext)
        {
            this.houseHubContext = houseHubContext;
        }

        public uint Create(Resultadoavaliacao resultadoavaliacao)
        {
            houseHubContext.Add(resultadoavaliacao);
            houseHubContext.SaveChanges();
            return resultadoavaliacao.Id;
        }

        public void Delete(uint id)
        {
            var resultadoavaliacao = houseHubContext.Resultadoavaliacaos.Find(id);
            if (resultadoavaliacao == null) { return; }
            houseHubContext.Resultadoavaliacaos.Remove(resultadoavaliacao);
            houseHubContext.SaveChanges();
        }

        public Resultadoavaliacao? Get(int id)
        {
            var resultadoavaliacao = houseHubContext.Resultadoavaliacaos.Find(id);
            return resultadoavaliacao;
        }

        public IEnumerable<Resultadoavaliacao> GetAll()
        {
            return houseHubContext.Resultadoavaliacaos.AsNoTracking();
        }

        public void Update(Resultadoavaliacao resultadoavaliacao)
        {
            houseHubContext.Resultadoavaliacaos.Update(resultadoavaliacao);
            houseHubContext.SaveChanges();
        }
    }
}
