

using CarService.DataAccess;

using Microsoft.AspNetCore.SignalR;

using Microsoft.EntityFrameworkCore;

namespace CarService.WebApi.Hubs

{

    public class MessageHub : Hub

    {

        // "role:username" → connectionId

        private static Dictionary<string, string> ConnectedUsers = new();

        private readonly AppDataContext _context;

        public MessageHub(AppDataContext context)

        {

            _context = context;

        }

        public override async Task OnConnectedAsync()

        {

            var httpContext = Context.GetHttpContext();

            var username = httpContext?.Request.Query["username"].ToString();

            var role = httpContext?.Request.Query["role"].ToString();

            if (!string.IsNullOrEmpty(username) && !string.IsNullOrEmpty(role))

            {

                string userKey = $"{role}:{username}";

                ConnectedUsers[userKey] = Context.ConnectionId;

                int userId = await GetUserIdFromDb(username, role);

                await Clients.All.SendAsync("ReceiveConnectInfo", $"{username} ({role}, ID: {userId}) qoşuldu ✅");

            }

            await base.OnConnectedAsync();

        }

        public override async Task OnDisconnectedAsync(Exception? exception)

        {

            var currentConnectionId = Context.ConnectionId;

            var user = ConnectedUsers.FirstOrDefault(x => x.Value == currentConnectionId);

            if (!string.IsNullOrEmpty(user.Key))

            {

                ConnectedUsers.Remove(user.Key);

                await Clients.All.SendAsync("ReceiveDisconnectInfo", $"{user.Key} ayrıldı ❌");

            }

            await base.OnDisconnectedAsync(exception);

        }

        // username + role əsaslı mesaj göndərmək

        public async Task SendPrivateMessage(string fromKey, string toKey, string message)

        {

            if (ConnectedUsers.TryGetValue(toKey, out var targetConnId))

            {

                await Clients.Client(targetConnId)

                    .SendAsync("ReceivePrivateMessage", fromKey, message);

            }

            if (ConnectedUsers.TryGetValue(fromKey, out var senderConnId))

            {

                await Clients.Client(senderConnId)

                    .SendAsync("ReceivePrivateMessage", fromKey, message);

            }

        }

        // ID bazadan çəkilir, sadəcə info üçün

        private async Task<int> GetUserIdFromDb(string username, string role)

        {

            if (role == "user")

            {

                var user = await _context.Users

                    .Where(u => u.Username == username)

                    .Select(u => (int?)u.Id)

                    .FirstOrDefaultAsync();

                return user ?? 0;

            }

            else if (role == "admin")

            {

                var admin = await _context.Admins

                    .Where(a => a.Username == username)

                    .Select(a => (int?)a.Id)

                    .FirstOrDefaultAsync();

                return admin ?? 0;

            }

            return 0;

        }

    }

}

