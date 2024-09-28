using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service {
    public interface IAvalicaoService 
    {
        uint Create(Avaliacao avaliacao);
        void Update(Avaliacao avaliacao);
        void Delete(Avaliacao avaliacao);
        Avaliacao ? Get(int id);
        IEnumerable<Avaliacao> GetAll();
    }

}
