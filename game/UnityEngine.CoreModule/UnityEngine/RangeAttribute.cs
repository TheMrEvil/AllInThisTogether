using System;

namespace UnityEngine
{
	// Token: 0x020001DB RID: 475
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public sealed class RangeAttribute : PropertyAttribute
	{
		// Token: 0x060015E1 RID: 5601 RVA: 0x000231EF File Offset: 0x000213EF
		public RangeAttribute(float min, float max)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x040007B5 RID: 1973
		public readonly float min;

		// Token: 0x040007B6 RID: 1974
		public readonly float max;
	}
}
