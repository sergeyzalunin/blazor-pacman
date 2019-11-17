using Pacman.Core.Common;
using Pacman.Core.Data;
using Pacman.Core.Interfaces;
using Pacman.Core.JsInterop;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace Pacman.Core.Implementation
{
	public class Pacman : Unit, IPacman
	{
		Looking interimDirection = Looking.Right;
		public Looking Direction { get; set; } = Looking.Right;
		public Position Coordinates { get; set; } = new Position { Top = Constants.TopScoreBoard, Left = 100 };

		Timer moveTimer;
		
		public Pacman(SvgHelper svgHelper, BrowserService service): base(svgHelper, service)
		{
		}

		public async override Task OnInitializedAsync()
		{
			await SvgHelper.LoadIconsAsync();
		}

		public async override Task OnAfterRenderAsync()
		{
			await Task.Run(() =>
			{
				InteropKeyPress.KeyDown += HandleKeyDown;
				moveTimer = new Timer(Move, Constants.MoveSpeed);
			});
		}

		private void Move()
		{
			this.Direction = interimDirection;
			Move(this.Coordinates, this.Direction);
		}

		public string GetDirectionClassName()
		{
			string result;

			switch(Direction)
			{
				case Looking.Up: result = "up"; break;
				case Looking.Left: result = "left"; break;
				case Looking.Right: result = "right"; break;
				default: result = "down"; break;
			}

			return result;
		}

		private void HandleKeyDown(object sender, ConsoleKey keyCode)
		{
			ConsoleKey[] arrows = new ConsoleKey[] 
			{ 
				ConsoleKey.NumPad5, 
				ConsoleKey.NumPad1, 
				ConsoleKey.NumPad2, 
				ConsoleKey.NumPad3,
				ConsoleKey.LeftArrow,
				ConsoleKey.UpArrow,
				ConsoleKey.RightArrow,
				ConsoleKey.DownArrow
			};

			if(arrows.Contains(keyCode))
			{
				this.Rotate(keyCode);
			}
		}

		private void Rotate(ConsoleKey keyCode)
		{
			switch(keyCode)
			{
				case ConsoleKey.NumPad3: case ConsoleKey.RightArrow: this.interimDirection = Looking.Right; break;
				case ConsoleKey.NumPad1: case ConsoleKey.LeftArrow: this.interimDirection = Looking.Left; break;
				case ConsoleKey.NumPad5: case ConsoleKey.UpArrow: this.interimDirection = Looking.Up; break;
				default: this.interimDirection = Looking.Down; break;
			}
		}

		public override void KillUnit()
		{
			moveTimer.StopTimer();
		}

		public override void Dispose()
		{
			if(moveTimer != null) moveTimer.Dispose();
		}
	}
}
