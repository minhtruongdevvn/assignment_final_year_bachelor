using Microsoft.EntityFrameworkCore.Migrations;

#nullable disable

namespace AAM.Infrastructure.Migrations
{
    public partial class AddStoreForPredictor : Migration
    {
        protected override void Up(MigrationBuilder migrationBuilder)
        {
            var sql = @"
			IF OBJECT_ID('GetDataForTrainer', 'P') IS NOT NULL
			DROP PROC GetDataForTrainer
			GO

			CREATE PROCEDURE [dbo].[GetDataForTrainer]
			AS
			BEGIN

				DECLARE 
					@selectCols AS NVARCHAR(MAX),
					@pivotCols AS NVARCHAR(MAX),
					@query AS NVARCHAR(MAX),
					@successCode AS NVARCHAR(1) = '4',
					@failedCode AS NVARCHAR(1) = '5'

				SELECT @selectCols = STUFF((
						SELECT ',COALESCE(' + QUOTENAME([Name]) + ', 0) ' + QUOTENAME([Name]) 
						FROM [aamdb].[dbo].[Skills] 
						GROUP BY [Name], id FOR XML PATH(''), TYPE
					).value('.', 'NVARCHAR(MAX)'), 1, 1, ''
				)

				SELECT @pivotCols = STUFF((
						SELECT ',' + QUOTENAME([Name]) 
						FROM [aamdb].[dbo].[Skills] 
						GROUP BY [Name], id FOR XML PATH(''), TYPE
					).value('.', 'NVARCHAR(MAX)'), 1, 1, ''
				)

				SET @query = 
				N'SELECT
					Necessity AS necessity,
					Sex AS sex, 
					SelfDiscipline AS self_discipline, 
					Height AS height, 
					Age AS age, 
					IQ AS iq, 
					EQ AS eq, 
					Stamina AS stamina, 
					Strength AS strength, 
					ReactionTime AS reaction_time,
					NumberOfAgent AS num_agent,
					category_code - 1 AS category_code,
					COALESCE(num_success, 0) num_success,
					result, ' 
				+ @selectCols +
				N' FROM 
				(
					SELECT 
						ASkills.Score,
						Skills.[Name],
						A.Id, A.Sex , A.SelfDiscipline, A.Height, A.Age, A.IQ ,A.EQ, A.Stamina, A.Strength, A.ReactionTime, 
						Q.NumberOfAgent, Q.Id AS quest_id, Q.Necessity,
						QuestCategoryIndex.category_code,
						SuccessCount.num_success,
						CAST(IIF(Q.QuestStatus = ' + @successCode + N', 1, 0) AS BIT) AS result
					FROM [dbo].[AgentSkills] AS ASkills
					LEFT JOIN [dbo].[Skills] AS Skills ON ASkills.SKillId = Skills.Id
					LEFT JOIN [dbo].[Agents] AS A ON ASkills.AgentId = A.Id
					INNER JOIN [dbo].[AgentQuests] AS AQ ON A.Id = AQ.AgentId
					LEFT JOIN [dbo].[Quests] AS Q ON AQ.QuestId = Q.Id
					LEFT JOIN [dbo].[QuestCategories] AS QC ON Q.CategoryId= QC.Id
					LEFT JOIN 
					(
						SELECT [Id], ROW_NUMBER() OVER(ORDER BY [Name]) AS category_code
						FROM [aamdb].[dbo].[QuestCategories]
					) AS QuestCategoryIndex ON QC.Id = QuestCategoryIndex.Id
					LEFT JOIN 
					(
						SELECT AgentId, Q.CategoryId, COUNT(Q.QuestStatus) AS num_success
						FROM [dbo].[AgentQuests] AS AQ
						INNER JOIN [dbo].[Quests] AS Q ON AQ.QuestId = Q.Id AND Q.QuestStatus = ' + @successCode + N'
						GROUP BY AgentId, Q.CategoryId
					) AS SuccessCount ON ASkills.AgentId = SuccessCount.AgentId AND QC.Id = SuccessCount.CategoryId
					WHERE Q.QuestStatus = ' + @successCode + N' OR Q.QuestStatus = ' + @failedCode + N' 
				) x
				PIVOT 
				(
					MAX(Score)
					FOR [Name] IN (' + @pivotCols + N')
				) p '

				EXEC sp_executesql @query;

			END
			GO
				
			IF OBJECT_ID('GetDataForPredictor', 'P') IS NOT NULL
			DROP PROC GetDataForPredictor
			GO

			CREATE PROCEDURE [dbo].[GetDataForPredictor]
				@questId AS NVARCHAR(MAX),
				@agentId AS NVARCHAR(MAX) = NULL
			AS
			BEGIN

				DECLARE 
					@selectCols AS NVARCHAR(MAX),
					@pivotCols AS NVARCHAR(MAX),
					@query AS NVARCHAR(MAX),
					@successCode AS NVARCHAR(1) = '4',
					@failCode AS NVARCHAR(1) = '5',
					@inprogressCode AS NVARCHAR(1) = '2',
					@isSingleQuery AS BIT = 1

				IF EXISTS (
					SELECT 1 FROM [dbo].[Quests] 
					WHERE Id = @questId 
						AND (QuestStatus = @successCode OR QuestStatus = @failCode)
				)
				BEGIN
					RAISERROR('The quest is completed',18,1)
					RETURN
				END

				SELECT @isSingleQuery = 0, @agentId = NEWID() WHERE @agentId IS NULL

				SELECT @selectCols = STUFF((
						SELECT ',COALESCE(' + QUOTENAME([Name]) + ', 0) ' + QUOTENAME([Name]) 
						FROM [aamdb].[dbo].[Skills] 
						GROUP BY [Name], id FOR XML PATH(''), TYPE
					).value('.', 'NVARCHAR(MAX)'), 1, 1, ''
				)

				SELECT @pivotCols = STUFF((
						SELECT ',' + QUOTENAME([Name]) 
						FROM [aamdb].[dbo].[Skills] 
						GROUP BY [Name], id FOR XML PATH(''), TYPE
					).value('.', 'NVARCHAR(MAX)'), 1, 1, ''
				)

				SET @query = 
				N'SELECT 
					Id AS id,
					Necessity AS necessity,
					Sex AS sex, 
					SelfDiscipline AS self_discipline, 
					Height AS height, 
					Age AS age, 
					IQ AS iq, 
					EQ AS eq, 
					Stamina AS stamina, 
					Strength AS strength, 
					ReactionTime AS reaction_time,
					NumberOfAgent AS num_agent,
					category_code - 1 AS category_code,
					COALESCE(num_success, 0) num_success, ' 
				+ @selectCols +
				N' FROM 
				(
					SELECT 
						ASkills.Score,
						Skills.[Name],
						A.Id, A.Sex , A.SelfDiscipline, A.Height, A.Age, A.IQ ,A.EQ, A.Stamina, A.Strength, A.ReactionTime, 
						Q.NumberOfAgent, Q.Necessity,
						QuestCategoryIndex.category_code,
						SuccessCount.num_success
					FROM [dbo].[AgentSkills] AS ASkills
					LEFT JOIN [dbo].[Skills] AS Skills ON ASkills.SKillId = Skills.Id
					LEFT JOIN [dbo].[Agents] AS A ON ASkills.AgentId = A.Id
					LEFT JOIN [dbo].[Quests] AS Q ON ''' + @questId + N''' = Q.Id
					LEFT JOIN [dbo].[QuestCategories] AS QC ON Q.CategoryId= QC.Id
					LEFT JOIN 
					(
						SELECT [Id], ROW_NUMBER() OVER(ORDER BY [Name]) AS category_code
						FROM [aamdb].[dbo].[QuestCategories]
					) AS QuestCategoryIndex ON QC.Id = QuestCategoryIndex.Id
					LEFT JOIN 
					(
						SELECT [AgentId], COUNT(Q.QuestStatus) AS num_success
						FROM [dbo].[AgentQuests] AS AQ
						INNER JOIN [dbo].[Quests] AS Q ON AQ.QuestId = Q.Id AND Q.QuestStatus = ' + @successCode + N'
						INNER JOIN 
						(
							SELECT [CategoryId]
							FROM [dbo].[Quests] WHERE Id = ''' + @questId + N'''
						) AS QC ON QC.CategoryId = Q.CategoryId
						GROUP BY [AgentId]
					) AS SuccessCount ON ASkills.AgentId = SuccessCount.[AgentId]
					WHERE (' + CAST(@isSingleQuery AS VARCHAR) + N' = 0 OR A.Id = ''' + @agentId 
				+ N''') AND (' + CAST(@isSingleQuery AS VARCHAR) + N' = 1 OR A.Id NOT IN 
								(
									SELECT A.Id FROM dbo.Agents A 
									INNER JOIN dbo.AgentQuests AS AQ on A.Id = AQ.AgentId 
									INNER JOIN dbo.Quests AS Q ON Q.Id = AQ.QuestId
									WHERE Q.QuestStatus = ' + @inprogressCode + N' OR AQ.QuestId = ''' + @questId + N'''
								)
							) 
				) x
				PIVOT 
				(
					MAX(Score)
					FOR [Name] IN (' + @pivotCols + N')
				) p '

				EXEC sp_executesql @query;

			END
			GO

			IF OBJECT_ID('GetSkillAndMean', 'P') IS NOT NULL
			DROP PROC GetSkillAndMean
			GO

			CREATE PROCEDURE [dbo].[GetSkillAndMean]
			AS
			BEGIN

				DECLARE 
					@selectCols AS NVARCHAR(MAX),
					@pivotCols AS NVARCHAR(MAX),
					@query AS NVARCHAR(MAX),
					@agentCount AS NVARCHAR(MAX)

				SELECT @agentCount = COUNT(*) FROM [dbo].[Agents];
				SELECT @selectCols = STUFF((
						SELECT '+COALESCE(' + QUOTENAME([Id]) + ', 0) '
						FROM [aamdb].[dbo].[Agents] 
						GROUP BY [Id], id FOR XML PATH(''), TYPE
					).value('.', 'NVARCHAR(MAX)'), 1, 1, ''
				)
				SELECT @selectCols = '(' + @selectCols + N')/' + @agentCount + N' AS mean'
				SELECT @pivotCols = STUFF((
						SELECT ',' + QUOTENAME([Id]) 
						FROM [aamdb].[dbo].[Agents] 
						GROUP BY [Id], id FOR XML PATH(''), TYPE
					).value('.', 'NVARCHAR(MAX)'), 1, 1, ''
				)

				SET @query = 
				N'SELECT [Name] AS name, ' 
				+ @selectCols +
				N' FROM 
				(
					SELECT 
						ASkills.Score,
						Skills.[Name],
						A.Id
					FROM [dbo].[Skills] AS Skills
					LEFT JOIN [dbo].[AgentSkills] AS ASkills ON ASkills.SKillId = Skills.Id
					LEFT JOIN [dbo].[Agents] AS A ON ASkills.AgentId = A.Id
				) x
				PIVOT 
				(
					AVG(Score)
					FOR Id IN (' + @pivotCols + N')
				) p '


				EXEC sp_executesql @query

			END
            ";

            migrationBuilder.Sql(sql);
        }

        protected override void Down(MigrationBuilder migrationBuilder)
        {
            migrationBuilder.Sql(@"DROP PROC GetDataForPredictor");
            migrationBuilder.Sql(@"DROP PROC GetDataForTrainer");
            migrationBuilder.Sql(@"DROP PROC GetSkillAndMean");
        }
    }
}
