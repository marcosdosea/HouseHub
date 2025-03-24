using AutoMapper;
using Core;
using HouseHubWeb.Models;

namespace HouseHubWeb.Mappers
{
    public class SolicitacaoReparoProfile : Profile
    {
        public SolicitacaoReparoProfile()
        {
            // Mapeamento de Solicitacaoreparo para SolicitacaoReparoViewModel
            CreateMap<Solicitacaoreparo, SolicitacaoReparoViewModel>()
                // Converter sbyte EnviarAlguem para bool
                .ForMember(dest => dest.EnviarAlguem, opt => opt.MapFrom(src => src.EnviarAlguem != 0))
                // Configurar mapeamento para IdLocacaoNavigation
                .ForMember(dest => dest.IdLocacaoNavigation, opt => opt.MapFrom(src => src.IdLocacaoNavigation));

            // Mapeamento de SolicitacaoReparoViewModel para Solicitacaoreparo
            CreateMap<SolicitacaoReparoViewModel, Solicitacaoreparo>()
                // Converter bool EnviarAlguem para sbyte
                .ForMember(dest => dest.EnviarAlguem, opt => opt.MapFrom(src => src.EnviarAlguem ? (sbyte)1 : (sbyte)0));

            // Adicione também o mapeamento para Locacao e LocacaoViewModel
            CreateMap<Locacao, LocacaoViewModel>();
            CreateMap<LocacaoViewModel, Locacao>();
        }
    }
}
