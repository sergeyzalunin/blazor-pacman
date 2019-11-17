using Pacman.Core.Data;
using Pacman.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace Pacman.Core.Implementation
{
	public abstract class Unit : IUnit, IDisposable
	{
		public SvgHelper SvgHelper { get; set; }
		public BrowserService Service { get; set; }
		
		public byte Size { get; set; } = Constants.Size;
		public byte Border { get; set; } = Constants.Border;
		public byte Velocity { get; set; } = Constants.Velocity;
		public byte TopScoreBoard { get; set; } = Constants.TopScoreBoard;

		public event EventHandler OnStateHasChanged;

		public abstract Task OnInitializedAsync();

		public abstract Task OnAfterRenderAsync();
		public abstract void KillUnit();

		public Unit(SvgHelper svgHelper, BrowserService service)
		{
			this.SvgHelper = svgHelper;
			this.Service = service;
		}

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
				coordinates.Left = Math.Max(currentLeft - this.Velocity, 0);
			}
			else
			{
				if(direction == Looking.Up)
				{
					coordinates.Top = Math.Max(currentTop - this.Velocity, this.TopScoreBoard);
					coordinates.Left = currentLeft;
				}
				else
				{
					BrowserDimension window = await GetBrowserDimensionAsync();

					if(direction == Looking.Right)
					{
						coordinates.Top = currentTop;
						coordinates.Left = Math.Min(currentLeft + this.Velocity, window.Width - this.Size - this.Border);
					}
					else
					{
						coordinates.Top = Math.Min(currentTop + this.Velocity, window.Height - this.Size - this.Border);
						coordinates.Left = currentLeft;
					}
				}
			}

			StateHasChanging();
		}

		public async Task<BrowserDimension> GetBrowserDimensionAsync()
		{
			BrowserDimension window;
			try
			{
				window = await Service.GetDimensions();
			}
			catch //TODO: Try to find better way to handle this
			{
				window = new BrowserDimension();
			}
			return window;
		}
		 
      protected virtual void StateHasChanging()
      {  
         OnStateHasChanged?.Invoke(this, EventArgs.Empty);  
      }  

		public abstract void Dispose();
	}
}
