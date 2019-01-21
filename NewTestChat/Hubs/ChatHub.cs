using Microsoft.AspNetCore.SignalR;
using System.Threading.Tasks;
using ca.HenrySoftware;
using System.Diagnostics;
namespace NewTestChat
{
	public class ChatHub : Hub
	{
		private Gename _gename = new Gename();
		public async Task SendMessage(string user, string message)
		{
			var name = _gename.Name();
			Debug.Print(name);
			await Clients.All.SendAsync("ReceiveMessage", user, message);
		}
	}
}
