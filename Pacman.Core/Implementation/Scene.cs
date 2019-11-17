using System;
using System.Threading.Tasks;
using Pacman.Core.Common;
using Pacman.Core.Interfaces;

namespace Pacman.Core.Implementation
{
	public class Scene : IScene
	{
		bool crashed;		
		Timer crashTimer, lookingForEatTimer;

		public event EventHandler OnStateHasChanged;

		public IWindow Window { get; set; }
		public IPacman Pacman { get; set; }
		public IGhost[] Ghosts { get; set; }
		public ISceneHeader SceneHeader { get; set; }
		public IFood[] Foods { get; set; }

		public Scene(IWindow window, ISceneHeader sceneHeader, IPacman pacman, IGhost[] ghosts)
		{
			this.Window = window;
			this.SceneHeader = sceneHeader;
			this.Pacman = pacman;
			this.Ghosts = ghosts;
		}

		public Task OnInitializedAsync()
		{
			throw new System.NotImplementedException();
		}

		public async Task OnAfterRenderAsync()
		{
			await Task.Run(() =>
			{
				crashTimer = new Timer(LookForCrash, 100);
				lookingForEatTimer = new Timer(LookingForEat, 300);
			});
		}

		void KillGhosts()
		{
			for(int i = 0; i < 4; i++)
			{
				var currentGhost = Ghosts[i];
				currentGhost.KillUnit();
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
					KillGhosts();
					Pacman.KillUnit();
					StateHasChanging();
					break;
				}
			}
		}

		void LookingForEat()
		{
			var pacmanX = Pacman.Coordinates.Left;
			var pacmanY = Pacman.Coordinates.Top;
			var pacmanLastX = pacmanX + Pacman.Size / 2;
			var pacmanLastY = pacmanY + Pacman.Size / 2;

			for(var i = 0; i < Foods.Length; i++)
			{
				var currentFood = Foods[i];
				var currentFoodX = currentFood.Coordinates.Left;
				var currentFoodY = currentFood.Coordinates.Top;
				var currentFoodLastX = currentFoodX + currentFood.Size / 2;
				var currentFoodLastY = currentFoodY + currentFood.Size / 2;

				if((pacmanX >= currentFoodX && pacmanX <= currentFoodLastX) || (pacmanLastX >= currentFoodX && pacmanLastX <= currentFoodLastX))
				{
					if((pacmanY >= currentFoodY && pacmanY <= currentFoodLastY) || (pacmanLastY >= currentFoodY && pacmanLastY <= currentFoodLastY))
					{
						if(!currentFood.Hidden)
						{
							currentFood.Ate();
							SceneHeader.IncreasePoints();
							StateHasChanging();
						}
					}
				}

				if(this.crashed)
				{
					lookingForEatTimer.StopTimer();
					StateHasChanging();
				}
			}
		}
		 
      protected virtual void StateHasChanging()
      {  
         OnStateHasChanged?.Invoke(this, EventArgs.Empty);  
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
