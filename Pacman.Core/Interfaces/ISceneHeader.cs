namespace Pacman.Core.Interfaces
{
	public interface ISceneHeader
	{
		string GetPoints();
		void IncreasePoints();
		void GameOver();
	}
}
