using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using ca.HenrySoftware;
using System.Diagnostics;
namespace NewTestChat
{
	public class ChatHub : Hub
	{
		public async Task SendMessage(string user, string message)
		{
			var g = new Gename();
			var name = g.Name();
			Debug.Print(name);
			await Clients.All.SendAsync("ReceiveMessage", user, message);
		}
	}
}
