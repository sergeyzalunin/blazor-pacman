using Pacman.Core.Common;
using Pacman.Core.Data;
using Pacman.Core.Interfaces;
using System;
using System.Threading.Tasks;

namespace Pacman.Core.Implementation
{
	public class Ghost : Unit, IGhost
	{
		public string ColorName { get; set; } = "red";
		public Looking Direction { get; set; } = Looking.Left;
		public Position Coordinates { get; set; } = new Position { Top = 300, Left = 300 };

		Timer moveTimer, changeDirectionTimer;

		public Ghost(SvgHelper svgHelper, BrowserService service): base(svgHelper, service)
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
				moveTimer = new Timer(Move, Constants.MoveSpeed);
				changeDirectionTimer = new Timer(ChangeDirection, 500);
			});
		}

		private void Move()
		{
			Move(this.Coordinates, this.Direction);
		}

		private void ChangeDirection()
		{
			var randomValue = new Random(DateTime.Now.Millisecond + DateTime.Now.Second).NextDouble();
			var movement = (int)Math.Floor(randomValue * 4);
			var arrayOfMovement = new Looking[] { Looking.Left, Looking.Up, Looking.Down, Looking.Right };

			this.Direction = arrayOfMovement[movement];
		}

		public override void KillUnit()
		{
			moveTimer.StopTimer();
			changeDirectionTimer.StopTimer();
			this.ColorName = "white";
		}

		public override void Dispose()
		{
			if(moveTimer != null) moveTimer.Dispose();
			if(changeDirectionTimer != null) changeDirectionTimer.Dispose();
		}
	}
}
