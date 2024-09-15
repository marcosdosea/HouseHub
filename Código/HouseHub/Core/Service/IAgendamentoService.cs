using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service {
    public interface IAgendamentoService 
    {
        void Create(Agendamento agendamento);
        void Edit(Agendamento agendamento);
        void Delete(Agendamento agendamento);
        Agendamento Get(int id);
        IEnumerable<Agendamento> GetAll();
        Agendamento GetById(int id);
    }
}
