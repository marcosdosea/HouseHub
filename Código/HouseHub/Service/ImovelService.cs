using Core;
using Core.Service;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Service {
    public class ImovelService : IImovelService {

        private readonly HouseHubContext houseHubContext;

        public ImovelService(HouseHubContext houseHubContext)
        {
            this.houseHubContext = houseHubContext;
        }
        /// <summary>
        /// cria um imovel
        /// </summary>
        /// <param name="imovel"></param>
        /// <returns>id do imovel</returns>
        public uint Create(Imovel imovel) {
            houseHubContext.Imovels.Add(imovel);
            houseHubContext.SaveChanges();
            return imovel.Id;
        }

        public void Delete(Imovel imovel) {
            var imovelToDelete = houseHubContext.Imovels.Find(imovel.Id);
            if (imovelToDelete != null)
            {
                houseHubContext.Remove(imovelToDelete);
                houseHubContext.SaveChanges();
            }
        }

        public Imovel Get(uint id) {
            return houseHubContext.Imovels.AsNoTracking().Find(id);
        }

        public IEnumerable<Imovel> GetAll() {
            return houseHubContext.Imovels.AsNoTracking();
        }

        public void Update(Imovel imovel) {
            houseHubContext.Imovels.Update(imovel);
            houseHubContext.SaveChanges();
        }
    }
}
