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
    public class AgendamentoService : IAgendamentoService
    {
        private readonly HouseHubContext houseHubContext;

        public AgendamentoService(HouseHubContext houseHubContext)
        {
            this.houseHubContext = houseHubContext;
        }

        /// <summary>
        /// Cria um agendamento
        /// </summary>
        /// <param name="agendamento"></param>
        /// <returns>Id do agendamento criado</returns>
        public uint Create(Agendamento agendamento)
        {
            houseHubContext.Agendamentos.Add(agendamento);
            houseHubContext.SaveChanges();
            return agendamento.Id;
        }

        public void Delete(uint Id)
        {
            var agendamento = houseHubContext.Agendamentos.Find(Id);
            if (agendamento != null)
            {
                houseHubContext.Remove(agendamento);
                houseHubContext.SaveChanges();
            }

        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public Agendamento? Get(uint id)
        {
            return houseHubContext.Agendamentos.AsNoTracking().FirstOrDefault(x => x.Id == id);
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public IEnumerable<Agendamento> GetAll()
        {
            return houseHubContext.Agendamentos.AsNoTracking();
        }

        /// <summary>
        /// faz a atualizacao de uma entidade caso nao esteja usando as no tracking
        /// </summary>
        /// <param name="agendamento"></param>
        public void Update(Agendamento agendamento)
        {
            houseHubContext.Agendamentos.Update(agendamento);
            houseHubContext.SaveChanges();
        }
    }
}
