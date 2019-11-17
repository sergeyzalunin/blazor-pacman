using System;
using System.Timers;

namespace Pacman.Core.Common
{
	public class Timer : IDisposable
	{
		System.Timers.Timer timer;

		public bool IsEnabled
		{
			get
			{
				return timer.Enabled;
			}
		}

		public Timer(Action action, int interval)
		{
			timer = new System.Timers.Timer(interval);
			timer.Elapsed += new ElapsedEventHandler((o, e) => action());
			timer.AutoReset = true;
			timer.Enabled = true;
		}

		public void StopTimer()
		{
			if (timer != null && timer.Enabled)
				timer.Stop();
		}

		public void Dispose()
		{
			if (timer != null)
			{
				this.StopTimer();
				timer.Dispose();
			}
		}

	}
}
