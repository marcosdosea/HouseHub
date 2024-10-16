using AutoMapper;

namespace HouseHubWeb.Mappers
{
    public class PagamentoViewModel : Profile
    {
        public PagamentoViewModel()
        {
            CreateMap<Core.Pagamento, Models.PagamentoViewModel>().ReverseMap();
        }

    }
}
