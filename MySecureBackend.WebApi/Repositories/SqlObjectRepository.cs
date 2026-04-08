using Dapper;
using Microsoft.Data.SqlClient;
using MySecureBackend.WebApi.Models;

namespace MySecureBackend.WebApi.Repositories
{
    public class SqlObjectRepository : IObjectRepository
    {
        private readonly string sqlConnectionString;

        public SqlObjectRepository(string sqlConnectionString)
        {
            this.sqlConnectionString = sqlConnectionString;
        }

        public async Task InsertAsync(ObjectRepo objectRepo)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("INSERT INTO [Object] (ObjGuid, ObjName, PrefabId, PositionX, PositionY, ScaleX, ScaleY, RotationZ, SortingLayer, EnvGuid) VALUES (@ObjGuid, @ObjName, @PrefabId, @PositionX, @PositionY, @ScaleX, @ScaleY, @RotationZ, @SortingLayer, @EnvGuid)", objectRepo);
            }
        }

        public async Task<ObjectRepo?> SelectAsync(Guid ObjGuid)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QuerySingleOrDefaultAsync<ObjectRepo>("SELECT * FROM [Object] WHERE ObjGuid = @ObjGuid", new { ObjGuid });
            }
        }

        public async Task<IEnumerable<ObjectRepo>> SelectAsync()
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QueryAsync<ObjectRepo>("SELECT * FROM [Object]");
            }
        }

        public async Task UpdateAsync(ObjectRepo objectRepo)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("UPDATE [Object] SET " +
                                                 "ObjName = @ObjName, " +
                                                 "PrefabId = @PrefabId " +
                                                 "PositionX = @PositionX " +
                                                 "PositionY = @PositionY " +
                                                 "ScaleX = @ScaleX " +
                                                 "ScaleY = @ScaleY " +
                                                 "RotationZ = @RotationZ " +
                                                 "SortingLayer = @SortingLayer " +
                                                 "EnvGuid = @EnvGuid " +
                                                 "WHERE ObjGuid = @ObjGuid", objectRepo);

            }
        }

        public async Task<IEnumerable<ObjectRepo>> SelectByEnvironmentAsync(Guid EnvGuid)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                return await sqlConnection.QueryAsync<ObjectRepo>("SELECT * FROM [Object] WHERE EnvGuid = @EnvGuid", new { EnvGuid });
            }
        }

        public async Task DeleteAsync(Guid ObjGuid)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("DELETE FROM [Object] WHERE ObjGuid = @ObjGuid", new { ObjGuid });
            }
        }

        public async Task DeleteEnvAsync(Guid EnvGuid)
        {
            using (var sqlConnection = new SqlConnection(sqlConnectionString))
            {
                await sqlConnection.ExecuteAsync("DELETE FROM [Object] WHERE EnvGuid = @EnvGuid", new { EnvGuid });
            }
        }
    }
}