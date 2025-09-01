using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.SubsystemsImplementation
{
	// Token: 0x02000018 RID: 24
	public class SubsystemProxy<TSubsystem, TProvider> where TSubsystem : SubsystemWithProvider, new() where TProvider : SubsystemProvider<TSubsystem>
	{
		// Token: 0x17000016 RID: 22
		// (get) Token: 0x06000078 RID: 120 RVA: 0x00002CCD File Offset: 0x00000ECD
		// (set) Token: 0x06000079 RID: 121 RVA: 0x00002CD5 File Offset: 0x00000ED5
		public TProvider provider
		{
			[CompilerGenerated]
			get
			{
				return this.<provider>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<provider>k__BackingField = value;
			}
		}

		// Token: 0x17000017 RID: 23
		// (get) Token: 0x0600007A RID: 122 RVA: 0x00002CDE File Offset: 0x00000EDE
		// (set) Token: 0x0600007B RID: 123 RVA: 0x00002CF0 File Offset: 0x00000EF0
		public bool running
		{
			get
			{
				return this.provider.running;
			}
			set
			{
				this.provider.m_Running = value;
			}
		}

		// Token: 0x0600007C RID: 124 RVA: 0x00002D03 File Offset: 0x00000F03
		internal SubsystemProxy(TProvider provider)
		{
			this.provider = provider;
		}

		// Token: 0x04000015 RID: 21
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private TProvider <provider>k__BackingField;
	}
}
