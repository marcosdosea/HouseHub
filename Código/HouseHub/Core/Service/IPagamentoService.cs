using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service {
    public interface IPagamentoService
    {
        uint Create(Pagamento pagamento);
        void Update(Pagamento pagamento);
        void Delete(Pagamento pagamento);
        Pagamento? Get(int id);
        IEnumerable<Pagamento> GetAll();

    }
}
