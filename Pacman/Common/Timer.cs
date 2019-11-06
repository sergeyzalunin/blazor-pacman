using System;
using System.Timers;

namespace Pacman.Common
{
	public class Timer : IDisposable
	{
		System.Timers.Timer timer;

		public Timer(Action action, int interval)
		{
			timer = new System.Timers.Timer(interval);
			timer.Elapsed += new ElapsedEventHandler((o, e) => action());
			timer.AutoReset = true;
			timer.Enabled = true;
		}

		public void StopTimer()
		{
			timer.Stop();
		}

		public void Dispose()
		{
			timer.Dispose();
		}

	}
}
