using ca.HenrySoftware;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;
using System.Diagnostics;
namespace NewTestChat.Pages
{
	public class IndexModel : PageModel
	{
		private readonly ILogger _logger;
		public IndexModel(ILogger<IndexModel> logger)
		{
			_logger = logger;
		}
		private Gename _gename = new Gename();
		[ViewData]
		public string Name => _gename.Name();
		public void OnGet()
		{
			var name = Name;
			_logger.LogInformation("Message displayed: {Message}", name);
			Debug.Print(name);
		}
	}
}
