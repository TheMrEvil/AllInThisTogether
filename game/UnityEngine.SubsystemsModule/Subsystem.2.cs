using System;

namespace UnityEngine
{
	// Token: 0x0200000C RID: 12
	public abstract class Subsystem<TSubsystemDescriptor> : Subsystem where TSubsystemDescriptor : ISubsystemDescriptor
	{
		// Token: 0x1700000B RID: 11
		// (get) Token: 0x06000026 RID: 38 RVA: 0x00002187 File Offset: 0x00000387
		public TSubsystemDescriptor SubsystemDescriptor
		{
			get
			{
				return (TSubsystemDescriptor)((object)this.m_SubsystemDescriptor);
			}
		}

		// Token: 0x06000027 RID: 39 RVA: 0x00002194 File Offset: 0x00000394
		protected Subsystem()
		{
		}
	}
}
