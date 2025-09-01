using System;
using System.Diagnostics;
using System.Runtime.CompilerServices;
using UnityEngine.Bindings;

namespace UnityEngine.Scripting
{
	// Token: 0x0200002B RID: 43
	[AttributeUsage(AttributeTargets.Class | AttributeTargets.Struct | AttributeTargets.Enum | AttributeTargets.Constructor | AttributeTargets.Method | AttributeTargets.Property | AttributeTargets.Field | AttributeTargets.Interface, Inherited = false)]
	[VisibleToOtherModules]
	internal class UsedByNativeCodeAttribute : Attribute
	{
		// Token: 0x06000088 RID: 136 RVA: 0x00002078 File Offset: 0x00000278
		public UsedByNativeCodeAttribute()
		{
		}

		// Token: 0x06000089 RID: 137 RVA: 0x00002663 File Offset: 0x00000863
		public UsedByNativeCodeAttribute(string name)
		{
			this.Name = name;
		}

		// Token: 0x17000029 RID: 41
		// (get) Token: 0x0600008A RID: 138 RVA: 0x00002675 File Offset: 0x00000875
		// (set) Token: 0x0600008B RID: 139 RVA: 0x0000267D File Offset: 0x0000087D
		public string Name
		{
			[CompilerGenerated]
			get
			{
				return this.<Name>k__BackingField;
			}
			[CompilerGenerated]
			set
			{
				this.<Name>k__BackingField = value;
			}
		}

		// Token: 0x0400002F RID: 47
		[CompilerGenerated]
		[DebuggerBrowsable(DebuggerBrowsableState.Never)]
		private string <Name>k__BackingField;
	}
}
