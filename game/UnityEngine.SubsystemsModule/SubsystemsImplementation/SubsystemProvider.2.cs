using System;

namespace UnityEngine.SubsystemsImplementation
{
	// Token: 0x02000017 RID: 23
	public abstract class SubsystemProvider<TSubsystem> : SubsystemProvider where TSubsystem : SubsystemWithProvider, new()
	{
		// Token: 0x06000073 RID: 115 RVA: 0x00002CC1 File Offset: 0x00000EC1
		protected internal virtual bool TryInitialize()
		{
			return true;
		}

		// Token: 0x06000074 RID: 116
		public abstract void Start();

		// Token: 0x06000075 RID: 117
		public abstract void Stop();

		// Token: 0x06000076 RID: 118
		public abstract void Destroy();

		// Token: 0x06000077 RID: 119 RVA: 0x00002CC4 File Offset: 0x00000EC4
		protected SubsystemProvider()
		{
		}
	}
}
