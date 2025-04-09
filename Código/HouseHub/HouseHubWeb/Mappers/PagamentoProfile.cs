using AutoMapper;

namespace HouseHubWeb.Mappers
{
    public class PagamentoProfile : Profile
    {
        public PagamentoProfile()
        {
            CreateMap<Core.Pagamento, Models.PagamentoViewModel>().ReverseMap();
        }

    }
}
