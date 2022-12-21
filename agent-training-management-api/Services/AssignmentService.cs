using Flurl;
using Flurl.Http;
using System.Text;
using System;

namespace AtmAPI.Services;

public class AssignmentService
{
	private readonly AccessTokenProvider _accessTokenProvider;

	public AssignmentService(
		AccessTokenProvider accessTokenProvider
	)
	{
		_accessTokenProvider = accessTokenProvider;
	}

	public async Task CreateAgentAsync(AgentUpsertRequest request)
	{
		await HandleRequestAsync(
			$"create/agent/ID_REF:{request.IdentityReference}",
			request: (token) =>
				Flurl("agents")
					.WithHeader("Content-Type", "application/json")
					.WithOAuthBearerToken(token)
					.PostJsonAsync(request)
					.ReceiveJson()
		);
	}

	public async Task UpdateAgentAsync(AgentUpsertRequest request)
	{
		await HandleRequestAsync(
			$"update/agent/ID_REF:{request.IdentityReference}",
			request: (token) =>
				Flurl($"agents")
					.WithHeader("Content-Type", "application/json")
					.WithOAuthBearerToken(token)
					.PutJsonAsync(request)
					.ReceiveJson()
		);
	}

	public async Task DeleteAgentAsync(int id)
	{
		await HandleRequestAsync(
			$"delete/agent/ID_REF:{id}",
			request: (token) =>
				Flurl($"agents/{id}").WithOAuthBearerToken(token).DeleteAsync().ReceiveJson()
		);
	}

	public async Task CreateSkillAsync(AAMSkillUpsertRequest request)
	{
		await HandleRequestAsync(
			$"create/skill/NAME:{request.Name}",
			request: (token) =>
				Flurl("skills")
					.WithHeader("Content-Type", "application/json")
					.WithOAuthBearerToken(token)
					.PostJsonAsync(request)
					.ReceiveJson()
		);
	}

	public async Task UpdateSkillAsync(AAMSkillUpsertRequest request)
	{
		await HandleRequestAsync(
			$"update/skill/NAME:{request.Name}",
			request: (token) =>
				Flurl("skills")
					.WithHeader("Content-Type", "application/json")
					.WithOAuthBearerToken(token)
					.PutJsonAsync(request)
					.ReceiveJson()
		);
	}

	public async Task DeleteSkillAsync(string skillName)
	{
		await HandleRequestAsync(
			$"delete/skill/NAME:skillName",
			request: (token) =>
				Flurl($"skills/{skillName}").WithOAuthBearerToken(token).DeleteAsync().ReceiveJson()
		);
	}

	public async Task UpsertAgentSkillAsync(AgentSkillUpsertRequest request)
	{
		await HandleRequestAsync(
			$"upsert/agent/skill/REF_NAME:{request.IdentityReference}_{request.SkillName}",
			request: (token) =>
				Flurl("agents/skills")
					.WithHeader("Content-Type", "application/json")
					.WithOAuthBearerToken(token)
					.PostJsonAsync(request)
					.ReceiveJson()
		);
	}

	private static IFlurlRequest Flurl(string appendPath) =>
		new Url(ExternalService.Assignment.Endpoint).AppendPathSegment(appendPath).WithTimeout(60);

	private async Task HandleRequestAsync(string subject, Func<string?, Task> request)
	{
		try
		{
			await request(await _accessTokenProvider.GetTokenAsync());
		}
		catch (FlurlHttpException ex)
		{
			var response = await ex.Call.Response.GetStringAsync();
			var errContent = string.IsNullOrEmpty(response) ? "None" : response;
			var message = string.IsNullOrEmpty(ex.Message) ? "None" : ex.Message;

			var errMessageBuilder = new StringBuilder();
			errMessageBuilder.AppendLine($"Assignment service sync failed: {subject}");
			errMessageBuilder.AppendLine($"Status: {ex.StatusCode} ({(HttpStatusCode)ex.Call.Response.StatusCode})");
			errMessageBuilder.AppendLine("Message:");
			errMessageBuilder.AppendLine(message);
			errMessageBuilder.AppendLine("Content:");
			errMessageBuilder.AppendLine(errContent);

			LoggerHelpers.Logger.Error(errMessageBuilder.ToString());
		}
	}
}
