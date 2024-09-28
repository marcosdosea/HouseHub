using AutoMapper;
using Core;
using HouseHubWeb.Models;

namespace HouseHubWeb.Mappers
{
    public class SolicitacaoReparoProfile : Profile
    {
        public SolicitacaoReparoProfile()
        {
            CreateMap<SolicitacaoReparoViewModel, Solicitacaoreparo>().ReverseMap();
        }
    }
}
