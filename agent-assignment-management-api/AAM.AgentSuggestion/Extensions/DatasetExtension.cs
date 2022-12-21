using AAM.AgentSuggestion.Entities;
using Dapper;
using Microsoft.Data.Analysis;
using Microsoft.Data.SqlClient;
using System.Data;
using System.Text;

namespace AAM.AgentSuggestion;

public static class DatasetExtension
{
    public static async Task<List<MeanOfFeature>> GetSkillAsync(string connectionString)
    {
        var connection = new SqlConnection(connectionString);
        var skills = await connection.QueryAsync<MeanOfFeature>(
            "EXEC [dbo].GetSkillAndMean"
        );
        return skills.ToList();
    }

    public static async Task<DataFrame> GetDataFromDBAsync(
        IEnumerable<MeanOfFeature> props,
        string connectionString,
        string storeName,
        string? lableName = null,
        object? param = null
    )
    {
        var connection = new SqlConnection(connectionString);
        var records = await connection.QueryAsync<object>(
            storeName,
            param: param,
            commandType: CommandType.StoredProcedure
        );

        var csv = new StringBuilder();
        csv.Append(string.Join(",", props.Select(x => x.Name)));
        csv.AppendLine();
        foreach (var record in records)
        {
            var recordDic = (IDictionary<string, object>)record!;
            var row = "";
            foreach (var prop in props)
            {
                var propValue = recordDic[prop.Name] ?? prop.Mean;
                if (propValue is bool && prop.Name != lableName)
                {
                    var value = (bool)propValue ? 1 : 0;
                    row = $"{row},{value}";
                }
                else
                {
                    row = $"{row},{propValue}";
                }
            }
            row = row.Remove(0, 1); // remove first comma
            csv.AppendLine(row);
        }

        return DataFrame.LoadCsvFromString(csv.ToString());
    }
}
