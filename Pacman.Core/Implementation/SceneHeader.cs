using Pacman.Core.Interfaces;

namespace Pacman.Core.Implementation
{
	public class SceneHeader : ISceneHeader
	{
		long points;
		bool playing = true;

		public void GameOver()
		{
			playing = false;
		}

		public string GetPoints()
		{
			return playing ? points.ToString() : "GAME OVER";
		}

		public void IncreasePoints()
		{
			points++;
		}
	}
}
