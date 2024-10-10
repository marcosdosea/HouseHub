using AutoMapper;

namespace HouseHubWeb.Mappers
{
    public class PessoaProfile : Profile
    {
        public PessoaProfile()
        {
            CreateMap<Core.Pessoa, Models.PessoaViewModel>().ReverseMap();
        }
    }
}
