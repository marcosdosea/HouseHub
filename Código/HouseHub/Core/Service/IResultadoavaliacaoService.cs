using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service {
    public interface IResultadoavaliacaoService 
    {
        void Create(Resultadoavaliacao resultadoavaliacao);
        void Update(Resultadoavaliacao resultadoavaliacao);
        void Delete(Resultadoavaliacao resultadoavaliacao);
        Resultadoavaliacao Get(int id);
        IEnumerable<Resultadoavaliacao> GetAll();

    }
}
