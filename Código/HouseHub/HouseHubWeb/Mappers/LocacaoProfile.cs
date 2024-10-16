using AutoMapper;
using Core.DTOs;
using HouseHubWeb.Models;

namespace HouseHubWeb.Mappers
{
    public class LocacaoProfile : Profile
    {
        public LocacaoProfile()
        {
            CreateMap<ImovelDto, LocacaoViewModel>().ReverseMap();
        }
    }
}
