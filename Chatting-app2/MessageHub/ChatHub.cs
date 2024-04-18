using Microsoft.AspNetCore.SignalR;

namespace Chatting_app2.MessageHub
{
    public class ChatHub : Hub
    {
        public async Task Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            await Clients.All.SendAsync("ReceiveOne", name, message);
            //others also can be used  
        }
    }
}