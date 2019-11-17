using Pacman.Core.Data;

namespace Pacman.Core.Interfaces
{
	public interface IPacman : IUnit
	{
		Looking Direction { get; set; }
		Position Coordinates { get; set; }

		string GetDirectionClassName();
	}
}
