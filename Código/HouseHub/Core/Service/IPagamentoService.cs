﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service {
    public interface IPagamentoService
    {
        uint Create(Pagamento pagamento);
        IEnumerable<Pagamento> GetAllCobranca(int idPessoa);
    }
}
