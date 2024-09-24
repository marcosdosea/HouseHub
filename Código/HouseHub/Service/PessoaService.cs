using Core;
using Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service {
    public class PessoaService : IPessoaService 
    {

        private readonly HouseHubContext houseHubContext;
        public PessoaService(HouseHubContext houseHubContext) 
        {
            this.houseHubContext = houseHubContext;
        }

        /// <summary>
        /// Cria uma pessoa
        /// </summary>
        /// <param name="pessoa"></param>
        /// <returns>Retorna o id da pessoa cadastrada</returns>
        public uint Create(Pessoa pessoa) 
        {
            houseHubContext.Pessoas.Add(pessoa);
            houseHubContext.SaveChanges();
            return pessoa.Id;
        }

        /// <summary>
        /// Deleta uma pessoa
        /// </summary>
        /// <param name="Id"></param>
        /// <returns>Retorna verdadeiro se a remoção for bem sucedida</returns>
        public bool Delete(uint Id) 
        {
            var Pessoa = houseHubContext.Pessoas.Find(Id);
            if(Pessoa != null) 
            {
                houseHubContext.Remove(Pessoa);
                houseHubContext.SaveChanges();
                return true;
            }
            return false;
        }

        /// <summary>
        /// Obter uma pessoa
        /// </summary>
        /// <param name="id"></param>
        /// <returns>Retorna um registro de pessoa através do id</returns>
        public Pessoa? Get(uint id)
        {
            return houseHubContext.Pessoas.Find(id);
        }

        /// <summary>
        /// Obter todas as pessoas com as no tracking
        /// </summary>
        /// <returns>Retorna uma lista com todas as pessoas</returns>
        public IEnumerable<Pessoa> GetAll() 
        {
            return houseHubContext.Pessoas.AsNoTracking();
        }

        /// <summary>
        /// Obter uma ou mais pessoas pelo nome com as no tracking
        /// </summary>
        /// <param name="nome"></param>
        /// <returns>Retorna uma ou mais pessoas que contenham o mesmo nome</returns>
        public IEnumerable<Pessoa> GetByNome(string nome) 
        {
            return houseHubContext.Pessoas.AsNoTracking().Where(p => p.Nome.Contains(nome));
        }

        /// <summary>
        /// Atualiza o registro de uma pessoa
        /// </summary>
        /// <param name="pessoa"></param>
        public void Update(Pessoa pessoa) 
        {
            houseHubContext.Pessoas.Update(pessoa);
            houseHubContext.SaveChanges();
        }
    }
}
