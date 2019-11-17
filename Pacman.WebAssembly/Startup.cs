using Microsoft.AspNetCore.Components.Builder;
using Microsoft.Extensions.DependencyInjection;
using Pacman.Core.Data;
using Pacman.Core.Implementation;
using Pacman.Core.Interfaces;

namespace Pacman.WebAssembly
{
	public class Startup
	{
		// This method gets called by the runtime. Use this method to add services to the container.
		// For more information on how to configure your application, visit https://go.microsoft.com/fwlink/?LinkID=398940
		public void ConfigureServices(IServiceCollection services)
		{
			services.AddTransient<SvgHelper>();
			services.AddScoped<BrowserService>();
			services.AddScoped<IWindow, BrowserWindow>();
			services.AddScoped<IPacman, Pacman.Core.Implementation.Pacman>();
			services.AddScoped<ISceneHeader, SceneHeader>();
			services.AddScoped<IGhost[]>(new GhostFactory().GetGhosts);
			services.AddScoped<IFood[]>(new FoodFactory().GetFood);
			services.AddScoped<IScene, Scene>();
		}

		public void Configure(IComponentsApplicationBuilder app)
		{
			app.AddComponent<App>("app");
		}
	}
}
