using AutoMapper;
using Monitoring.API.ViewModels;
using Monitoring.Domain.Models;

namespace Monitoring.API.AutoMapper
{
    public class ViewModelToDomainMappingProfile : Profile
    {
        public ViewModelToDomainMappingProfile()
        {
            CreateMap<TelemetryViewModel, TelemetryModel>();
        }
    }
}
