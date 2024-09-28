using Core;
using Core.Service;
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

    }
}
