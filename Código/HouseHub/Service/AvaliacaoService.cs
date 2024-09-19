using Core;
using Core.Service;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service {
    public class AvaliacaoService : IAvalicaoService
    {
        private readonly HouseHubContext houseHubContext;

        public AvaliacaoService(HouseHubContext houseHubContext)
        {
            this.houseHubContext = houseHubContext;
        }

        public uint Create(Avaliacao avaliacao)
        {
            houseHubContext.Add(avaliacao);
            houseHubContext.SaveChanges();
            return avaliacao.Id;
        }

        public void Delete(Avaliacao avaliacao)
        {
            var _avaliacao = houseHubContext.Avaliacaos.Find(avaliacao.Id);
            if( _avaliacao != null)
            {
                houseHubContext.Remove(_avaliacao);
                houseHubContext.SaveChanges();
            }
        }

        public Avaliacao Get(int id)
        {
            return houseHubContext.Avaliacaos.Find(id);
        }

        public IEnumerable<Avaliacao> GetAll()
        {
            return houseHubContext.Avaliacaos.AsNoTracking();
        }

        public void Update(Avaliacao avaliacao)
        {
            houseHubContext.Update(avaliacao);
            houseHubContext.SaveChanges();
        }
    }
}
