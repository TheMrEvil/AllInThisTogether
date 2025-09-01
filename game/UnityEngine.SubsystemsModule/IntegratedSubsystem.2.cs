using System;
using UnityEngine.Scripting;

namespace UnityEngine
{
	// Token: 0x02000003 RID: 3
	[UsedByNativeCode("Subsystem_TSubsystemDescriptor")]
	public class IntegratedSubsystem<TSubsystemDescriptor> : IntegratedSubsystem where TSubsystemDescriptor : ISubsystemDescriptor
	{
		// Token: 0x17000003 RID: 3
		// (get) Token: 0x06000009 RID: 9 RVA: 0x000020B1 File Offset: 0x000002B1
		public TSubsystemDescriptor subsystemDescriptor
		{
			get
			{
				return (TSubsystemDescriptor)((object)this.m_SubsystemDescriptor);
			}
		}

		// Token: 0x17000004 RID: 4
		// (get) Token: 0x0600000A RID: 10 RVA: 0x000020BE File Offset: 0x000002BE
		public TSubsystemDescriptor SubsystemDescriptor
		{
			get
			{
				return this.subsystemDescriptor;
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000020C6 File Offset: 0x000002C6
		public IntegratedSubsystem()
		{
		}
	}
}
