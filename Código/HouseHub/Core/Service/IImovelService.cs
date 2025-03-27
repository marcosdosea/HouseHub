using Core.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Core.Service {
    public interface IImovelService 
    {
        uint Create(Imovel imovel);
        void Update(Imovel imovel);
        void Delete(uint imovel);
        void AssociarImagemAoImovel(uint imovelId, uint imagemId);
        Imovel ? Get(uint id);
        IEnumerable<Imovel> GetAll();
        IEnumerable<Imovel> GetAll(BuscarImovelDto busca);
        ImovelDto ? GetImovelDto(uint id);
        List<ImovelDto>? GetImoveisDtoByPessoa(uint idPessoa);
    }
}
