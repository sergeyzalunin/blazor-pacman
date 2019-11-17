using System.Threading.Tasks;
using Microsoft.AspNetCore.Components;
using Pacman.Core.Common;
using Pacman.Core.Interfaces;

namespace Pacman.Core.Implementation
{
	public class Scene : IScene
	{
		bool crashed;		
		Timer crashTimer, lookingForEatTimer;

		[Inject]
		public IWindow Window { get; set; }

		[Inject]
		public IPacman Pacman { get; set; }

		[Inject]
		public IGhost[] Ghosts { get; set; }

		[Inject]
		public ISceneHeader SceneHeader { get; set; }

		[Inject]
		public IFood[] Foods { get; set; }

		public Scene(IWindow window, ISceneHeader sceneHeader, IPacman pacman, IGhost[] ghosts, IFood[] foods)
		{
			this.Window = window;
			this.SceneHeader = sceneHeader;
			this.Pacman = pacman;
			this.Ghosts = ghosts;
			this.Foods = foods;
		}


		public Task OnInitializedAsync()
		{
			throw new System.NotImplementedException();
		}

		public async Task OnAfterRenderAsync(bool firstRender)
		{
			await Task.Run(() =>
			{
				crashTimer = new Timer(LookForCrash, 100);
				lookingForEatTimer = new Timer(LookingForEat, 500);
			});
		}

		void KillGhosts()
		{
			for(int i = 0; i < 4; i++)
			{
				var currentGhost = Ghosts[i];
				currentGhost.Kill();
			}
		}

		void LookForCrash()
		{
			var pacmanX = Pacman.Coordinates.Left;
			var pacmanY = Pacman.Coordinates.Top;
			var pacmanLastX = Pacman.Coordinates.Left + Pacman.Size;
			var pacmanLastY = Pacman.Coordinates.Top + Pacman.Size;

			for(var i = 0; i < 4; i++)
			{
				var currentGhost = Ghosts[i];
				var currentGhostX = currentGhost.Coordinates.Left;
				var currentGhostY = currentGhost.Coordinates.Top;
				var currentGhostLastX = currentGhost.Coordinates.Left + currentGhost.Size;
				var currentGhostLastY = currentGhost.Coordinates.Top + currentGhost.Size;

				if((pacmanX >= currentGhostX && pacmanX <= currentGhostLastX) || (pacmanLastX >= currentGhostX && pacmanLastX <= currentGhostLastX))
				{
					if((pacmanY >= currentGhostY && pacmanY <= currentGhostLastY) || (pacmanLastY >= currentGhostY && pacmanLastY <= currentGhostLastY))
					{
						this.crashed = true;
					}
				}

				if(this.crashed)
				{
					SceneHeader.GameOver();
					crashTimer.StopTimer();
					this.KillGhosts();
					break;
				}
			}
		}

		void LookingForEat()
		{
			var pacmanX = Pacman.Coordinates.Left;
			var pacmanY = Pacman.Coordinates.Top;
			var pacmanLastX = Pacman.Coordinates.Left + Pacman.Size / 2;
			var pacmanLastY = Pacman.Coordinates.Top + Pacman.Size / 2;

			for(var i = 0; i < Foods.Length; i++)
			{
				var currentFood = Foods[i];
				var currentFoodX = currentFood.Coordinates.Left;
				var currentFoodY = currentFood.Coordinates.Top;
				var currentFoodLastX = currentFood.Coordinates.Left + currentFood.Size / 2;
				var currentFoodLastY = currentFood.Coordinates.Top + currentFood.Size / 2;

				if((pacmanX >= currentFoodX && pacmanX <= currentFoodLastX) || (pacmanLastX >= currentFoodX && pacmanLastX <= currentFoodLastX))
				{
					if((pacmanY >= currentFoodY && pacmanY <= currentFoodLastY) || (pacmanLastY >= currentFoodY && pacmanLastY <= currentFoodLastY))
					{
						if(!currentFood.Hidden)
						{
							currentFood.Ate();
							SceneHeader.IncreasePoints();
						}
					}
				}

				if(this.crashed)
				{
					lookingForEatTimer.StopTimer();
				}
			}
		}
		
		public void OnCollide()
		{
			throw new System.NotImplementedException();
		}

		public void Dispose()
		{
			if(crashTimer != null) crashTimer.Dispose();
			if(lookingForEatTimer != null) lookingForEatTimer.Dispose();
		}
	}
}
