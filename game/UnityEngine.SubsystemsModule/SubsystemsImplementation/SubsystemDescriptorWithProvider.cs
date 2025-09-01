using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.SubsystemsImplementation
{
	// Token: 0x02000014 RID: 20
	public abstract class SubsystemDescriptorWithProvider : ISubsystemDescriptor
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002AD3 File Offset: 0x00000CD3
		// (set) Token: 0x06000063 RID: 99 RVA: 0x00002ADB File Offset: 0x00000CDB
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

		// Token: 0x17000013 RID: 19
		// (get) Token: 0x06000064 RID: 100 RVA: 0x00002AE4 File Offset: 0x00000CE4
		// (set) Token: 0x06000065 RID: 101 RVA: 0x00002AEC File Offset: 0x00000CEC
		protected internal Type providerType
		{
			[CompilerGenerated]
			get
			{
				return this.<providerType>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<providerType>k__BackingField = value;
			}
		}

		// Token: 0x17000014 RID: 20
		// (get) Token: 0x06000066 RID: 102 RVA: 0x00002AF5 File Offset: 0x00000CF5
		// (set) Token: 0x06000067 RID: 103 RVA: 0x00002AFD File Offset: 0x00000CFD
		protected internal Type subsystemTypeOverride
		{
			[CompilerGenerated]
			get
			{
				return this.<subsystemTypeOverride>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<subsystemTypeOverride>k__BackingField = value;
			}
		}

		// Token: 0x06000068 RID: 104
		internal abstract ISubsystem CreateImpl();

		// Token: 0x06000069 RID: 105 RVA: 0x00002B06 File Offset: 0x00000D06
		ISubsystem ISubsystemDescriptor.Create()
		{
			return this.CreateImpl();
		}

		// Token: 0x0600006A RID: 106
		internal abstract void ThrowIfInvalid();

		// Token: 0x0600006B RID: 107 RVA: 0x000020A8 File Offset: 0x000002A8
		protected SubsystemDescriptorWithProvider()
		{
		}

		// Token: 0x04000011 RID: 17
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <id>k__BackingField;

		// Token: 0x04000012 RID: 18
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Type <providerType>k__BackingField;

		// Token: 0x04000013 RID: 19
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private Type <subsystemTypeOverride>k__BackingField;
	}
}
