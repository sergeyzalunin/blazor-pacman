using Pacman.Data;
using System;
using System.Threading.Tasks;

namespace Pacman.Common 
{
	public abstract class UnitComponentBase : Microsoft.AspNetCore.Components.ComponentBase
	{
		[global::Microsoft.AspNetCore.Components.InjectAttribute] protected SvgHelper svgHelper { get; set; }
		[global::Microsoft.AspNetCore.Components.InjectAttribute] protected BrowserService Service { get; set; }	
		 
		public byte Size { get; set; } = 60;
		public byte Border { get; set; } = 20;
		public byte Velocity { get; set; } = 20;
		public byte TopScoreBoard { get; set; } = 100;
		
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
					var window = await Service.GetDimensions();

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
		}
	}
}
