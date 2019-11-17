using System;
using System.Threading.Tasks;

namespace Pacman.Core.Interfaces
{
	public interface IComponentEvents
	{
		Task OnInitializedAsync();
		Task OnAfterRenderAsync();

      event EventHandler OnStateHasChanged;
	}
}
