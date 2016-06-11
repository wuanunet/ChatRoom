using Microsoft.AspNet.SignalR;

namespace Chat.Web.Hubs
{
    public class ChatHub : Hub
    {
        public void SendMessage(string nickName, string message)
        {
            Clients.All.gotMessage(nickName, message);
        }
    }
}