using Microsoft.AspNet.SignalR.Client;

namespace Chat.Tail
{
    internal class Program
    {
        private static void Main(string[] args)
        {
            var connection = new HubConnection("http://localhost:26836/");
            var chatHub = connection.CreateHubProxy("ChatHub");

            chatHub.On<string, string>("gotMessage", (who, message) =>
            {
                System.Console.WriteLine(who + "：" + message);
            });

            connection.Start();

            System.Console.ReadKey();
        }
    }
}