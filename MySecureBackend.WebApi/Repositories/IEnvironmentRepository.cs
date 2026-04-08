using MySecureBackend.WebApi.Models;

namespace MySecureBackend.WebApi.Repositories
{
    public interface IEnvironmentRepository
    {
        Task InsertAsync(Models.EnvironmentObject environmentObject);
        Task DeleteAsync(Guid EnvGuid);
        Task<IEnumerable<EnvironmentObject>> SelectAsync();
        Task<IEnumerable<EnvironmentObject>> SelectUserAsync(string userId);
        Task<EnvironmentObject?> SelectAsync(Guid EnvGuid);
        Task UpdateAsync(EnvironmentObject environmentObject);
    }
}