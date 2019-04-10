using MediatR;
using System.Collections.Generic;
using System.Threading;
using System.Threading.Tasks;

namespace Log.API.Features.Log.List
{
    public class LogListHandler : IRequestHandler<LogListRequest, IEnumerable<LogList>>
    {
        private readonly ILogRepository _logRepository;

        public LogListHandler(ILogRepository logRepository)
        {
            _logRepository = logRepository;
        }

        public async Task<IEnumerable<LogList>> Handle(LogListRequest request, CancellationToken cancellationToken)
        {
            return await _logRepository.GetAllLastReadByLimit(request.Limit);
        }
    }
}
