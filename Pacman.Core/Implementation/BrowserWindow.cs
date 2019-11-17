using Microsoft.AspNetCore.Components;
using Pacman.Core.Interfaces;
using Pacman.Core.JsInterop;
using System.Drawing;

namespace Pacman.Core.Implementation
{
	public class BrowserWindow : IWindow
	{
		public int Height { get; private set; } = 790;

		public int Width { get; private set; } = 1024;

	/*	/// <summary>
		/// Available width in the current window for the main container.
		/// </summary>
		[Parameter]
		public int ElementHeight { get; set; } = 1024;

		/// <summary>
		/// Available height in the current window for the main container.
		/// </summary>
		[Parameter]
		public int ElementWidth { get; set; } = 768;*/

		public BrowserWindow()
		{
         InteropWindow.Initialized += InteropWindow_Loaded;
         InteropWindow.SizeChanged += InteropWindow_SizeChanged;
		}

		private void InteropWindow_Loaded(object sender, Rectangle e)
		{
         Width = e.Width;
         Height = e.Height;
		}

		private void InteropWindow_SizeChanged(object sender, Rectangle e)
		{
         Width = e.Width;
         Height = e.Height;
		}
	}
}
