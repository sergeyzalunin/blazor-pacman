using Microsoft.AspNetCore.Components;
using Pacman.Core.Data;
using Pacman.Core.Interfaces;

namespace Pacman.Core.Implementation
{
	public class Food : IFood
	{
		public bool Hidden { get; set; }
		public ushort Size { get; set; } = Constants.Size;
		public Position Coordinates { get; set; }

		public void Ate()
		{
			Hidden = true;
		}

		public string GetStyle()
		{
			return string.Format("top: {0}px; left: {1}px", Coordinates.Top, Coordinates.Left);
		}
	}
}
