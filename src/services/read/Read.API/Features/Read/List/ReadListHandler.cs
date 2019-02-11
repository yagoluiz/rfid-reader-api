using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Read.API.Features.Read.List
{
    public class ReadListHandler : IRequestHandler<ReadListRequest, IEnumerable<ReadList>>
    {
        private readonly IReadRepository _readRepository;

        public ReadListHandler(IReadRepository readRepository)
        {
            _readRepository = readRepository;
        }

        public async Task<IEnumerable<ReadList>> Handle(ReadListRequest request, CancellationToken cancellationToken)
        {
            return await _readRepository.GetAllByLimit(request.Limit);
        }
    }
}
