using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service {
    public interface IPessoaService 
    {
        void Create(Pessoa pessoa);
        void Update(Pessoa pessoa);
        void Delete(Pessoa pessoa);
        Pessoa Get(int id);
        IEnumerable<Pessoa> GetAll();
        IEnumerable<Pessoa> GetByNome(string nome);
    }
}
