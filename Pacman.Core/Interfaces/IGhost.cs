using Pacman.Core.Data;

namespace Pacman.Core.Interfaces
{
	public interface IGhost : IUnit
	{
		string ColorName { get; set; }
		Looking Direction { get; set; }
		Position Coordinates { get; set; }

		void Kill();
	}
}
