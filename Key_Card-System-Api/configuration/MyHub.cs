using Microsoft.AspNetCore.SignalR;
using System.Diagnostics;

namespace Key_Card_System_Api.configuration
{
    public class MyHub : Hub
    {
        private static readonly Dictionary<string, HashSet<string>> _userSubscriptions = [];

        public void SubscribeToEndpoint(string userId, string endpoint)
        {
            Debug.WriteLine("Subscribing to endpoint: " + endpoint);
            if (!_userSubscriptions.TryGetValue(userId, out HashSet<string>? value))
            {
                value = [];
                _userSubscriptions[userId] = value;
            }

            value.Add(endpoint);
        }

        public void UnsubscribeFromEndpoint(string userId, string endpoint)
        {
            if (_userSubscriptions.TryGetValue(userId, out HashSet<string>? value))
            {
                value.Remove(endpoint);
            }
        }

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
}