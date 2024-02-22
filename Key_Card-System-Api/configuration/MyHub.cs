using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Threading.Tasks;

public class MyHub : Hub
{
    // Dictionary to store user subscriptions
    private static readonly Dictionary<string, HashSet<string>> _userSubscriptions = new Dictionary<string, HashSet<string>>();

    // Method for users to subscribe to an endpoint
    public async Task SubscribeToEndpoint(string userId, string endpoint)
    {
        if (!_userSubscriptions.ContainsKey(userId))
        {
            _userSubscriptions[userId] = new HashSet<string>();
        }

        _userSubscriptions[userId].Add(endpoint);
    }

    // Method for users to unsubscribe from an endpoint
    public async Task UnsubscribeFromEndpoint(string userId, string endpoint)
    {
        if (_userSubscriptions.ContainsKey(userId))
        {
            _userSubscriptions[userId].Remove(endpoint);
        }
    }

    // Method for sending a notification to a specific user
    public async Task SendNotificationToUser(string userId, string message)
    {
        if (_userSubscriptions.ContainsKey(userId))
        {
            foreach (var endpoint in _userSubscriptions[userId])
            {
                await Clients.Client(endpoint).SendAsync("ReceiveNotification", message);
            }
        }
    }
}

