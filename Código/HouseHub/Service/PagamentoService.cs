using Core;
using Core.Service;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Localization;
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

        public uint Create(Pagamento pagamento)
        {
            houseHubContext.Add(pagamento);
            return pagamento.Id;
        }

        public IEnumerable<Pagamento> GetAllCobranca(int idPessoa)
        {
            var locacoes = houseHubContext.Locacaos.Where(l => l.IdPessoa == idPessoa);
            return houseHubContext.Pagamentos
                .Where(p => locacoes.Any(l => l.Id == p.Id) && p.DataVencimento <= DateTime.Now.AddDays(2));
        }

    }
}
