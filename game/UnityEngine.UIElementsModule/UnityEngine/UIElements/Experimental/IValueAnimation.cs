using System;

namespace UnityEngine.UIElements.Experimental
{
	// Token: 0x0200038B RID: 907
	public interface IValueAnimation
	{
		// Token: 0x06001D46 RID: 7494
		void Start();

		// Token: 0x06001D47 RID: 7495
		void Stop();

		// Token: 0x06001D48 RID: 7496
		void Recycle();

		// Token: 0x170006FA RID: 1786
		// (get) Token: 0x06001D49 RID: 7497
		bool isRunning { get; }

		// Token: 0x170006FB RID: 1787
		// (get) Token: 0x06001D4A RID: 7498
		// (set) Token: 0x06001D4B RID: 7499
		int durationMs { get; set; }
	}
}
