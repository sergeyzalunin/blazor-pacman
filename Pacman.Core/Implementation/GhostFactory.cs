using Pacman.Core.Data;
using Pacman.Core.Interfaces;
using System;

namespace Pacman.Core.Implementation
{
	public class GhostFactory
	{
		public IGhost[] GetGhosts(IServiceProvider prov)
		{
			SvgHelper svgHelper = (SvgHelper)prov.GetService(typeof(SvgHelper));
			BrowserService bs = (BrowserService)prov.GetService(typeof(BrowserService));

			return new[]
			{
				new Ghost(svgHelper, bs),
				new Ghost(svgHelper, bs),
				new Ghost(svgHelper, bs),
				new Ghost(svgHelper, bs)
			};
		}
	}
}
