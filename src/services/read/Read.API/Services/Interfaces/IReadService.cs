using Read.API.Common;
using System.Threading.Tasks;

namespace Read.API.Services.Interfaces
{
    public interface IReadService
    {
        Task<ResponseCommon> GetAllReadAsync();
    }
}
