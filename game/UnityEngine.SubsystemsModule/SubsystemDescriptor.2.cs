using System;

namespace UnityEngine
{
	// Token: 0x0200000E RID: 14
	public class SubsystemDescriptor<TSubsystem> : SubsystemDescriptor where TSubsystem : Subsystem
	{
		// Token: 0x0600002F RID: 47 RVA: 0x000021C7 File Offset: 0x000003C7
		internal override ISubsystem CreateImpl()
		{
			return this.Create();
		}

		// Token: 0x06000030 RID: 48 RVA: 0x000021D4 File Offset: 0x000003D4
		public TSubsystem Create()
		{
			TSubsystem tsubsystem = SubsystemManager.FindDeprecatedSubsystemByDescriptor(this) as TSubsystem;
			bool flag = tsubsystem != null;
			TSubsystem result;
			if (flag)
			{
				result = tsubsystem;
			}
			else
			{
				tsubsystem = (Activator.CreateInstance(base.subsystemImplementationType) as TSubsystem);
				tsubsystem.m_SubsystemDescriptor = this;
				SubsystemManager.AddDeprecatedSubsystem(tsubsystem);
				result = tsubsystem;
			}
			return result;
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002237 File Offset: 0x00000437
		public SubsystemDescriptor()
		{
		}
	}
}
