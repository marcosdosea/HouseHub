using AutoMapper;
using Core;
using HouseHubWeb.Models;

namespace HouseHubWeb.Mappers
{
    public class ImovelProfile : Profile
    {
        public ImovelProfile()
        {
            CreateMap<ImovelViewModel, Imovel>().ReverseMap();
        }
    }
}
