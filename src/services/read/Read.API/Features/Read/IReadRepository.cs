using Read.API.Features.Read.List;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Read.API.Features.Read
{
    public interface IReadRepository
    {
        Task<IEnumerable<ReadEpcList>> GetAllEpc();
        Task<IEnumerable<ReadList>> GetAllByLimit(int limit = 10);
    }
}
