using AutoMapper;
using Read.API.ViewModels;
using Read.Domain.Models;

namespace Read.API.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<ReadViewModel, ReadModel>();
        }
    }
}
