using System;

namespace UnityEngine
{
	// Token: 0x0200000B RID: 11
	public abstract class Subsystem : ISubsystem
	{
		// Token: 0x1700000A RID: 10
		// (get) Token: 0x06000020 RID: 32
		public abstract bool running { get; }

		// Token: 0x06000021 RID: 33
		public abstract void Start();

		// Token: 0x06000022 RID: 34
		public abstract void Stop();

		// Token: 0x06000023 RID: 35 RVA: 0x00002168 File Offset: 0x00000368
		public void Destroy()
		{
			bool flag = SubsystemManager.RemoveDeprecatedSubsystem(this);
			if (flag)
			{
				this.OnDestroy();
			}
		}

		// Token: 0x06000024 RID: 36
		protected abstract void OnDestroy();

		// Token: 0x06000025 RID: 37 RVA: 0x000020A8 File Offset: 0x000002A8
		protected Subsystem()
		{
		}

		// Token: 0x04000004 RID: 4
		internal ISubsystemDescriptor m_SubsystemDescriptor;
	}
}
