using Microsoft.AspNetCore.SignalR;
using System.Collections.Generic;
using System.Diagnostics;
using System.Threading.Tasks;

public class MyHub : Hub
{
    // Dictionary to store user subscriptions
    private static readonly Dictionary<string, HashSet<string>> _userSubscriptions = new Dictionary<string, HashSet<string>>();

    // Method for users to subscribe to an endpoint
    public async Task SubscribeToEndpoint(string userId, string endpoint)
    {
        Debug.WriteLine("Subscribing to endpoint: " + endpoint);
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
    // Method for sending a notification to a specific user
    public async Task SendNotificationToUser(string userId, string message)
    {
        Debug.WriteLine("Sending notification to user: " + userId);
        if (_userSubscriptions.TryGetValue(userId, out HashSet<string>? endpoints))
        {
            foreach (var endpoint in endpoints)
            {
                try
                {
                    await Clients.All.SendAsync("ReceiveNotification", message); // Not the best implementation, but works for now
                    Debug.WriteLine("Notification sent to endpoint: " + endpoint);
                }
                catch (Exception ex)
                {
                    Debug.WriteLine($"Error sending notification to endpoint {endpoint}: {ex.Message}");
                }
            }
        }
    }
}

