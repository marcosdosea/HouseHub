using AutoMapper;

namespace HouseHubWeb.Mappers
{
    public class AvaliacaoProfile : Profile
    {
        public AvaliacaoProfile()
        {
            CreateMap<Core.Avaliacao, Models.AvaliacaoViewModel>().ReverseMap();
        }
    }
}
