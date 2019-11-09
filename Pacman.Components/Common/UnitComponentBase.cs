using System;
using Microsoft.AspNetCore.Components;
using Pacman.Components.Data;

namespace Pacman.Components.Common
{
	public abstract class UnitComponentBase : ComponentBase, IDisposable
	{
		[global::Microsoft.AspNetCore.Components.InjectAttribute] protected SvgHelper svgHelper { get; set; }
		[global::Microsoft.AspNetCore.Components.InjectAttribute] protected BrowserService Service { get; set; }

		public byte Size { get; set; } = 60;
		public byte Border { get; set; } = 20;
		public byte Velocity { get; set; } = 20;
		public byte TopScoreBoard { get; set; } = 100;


		public string GetStyle(Position coordinates)
		{
			return string.Format("top: {0}px; left: {1}px", coordinates.Top, coordinates.Left);
		}

		public virtual async void Move(Position coordinates, Looking direction)
		{
			var currentLeft = coordinates.Left;
			var currentTop = coordinates.Top;

			if(direction == Looking.Left)
			{
				coordinates.Top = currentTop;
				coordinates.Left = Math.Max(currentLeft - Velocity, 0);
			}
			else
			{
				if(direction == Looking.Up)
				{
					coordinates.Top = Math.Max(currentTop - Velocity, 0);
					coordinates.Left = currentLeft;
				}
				else
				{
					BrowserDimension window = new BrowserDimension();
					try
					{
						window = await Service.GetDimensions();
					}
					catch { return; }

					if(direction == Looking.Right)
					{
						coordinates.Top = currentTop;
						coordinates.Left = Math.Min(currentLeft + Velocity, window.Width - Border - Size);
					}
					else
					{
						coordinates.Top = Math.Min(currentTop + Velocity, window.Height - Size - Border - TopScoreBoard);
						coordinates.Left = currentLeft;
					}
				}
			}
		   await InvokeAsync(StateHasChanged);
		}

		public abstract void Dispose();
	}
}
