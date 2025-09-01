using System;
using UnityEngine.Bindings;

namespace UnityEngine
{
	// Token: 0x02000004 RID: 4
	[AttributeUsage(AttributeTargets.Struct)]
	[VisibleToOtherModules]
	internal class IL2CPPStructAlignmentAttribute : Attribute
	{
		// Token: 0x06000005 RID: 5 RVA: 0x00002082 File Offset: 0x00000282
		public IL2CPPStructAlignmentAttribute()
		{
			this.Align = 1;
		}

		// Token: 0x04000003 RID: 3
		public int Align;
	}
}
