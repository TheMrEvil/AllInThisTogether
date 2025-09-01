using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;

namespace UnityEngine.Bindings
{
	// Token: 0x02000014 RID: 20
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Parameter | AttributeTargets.ReturnValue, AllowMultiple = true)]
	[VisibleToOtherModules]
	internal class NativeHeaderAttribute : Attribute, IBindingsHeaderProviderAttribute, IBindingsAttribute
	{
		// Token: 0x17000010 RID: 16
		// (get) Token: 0x0600002F RID: 47 RVA: 0x000021C1 File Offset: 0x000003C1
		// (set) Token: 0x06000030 RID: 48 RVA: 0x000021C9 File Offset: 0x000003C9
		public string Header
		{
			[CompilerGenerated]
			get
			{
				return this.<Header>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Header>k__BackingField = value;
			}
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00002078 File Offset: 0x00000278
		public NativeHeaderAttribute()
		{
		}

		// Token: 0x06000032 RID: 50 RVA: 0x000021D4 File Offset: 0x000003D4
		public NativeHeaderAttribute(string header)
		{
			bool flag = header == null;
			if (flag)
			{
				throw new ArgumentNullException("header");
			}
			bool flag2 = header == "";
			if (flag2)
			{
				throw new ArgumentException("header cannot be empty", "header");
			}
			this.Header = header;
		}

		// Token: 0x04000009 RID: 9
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		[CompilerGenerated]
		private string <Header>k__BackingField;
	}
}
