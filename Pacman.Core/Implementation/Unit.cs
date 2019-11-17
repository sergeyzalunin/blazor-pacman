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
		
		public byte Size { get; set; } = 60;
		public byte Border { get; set; } = 20;
		public byte Velocity { get; set; } = 20;
		public byte TopScoreBoard { get; set; } = 100;

		public event EventHandler OnMoving;

		public abstract Task OnInitializedAsync();

		public abstract Task OnAfterRenderAsync(bool firstRender);

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

			 Moving();
		}
		 
      protected virtual void Moving()
      {  
         OnMoving?.Invoke(this, EventArgs.Empty);  
      }  

		public void OnCollide()
		{
			throw new NotImplementedException();
		}

		public abstract void Dispose();
	}
}
