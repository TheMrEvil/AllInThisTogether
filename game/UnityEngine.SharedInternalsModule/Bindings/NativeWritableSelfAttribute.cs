using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Bindings
{
	// Token: 0x02000016 RID: 22
	[AttributeUsage(AttributeTargets.Method)]
	[VisibleToOtherModules]
	internal sealed class NativeWritableSelfAttribute : Attribute, IBindingsWritableSelfProviderAttribute, IBindingsAttribute
	{
		// Token: 0x17000012 RID: 18
		// (get) Token: 0x06000037 RID: 55 RVA: 0x00002283 File Offset: 0x00000483
		// (set) Token: 0x06000038 RID: 56 RVA: 0x0000228B File Offset: 0x0000048B
		public bool WritableSelf
		{
			[CompilerGenerated]
			get
			{
				return this.<WritableSelf>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<WritableSelf>k__BackingField = value;
			}
		}

		// Token: 0x06000039 RID: 57 RVA: 0x00002294 File Offset: 0x00000494
		public NativeWritableSelfAttribute()
		{
			this.WritableSelf = true;
		}

		// Token: 0x0600003A RID: 58 RVA: 0x000022A6 File Offset: 0x000004A6
		public NativeWritableSelfAttribute(bool writable)
		{
			this.WritableSelf = writable;
		}

		// Token: 0x0400000B RID: 11
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private bool <WritableSelf>k__BackingField;
	}
}
