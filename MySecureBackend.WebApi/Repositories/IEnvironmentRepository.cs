using MySecureBackend.WebApi.Models;

namespace MySecureBackend.WebApi.Repositories
{
    public interface IEnvironmentRepository
    {
        Task InsertAsync(EnvironmentObject environmentObject);
        Task DeleteAsync(Guid EnvGuid);
        Task<IEnumerable<EnvironmentObject>> SelectAsync();
        Task<EnvironmentObject?> SelectAsync(Guid EnvGuid);
        Task UpdateAsync(EnvironmentObject environmentObject);
    }
}