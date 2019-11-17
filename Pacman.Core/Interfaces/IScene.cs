using System;

namespace Pacman.Core.Interfaces
{
	public interface IScene : IComponentEvents, IDisposable
	{
		IWindow Window { get; set; }
		IPacman Pacman { get; set; }
		IGhost[] Ghosts { get; set; }
		ISceneHeader SceneHeader { get; set; }
		IFood[] Foods { get; set; }
	}
}
