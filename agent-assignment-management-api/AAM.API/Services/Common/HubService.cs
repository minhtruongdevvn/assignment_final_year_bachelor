using AAM.API.Authorization;
using AAM.Application;
using Microsoft.AspNetCore.SignalR;
using System.Security.Claims;

namespace AAM.API;

[AgentAppAuthorization()]
public class HubService : Hub
{
    public HubService() { 
    }

    public override async Task OnConnectedAsync()
    {       
        string userId = Context.User!.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
        string connectionId = Context.ConnectionId;
        await Groups.AddToGroupAsync(connectionId, userId);
        await base.OnConnectedAsync();
    }

    public override async Task OnDisconnectedAsync(Exception? exception)
    {
        string userId = Context.User!.Claims.First(x => x.Type == ClaimTypes.NameIdentifier).Value;
        string connectionId = Context.ConnectionId;
        await Groups.RemoveFromGroupAsync(connectionId, userId!);
        await base.OnDisconnectedAsync(exception);
    }
}

static class HubAction 
{
    public const string Add = "AgentQuestAdd";
    public const string Delete = "AgentQuestDelete";
    public const string Update = "AgentQuestUpdate";
}
