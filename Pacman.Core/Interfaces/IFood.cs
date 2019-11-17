using Pacman.Core.Data;

namespace Pacman.Core.Interfaces
{
	public interface IFood
	{
		bool Hidden { get; set; }
		ushort Size { get; set; }
		Position Coordinates { get; set; }

		void Ate();
		string GetStyle();
	}
}
