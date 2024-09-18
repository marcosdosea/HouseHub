using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service {
    public interface IAgendamentoService 
    {
        uint Create(Agendamento agendamento);
        void Update(Agendamento agendamento);
        void Delete(uint Id);
        Agendamento? Get(uint Id);
        IEnumerable<Agendamento> GetAll();
    }
}
