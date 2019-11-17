using Pacman.Core.Data;
using System;

namespace Pacman.Core.Interfaces
{
	public interface IUnit: IComponentEvents, IDisposable
	{
		SvgHelper SvgHelper { get; set; }

		byte Size { get; set; }
		byte Border { get; set; }
		byte Velocity { get; set; }
		byte TopScoreBoard { get; set; }
		string GetStyle(Position coordinates);

		void Move(Position coordinates, Looking direction);
      event EventHandler OnMoving;
	}
}
