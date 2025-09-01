using System;

namespace UnityEngine
{
	// Token: 0x02000009 RID: 9
	public interface ISubsystem
	{
		// Token: 0x17000008 RID: 8
		// (get) Token: 0x0600001A RID: 26
		bool running { get; }

		// Token: 0x0600001B RID: 27
		void Start();

		// Token: 0x0600001C RID: 28
		void Stop();

		// Token: 0x0600001D RID: 29
		void Destroy();
	}
}
