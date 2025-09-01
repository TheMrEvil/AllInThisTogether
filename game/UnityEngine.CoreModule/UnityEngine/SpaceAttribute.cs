using System;

namespace UnityEngine
{
	// Token: 0x020001D9 RID: 473
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = true)]
	public class SpaceAttribute : PropertyAttribute
	{
		// Token: 0x060015DE RID: 5598 RVA: 0x000231B8 File Offset: 0x000213B8
		public SpaceAttribute()
		{
			this.height = 8f;
		}

		// Token: 0x060015DF RID: 5599 RVA: 0x000231CD File Offset: 0x000213CD
		public SpaceAttribute(float height)
		{
			this.height = height;
		}

		// Token: 0x040007B3 RID: 1971
		public readonly float height;
	}
}
