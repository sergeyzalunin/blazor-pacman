﻿using Microsoft.JSInterop;
using System.Threading.Tasks;

namespace Pacman.Data
{
	public class BrowserService
	{
        private readonly IJSRuntime _js;

        public BrowserService(IJSRuntime js)
        {
            _js = js;
        }

        public async Task<BrowserDimension> GetDimensions()
        {
            return await _js.InvokeAsync<BrowserDimension>("getDimensions");
        }
	}
}