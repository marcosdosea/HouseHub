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
    public class PagamentoService : IPagamentoService
    {
        private readonly HouseHubContext houseHubContext;

        public PagamentoService(HouseHubContext houseHubContext)
        {
            this.houseHubContext = houseHubContext;
        }

        /// <summary>
        /// Cria um pagamento
        /// </summary>
        /// <param name="pagamento"></param>
        /// <returns>Retorna o id do pagamento cadastrado</returns>
        public uint Create(Pagamento pagamento)
        {
            houseHubContext.Pagamentos.Add(pagamento);
            houseHubContext.SaveChanges();
            return pagamento.Id;
        }

        /// <summary>
        /// Deleta um pagamento
        /// </summary>
        /// <param name="pagamento"></param>
        public void Delete(Pagamento pagamento)
        {
            houseHubContext.Pagamentos.Remove(pagamento);
            houseHubContext.SaveChanges();
        }

        /// <summary>
        /// Obter um pagamento
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna um registro de pagamento através do id</returns>
        public Pagamento? Get(int id)
        {
            return houseHubContext.Pagamentos.Find(id);
        }

        /// <summary>
        /// Obter todos os pagamentos com as no tracking
        /// </summary>
        /// <returns>Retorna uma lista com todos os pagamentos</returns>
        public IEnumerable<Pagamento> GetAll()
        {
            return houseHubContext.Pagamentos.AsNoTracking();
        }

        /// <summary>
        /// Atualiza o registro de um pagamento
        /// </summary>
        /// <param name="pagamento"></param>
        public void Update(Pagamento pagamento)
        {
            houseHubContext.Pagamentos.Update(pagamento);
            houseHubContext.SaveChanges();
        }
    }
}
