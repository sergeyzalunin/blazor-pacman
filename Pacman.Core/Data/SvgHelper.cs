using Microsoft.AspNetCore.Components;
using System.Net.Http;
using System.Threading.Tasks;

namespace Pacman.Core.Data
{
	public class SvgHelper
	{
		private readonly HttpClient httpClient;

		public MarkupString Ghost { get; set; }
		public MarkupString Packman { get; set; }

		public SvgHelper(HttpClient httpClient)
		{
			this.httpClient = httpClient;
		}

		public async Task LoadIconsAsync()
		{
			Ghost = new MarkupString(await httpClient.GetStringAsync(@"Assets/Images/ghost.svg"));
			Packman = new MarkupString(await httpClient.GetStringAsync(@"Assets/Images/pacman.svg"));
		}
	}
}
