using Pacman.Core.Data;
using Pacman.Core.Interfaces;
using System.Linq;

namespace Pacman.Core.Implementation
{
	public class FoodFactory
	{
		int foodSize = Constants.Size;
		int border = Constants.Border;
		int topScoreBoard = Constants.TopScoreBoard;

		public IFood[] GetFood(IWindow window)
		{
			int currentTop = topScoreBoard, currentLeft = 0;

			int amountOnWidth = (window.Width - border) / foodSize;
			int amountOnHeight = (window.Height - border - topScoreBoard) / foodSize;
			int amountOfFood = amountOnWidth * amountOnHeight;

			IFood[] result = new IFood[amountOfFood];

			for(var i = 0; i < amountOfFood; i++)
			{
				if (currentLeft + foodSize >= window.Width - border)
				{
					currentTop += foodSize;
					currentLeft = 0;
				}

				if (currentTop + foodSize >= window.Height)
				{
					break;
				}

				result[i] = new Food
				{
					Coordinates = new Position { Left = currentLeft, Top = currentTop }
				};

				currentLeft += foodSize;
			}

			return result.Where(n => n != null).ToArray();
		}
	}
}
