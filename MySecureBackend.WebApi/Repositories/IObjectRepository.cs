using MySecureBackend.WebApi.Models;

namespace MySecureBackend.WebApi.Repositories
{
    public interface IObjectRepository
    {
        Task InsertAsync(Models.ObjectRepo objectRepo);
        Task DeleteAsync(Guid ObjGuid);
        Task DeleteEnvAsync(Guid EnvGuid);
        Task<IEnumerable<ObjectRepo>> SelectAsync();
        Task<ObjectRepo?> SelectAsync(Guid ObjGuid);
        Task UpdateAsync(ObjectRepo objectRepo);
        Task<IEnumerable<ObjectRepo>> SelectByEnvironmentAsync(Guid EnvGuid);
    }
}