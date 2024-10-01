using Microsoft.AspNetCore.SignalR;

namespace Infrastructures.SignalR
{
    public class SignalRHub : Hub
    {
        // Send notifcation, receiver based on broadcast
        public async Task SendNotification(string broadcast, string title, string author, string message)
        {
            await Clients.All.SendAsync(broadcast, title, author, message);
        }
        // Send Notification to roles who are online
        public async Task SendNotificationManager(string title, string author, string message)
        {
            await Clients.All.SendAsync("Manager", title, author, message);
        }
        public async Task SendNotificationStaff(string title, string author, string message)
        {
            await Clients.All.SendAsync("Staff", title, author, message);
        }
        public async Task SendNotificationTranslator(string title, string author, string message)
        {
            await Clients.All.SendAsync("Translator", title, author, message);
        }
        public async Task SendNotificationShipper(string title, string author, string message)
        {
            await Clients.All.SendAsync("Shipper", title, author, message);
        }
    }
}
