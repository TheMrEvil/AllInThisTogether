using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine
{
	// Token: 0x0200000D RID: 13
	public abstract class SubsystemDescriptor : ISubsystemDescriptor
	{
		// Token: 0x1700000C RID: 12
		// (get) Token: 0x06000028 RID: 40 RVA: 0x0000219D File Offset: 0x0000039D
		// (set) Token: 0x06000029 RID: 41 RVA: 0x000021A5 File Offset: 0x000003A5
		public string id
		{
			[CompilerGenerated]
			get
			{
				return this.<id>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<id>k__BackingField = value;
			}
		}

		// Token: 0x1700000D RID: 13
		// (get) Token: 0x0600002A RID: 42 RVA: 0x000021AE File Offset: 0x000003AE
		// (set) Token: 0x0600002B RID: 43 RVA: 0x000021B6 File Offset: 0x000003B6
		public Type subsystemImplementationType
		{
			[CompilerGenerated]
			get
			{
				return this.<subsystemImplementationType>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<subsystemImplementationType>k__BackingField = value;
			}
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000021BF File Offset: 0x000003BF
		ISubsystem ISubsystemDescriptor.Create()
		{
			return this.CreateImpl();
		}

		// Token: 0x0600002D RID: 45
		internal abstract ISubsystem CreateImpl();

		// Token: 0x0600002E RID: 46 RVA: 0x000020A8 File Offset: 0x000002A8
		protected SubsystemDescriptor()
		{
		}

		// Token: 0x04000005 RID: 5
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <id>k__BackingField;

		// Token: 0x04000006 RID: 6
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Type <subsystemImplementationType>k__BackingField;
	}
}
