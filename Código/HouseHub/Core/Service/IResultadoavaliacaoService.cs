using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service {
    public interface IResultadoavaliacaoService 
    {
        uint Create(Resultadoavaliacao resultadoavaliacao);
        void Update(Resultadoavaliacao resultadoavaliacao);
        void Delete(uint id);
        Resultadoavaliacao? Get(int id);
        IEnumerable<Resultadoavaliacao> GetAll();

    }
}
