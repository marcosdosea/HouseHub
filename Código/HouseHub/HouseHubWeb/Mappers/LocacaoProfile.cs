using AutoMapper;
using Core;
using Core.DTOs;
using HouseHubWeb.Models;

namespace HouseHubWeb.Mappers
{
    public class LocacaoProfile : Profile
    {
        public LocacaoProfile()
        {
            CreateMap<Locacao, LocacaoViewModel>().ReverseMap();
            CreateMap<ImovelDto, LocacaoViewModel>().ReverseMap();
        }
    }
}
