using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.SubsystemsImplementation
{
	// Token: 0x0200001A RID: 26
	public abstract class SubsystemWithProvider<TSubsystem, TSubsystemDescriptor, TProvider> : SubsystemWithProvider where TSubsystem : SubsystemWithProvider, new() where TSubsystemDescriptor : SubsystemDescriptorWithProvider where TProvider : SubsystemProvider<TSubsystem>
	{
		// Token: 0x1700001B RID: 27
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00002DCC File Offset: 0x00000FCC
		// (set) Token: 0x0600008B RID: 139 RVA: 0x00002DD4 File Offset: 0x00000FD4
		public TSubsystemDescriptor subsystemDescriptor
		{
			[CompilerGenerated]
			get
			{
				return this.<subsystemDescriptor>k__BackingField;
			}
			[CompilerGenerated]
			private set
			{
				this.<subsystemDescriptor>k__BackingField = value;
			}
		}

		// Token: 0x1700001C RID: 28
		// (get) Token: 0x0600008C RID: 140 RVA: 0x00002DDD File Offset: 0x00000FDD
		// (set) Token: 0x0600008D RID: 141 RVA: 0x00002DE5 File Offset: 0x00000FE5
		protected internal TProvider provider
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

		// Token: 0x0600008E RID: 142 RVA: 0x00002DEE File Offset: 0x00000FEE
		protected virtual void OnCreate()
		{
		}

		// Token: 0x0600008F RID: 143 RVA: 0x00002DF1 File Offset: 0x00000FF1
		protected override void OnStart()
		{
			this.provider.Start();
		}

		// Token: 0x06000090 RID: 144 RVA: 0x00002E04 File Offset: 0x00001004
		protected override void OnStop()
		{
			this.provider.Stop();
		}

		// Token: 0x06000091 RID: 145 RVA: 0x00002E17 File Offset: 0x00001017
		protected override void OnDestroy()
		{
			this.provider.Destroy();
		}

		// Token: 0x06000092 RID: 146 RVA: 0x00002E2A File Offset: 0x0000102A
		internal sealed override void Initialize(SubsystemDescriptorWithProvider descriptor, SubsystemProvider provider)
		{
			base.providerBase = provider;
			this.provider = (TProvider)((object)provider);
			this.subsystemDescriptor = (TSubsystemDescriptor)((object)descriptor);
			this.OnCreate();
		}

		// Token: 0x1700001D RID: 29
		// (get) Token: 0x06000093 RID: 147 RVA: 0x00002E56 File Offset: 0x00001056
		internal sealed override SubsystemDescriptorWithProvider descriptor
		{
			get
			{
				return this.subsystemDescriptor;
			}
		}

		// Token: 0x06000094 RID: 148 RVA: 0x00002E63 File Offset: 0x00001063
		protected SubsystemWithProvider()
		{
		}

		// Token: 0x04000018 RID: 24
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private TSubsystemDescriptor <subsystemDescriptor>k__BackingField;

		// Token: 0x04000019 RID: 25
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private TProvider <provider>k__BackingField;
	}
}
