using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Bindings
{
	// Token: 0x02000019 RID: 25
	[VisibleToOtherModules]
	[AttributeUsage(AttributeTargets.Property)]
	internal class NativePropertyAttribute : NativeMethodAttribute
	{
		// Token: 0x17000019 RID: 25
		// (get) Token: 0x0600004C RID: 76 RVA: 0x000023AC File Offset: 0x000005AC
		// (set) Token: 0x0600004D RID: 77 RVA: 0x000023B4 File Offset: 0x000005B4
		public TargetType TargetType
		{
			[CompilerGenerated]
			get
			{
				return this.<TargetType>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<TargetType>k__BackingField = value;
			}
		}

		// Token: 0x0600004E RID: 78 RVA: 0x000023BD File Offset: 0x000005BD
		public NativePropertyAttribute()
		{
		}

		// Token: 0x0600004F RID: 79 RVA: 0x000023C7 File Offset: 0x000005C7
		public NativePropertyAttribute(string name) : base(name)
		{
		}

		// Token: 0x06000050 RID: 80 RVA: 0x000023D2 File Offset: 0x000005D2
		public NativePropertyAttribute(string name, TargetType targetType) : base(name)
		{
			this.TargetType = targetType;
		}

		// Token: 0x06000051 RID: 81 RVA: 0x000023E5 File Offset: 0x000005E5
		public NativePropertyAttribute(string name, bool isFree, TargetType targetType) : base(name, isFree)
		{
			this.TargetType = targetType;
		}

		// Token: 0x06000052 RID: 82 RVA: 0x000023F9 File Offset: 0x000005F9
		public NativePropertyAttribute(string name, bool isFree, TargetType targetType, bool isThreadSafe) : base(name, isFree, isThreadSafe)
		{
			this.TargetType = targetType;
		}

		// Token: 0x04000015 RID: 21
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private TargetType <TargetType>k__BackingField;
	}
}
