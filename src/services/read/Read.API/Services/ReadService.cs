using AutoMapper;
using Read.API.Common;
using Read.API.Services.Interfaces;
using Read.API.ViewModels;
using Read.Domain.Interfaces.Repository;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Read.API.Services
{
    public class ReadService : IReadService
    {
        private readonly IReadRepository _readRepository;
        private readonly IMapper _mapper;

        public ReadService(IReadRepository readRepository, IMapper mapper)
        {
            _readRepository = readRepository;
            _mapper = mapper;
        }

        public async Task<ResponseCommon> GetAllReadAsync()
        {
            var model = _mapper.Map<IEnumerable<ReadViewModel>>(await _readRepository.GetAllReadAsync());
            return new ResponseCommon(model);
        }
    }
}
