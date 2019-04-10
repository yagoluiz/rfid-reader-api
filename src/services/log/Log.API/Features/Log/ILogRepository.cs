using Log.API.Features.Log.List;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Log.API.Features.Log
{
    public interface ILogRepository
    {
        Task<IEnumerable<LogList>> GetAllLastReadByLimit(int limit = 10);
    }
}
