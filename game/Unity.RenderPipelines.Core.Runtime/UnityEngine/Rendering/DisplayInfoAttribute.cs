using System;

namespace UnityEngine.Rendering
{
	// Token: 0x02000048 RID: 72
	[AttributeUsage(AttributeTargets.Field)]
	public class DisplayInfoAttribute : Attribute
	{
		// Token: 0x0600027D RID: 637 RVA: 0x0000D0F4 File Offset: 0x0000B2F4
		public DisplayInfoAttribute()
		{
		}

		// Token: 0x040001B3 RID: 435
		public string name;

		// Token: 0x040001B4 RID: 436
		public int order;
	}
}
