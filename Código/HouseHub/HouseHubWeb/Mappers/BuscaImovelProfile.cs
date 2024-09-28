using AutoMapper;
using Core.DTOs;
using HouseHubWeb.Models;

namespace HouseHubWeb.Mappers
{
    public class BuscaImovelProfile : Profile
    {
        public BuscaImovelProfile()
        {
            CreateMap<BuscarImovelDto, BuscarImovelViewModel>().ReverseMap();
        }
    }
}
