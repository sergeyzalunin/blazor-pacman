using Microsoft.JSInterop;
using System;
using System.Threading;
using System.Threading.Tasks;

namespace Pacman.Core.Data
{
	public class BrowserService: IDisposable
	{
		private readonly IJSRuntime js;
		private CancellationTokenSource tokenSource;

		public BrowserService(IJSRuntime js)
		{
			tokenSource = new CancellationTokenSource();
			this.js = js;
		}

		public async Task<BrowserDimension> GetDimensions()
		{
			return await js.InvokeAsync<BrowserDimension>("getDimensions", tokenSource.Token);
		}

		public void Dispose()
		{
			tokenSource.Cancel();
		}
	}
}
