using CarService.DataAccess;
using Microsoft.AspNetCore.SignalR;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace CarService.WebApi.Hubs

{

    public class MessageHub : Hub
    {
        private static Dictionary<string, int> ConnectedUsers = new();

        private readonly AppDataContext _context;

        public MessageHub(AppDataContext context)
        {
            _context = context;
        }

        public override async Task OnConnectedAsync()
        {

            var username = Context.GetHttpContext()?.Request.Query["username"].ToString();

            if (!string.IsNullOrEmpty(username))
            {

                int userId = await GetUserIdFromDb(username);

                if (userId > 0)
                {
                    ConnectedUsers[Context.ConnectionId] = userId;
                    await Clients.All.SendAsync("ReceiveConnectInfo", $"{username} (ID: {userId}) qoşuldu ✅");
                }

            }

            await base.OnConnectedAsync();

        }

        public override async Task OnDisconnectedAsync(Exception? exception)
        {

            if (ConnectedUsers.TryGetValue(Context.ConnectionId, out var userId))
            {

                ConnectedUsers.Remove(Context.ConnectionId);

                await Clients.All.SendAsync("ReceiveDisconnectInfo", $"ID {userId} ayrıldı ❌");

            }

            await base.OnDisconnectedAsync(exception);

        }

        public async Task SendPrivateMessage(int fromId, int toId, string message)
        {

            var targetConnectionId = ConnectedUsers
                .FirstOrDefault(x => x.Value == toId).Key;

            if (!string.IsNullOrEmpty(targetConnectionId))
            {
                await Clients.Client(targetConnectionId)

                    .SendAsync("ReceivePrivateMessage", fromId, message);
            }

        }

        private async Task<int> GetUserIdFromDb(string username)
        {
            var user = await _context.Users
                .Where(u => u.Username == username)
                .Select(u => (int?)u.Id)
                .FirstOrDefaultAsync();

            if (user.HasValue)
                return user.Value;

            var admin = await _context.Admins
                .Where(a => a.Username == username)
                .Select(a => (int?)a.Id)
                .FirstOrDefaultAsync();

            return admin ?? 0;
        }

    }

}


