using System;

namespace UnityEngine
{
	// Token: 0x020001DD RID: 477
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public sealed class MultilineAttribute : PropertyAttribute
	{
		// Token: 0x060015E3 RID: 5603 RVA: 0x00023218 File Offset: 0x00021418
		public MultilineAttribute()
		{
			this.lines = 3;
		}

		// Token: 0x060015E4 RID: 5604 RVA: 0x00023229 File Offset: 0x00021429
		public MultilineAttribute(int lines)
		{
			this.lines = lines;
		}

		// Token: 0x040007B8 RID: 1976
		public readonly int lines;
	}
}
