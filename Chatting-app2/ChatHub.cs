using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using com.sun.security.ntlm;

namespace Chatting_app2
{
    public class ChatHub : Hub
    {
        public async Task Send(string name, string message)
        {
            // Call the broadcastMessage method to update clients.
            await Clients.All.SendAsync("broadcastMessage", name, message);
           //others also can be used  
        }
    }
}
