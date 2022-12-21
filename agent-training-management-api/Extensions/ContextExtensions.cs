using Dapper;
using Microsoft.Data.SqlClient;

namespace AtmAPI.Extensions;

public static class ContextExtensions
{
	public static async Task<int> GetTotalRowAsync<TEntity>(this AtmContext context)
		where TEntity : class
	{
		var entityType = typeof(TEntity);
		var tableName = context.Model.FindEntityType(entityType)?.GetSchemaQualifiedTableName();

		tableName.ThrowIfNull("Table name").IfEmpty();

		await using var connection = new SqlConnection(AppSettings.DbConnectionString);
		var result = await connection.QueryFirstOrDefaultAsync<int>(
			@$"SELECT SUM(row_count)
				FROM sys.dm_db_partition_stats
				WHERE object_id = OBJECT_ID('{tableName}')
				AND index_id < 2
				GROUP BY OBJECT_NAME(object_id)"
		);
		return result;
	}
}
