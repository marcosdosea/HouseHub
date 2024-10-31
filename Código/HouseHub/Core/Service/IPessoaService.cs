using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service {
    public interface IPessoaService 
    {
        uint GetUserByCpf(string cpf);
        uint GetUserByEmail(string email);
        uint Create(Pessoa pessoa);
        void Update(Pessoa pessoa);
        bool Delete(uint Id);
        Pessoa? Get(uint id);
        IEnumerable<Pessoa> GetAll();
        IEnumerable<Pessoa> GetByNome(string nome);
        PessoaDto ? GetByCpf(string cpf);
        PessoaDto ? GetByEmail(string email);
    }
}
