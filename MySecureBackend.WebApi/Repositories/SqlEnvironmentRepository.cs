using Dapper;
using Microsoft.Data.SqlClient;
using MySecureBackend.WebApi.Models;

namespace MySecureBackend.WebApi.Repositories
{
    public class SqlEnvironmentRepository : IEnvironmentRepository
    {
        private readonly string sqlConnectionString;

        public SqlEnvironmentRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }

        public async Task InsertAsync(EnvironmentObject environmentObject)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("INSERT INTO [Environment2D] (EnvGuid, EnvName, MaxHeight, MaxLenght, Id) " + "VALUES (@EnvGuid, @EnvName, @MaxHeight, @MaxLenght, @Id)", environmentObject);
            }
        }

        public async Task<EnvironmentObject?> SelectAsync(Guid EnvGuid)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QuerySingleOrDefaultAsync<EnvironmentObject>("SELECT * FROM [Environment2D] WHERE EnvGuid = @EnvGuid", new { EnvGuid });
            }
        }

        public async Task<IEnumerable<EnvironmentObject>> SelectAsync()
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QueryAsync<EnvironmentObject>("SELECT * FROM [Environment2D]");
            }
        }

        public async Task UpdateAsync(EnvironmentObject environmentObject)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("UPDATE [Environment2D] SET " +
                                                 "EnvName = @EnvName, " +
                                                 "MaxHeight = @MaxHeight " +
                                                 "MaxLenght = @MaxLenght " +
                                                 "Id = @Id " +
                                                 "WHERE EnvGuid = @EnvGuid", environmentObject);

            }
        }

        public async Task DeleteAsync(Guid EnvGuid)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("DELETE FROM [Environment2D] WHERE EnvGuid = @EnvGuid", new { EnvGuid });
            }
        }
    }
}
