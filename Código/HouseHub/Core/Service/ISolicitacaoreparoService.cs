using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service {
    public interface ISolicitacaoreparoService 
    {
        uint Create(Solicitacaoreparo solicitacaoreparo);
        void Update(Solicitacaoreparo solicitacaoreparo);
        bool Delete(uint Id);
        Solicitacaoreparo? Get(uint id);
        IEnumerable<Solicitacaoreparo> GetAll();
    }
}
