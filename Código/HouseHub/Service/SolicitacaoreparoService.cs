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
    public class SolicitacaoreparoService : ISolicitacaoreparoService
    {

        private readonly HouseHubContext houseHubContext;

        public SolicitacaoreparoService(HouseHubContext houseHubContext)
        {
            this.houseHubContext = houseHubContext;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="solicitacaoreparo"></param>
        /// <returns>Id da solicitação do reparo criado</returns>
        public uint Create(Solicitacaoreparo solicitacaoreparo)
        {
            houseHubContext.Solicitacaoreparos.Add(solicitacaoreparo);
            houseHubContext.SaveChanges();
            return solicitacaoreparo.Id;
        }
        /// <summary>
        /// retorna true se a exclusão for bem sucedida e false se não for
        /// </summary>
        /// <param name="Id"></param>
        public bool Delete(uint Id)
        {
            var solicitacaoReparo = houseHubContext.Solicitacaoreparos.Find(Id);
            if (solicitacaoReparo != null)
            {
                houseHubContext.Remove(solicitacaoReparo);
                houseHubContext.SaveChanges();
                return true;
            }
            return false;
        }
        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns>retorna a solicitação de reparo encontrada</returns>
        public Solicitacaoreparo? Get(uint id)
        {
            return houseHubContext.Solicitacaoreparos.AsNoTracking().Find(id);
        }
        /// <summary>
        /// 
        /// </summary>
        /// <returns>retorna a lista de solicitações de reparo</returns>
        public IEnumerable<Solicitacaoreparo> GetAll()
        {
            return houseHubContext.Agendamentos.AsNoTracking();
        }
        /// <summary>
        /// faz a atualização de uma entidade caso não esteja usando as no tracking
        /// </summary>
        /// <param name="solicitacaoreparo"></param>
        public void Update(Solicitacaoreparo solicitacaoreparo)
        {
            houseHubContext.Solicitacaoreparos.Update(solicitacaoreparo);
            houseHubContext.SaveChanges();
        }
    }
}
