using AutoMapper;

namespace HouseHubWeb.Mappers
{
    public class AgendamentoProfile : Profile
    {
        public AgendamentoProfile()
        {
            CreateMap<Core.Agendamento, Models.AgendamentoViewModel>().ReverseMap();
        }
    }
}
