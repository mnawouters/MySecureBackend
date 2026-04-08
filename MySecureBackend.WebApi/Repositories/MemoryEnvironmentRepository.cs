using MySecureBackend.WebApi.Models;

namespace MySecureBackend.WebApi.Repositories
{
    public class MemoryExampleObjectRepository : IEnvironmentRepository
    {
        private static List<EnvironmentObject> Environments = [];

        public Task DeleteAsync(Guid envGuid)
        {
            Environments.Remove(Environments.Single(x => x.EnvGuid == envGuid));
            return Task.CompletedTask;
        }

        public Task InsertAsync(EnvironmentObject environmentObject)
        {
            Environments.Add(environmentObject);
            return Task.CompletedTask;
        }

        public Task<IEnumerable<EnvironmentObject>> SelectAsync()
        {
            return Task.FromResult(Environments.AsEnumerable());
        }

        public Task<EnvironmentObject?> SelectAsync(Guid envGuid)
        {
            return Task.FromResult(Environments.SingleOrDefault(x => x.EnvGuid == envGuid));
        }

        public Task<IEnumerable<EnvironmentObject>> SelectUserAsync(string userIdString)
        {
            throw new NotImplementedException();
        }

        public async Task UpdateAsync(ExampleObject exampleObject)
        {
            await DeleteAsync(exampleObject.Id);
            await InsertAsync(exampleObject);
        }
    }
}
