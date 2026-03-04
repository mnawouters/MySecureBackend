using MySecureBackend.WebApi.Models;

namespace MySecureBackend.WebApi.Repositories
{
    public interface IObjectRepository
    {
        Task InsertAsync(ObjectRepo objectRepo);
        Task DeleteAsync(Guid ObjGuid);
        Task<IEnumerable<ObjectRepo>> SelectAsync();
        Task<ObjectRepo?> SelectAsync(Guid ObjGuid);
        Task UpdateAsync(ObjectRepo objectRepo);
    }
}