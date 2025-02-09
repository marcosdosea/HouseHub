using AutoMapper;

namespace HouseHubWeb.Mappers
{
    public class PessoaProfile : Profile
    {
        public PessoaProfile()
        {
            CreateMap<Core.Pessoa, Models.PessoaViewModel>().ReverseMap();
            CreateMap<Core.DTOs.PessoaDto, Models.PessoaViewModel>().ReverseMap();
            CreateMap<Core.DTOs.PessoaDto, Core.Pessoa>().ReverseMap();
        }
    }
}
