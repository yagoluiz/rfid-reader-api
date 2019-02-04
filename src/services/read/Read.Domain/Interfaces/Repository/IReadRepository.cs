using Read.Domain.Models;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Read.Domain.Interfaces.Repository
{
    public interface IReadRepository
    {
        Task<IEnumerable<ReadModel>> GetAllReadAsync();
    }
}
