using Pacman.Core.Data;
using Pacman.Core.Interfaces;
using System;
using System.Linq;

namespace Pacman.Core.Implementation
{
	public class FoodFactory
	{
		const int foodSize = 60;
		const int border = 20;
		const int topScoreBoard = 100;

		public IFood[] GetFood(IServiceProvider prov)
		{
			IWindow window = (IWindow)prov.GetService(typeof(IWindow));

			int currentTop = topScoreBoard, currentLeft = 0;
			int amountOfFood = ((window.Width - border * 2 - foodSize) * (window.Height - border - topScoreBoard)) / (foodSize * 2);

			IFood[] result = new IFood[amountOfFood];

			for(var i = 0; i < amountOfFood; i++)
			{
				if(currentLeft + foodSize >= window.Width - border)
				{
					currentTop += foodSize;
					currentLeft = 0;
				}

				if(currentTop + foodSize >= (window.Height - border - topScoreBoard))
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
