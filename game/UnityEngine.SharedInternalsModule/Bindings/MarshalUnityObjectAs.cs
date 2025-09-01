using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Bindings
{
	// Token: 0x02000026 RID: 38
	[AttributeUsage(AttributeTargets.Class)]
	[VisibleToOtherModules]
	internal class MarshalUnityObjectAs : Attribute, IBindingsAttribute
	{
		// Token: 0x17000022 RID: 34
		// (get) Token: 0x06000077 RID: 119 RVA: 0x000025EB File Offset: 0x000007EB
		// (set) Token: 0x06000078 RID: 120 RVA: 0x000025F3 File Offset: 0x000007F3
		public Type MarshalAsType
		{
			[CompilerGenerated]
			get
			{
				return this.<MarshalAsType>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<MarshalAsType>k__BackingField = value;
			}
		}

		// Token: 0x06000079 RID: 121 RVA: 0x000025FC File Offset: 0x000007FC
		public MarshalUnityObjectAs(Type marshalAsType)
		{
			this.MarshalAsType = marshalAsType;
		}

		// Token: 0x04000027 RID: 39
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private Type <MarshalAsType>k__BackingField;
	}
}
