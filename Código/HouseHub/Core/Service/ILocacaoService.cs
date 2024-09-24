using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service {
    public interface ILocacaoService 
    {
        uint Create(Locacao locacao);
        void Update(Locacao locacao);
        void Delete(uint id);
        Locacao? Get(uint id);
        IEnumerable<Locacao> GetAll();

    }
}
