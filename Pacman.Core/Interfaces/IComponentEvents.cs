using System.Threading.Tasks;

namespace Pacman.Core.Interfaces
{
	public interface IComponentEvents
	{
		Task OnInitializedAsync();

		Task OnAfterRenderAsync(bool firstRender);

		void OnCollide();
	}
}
