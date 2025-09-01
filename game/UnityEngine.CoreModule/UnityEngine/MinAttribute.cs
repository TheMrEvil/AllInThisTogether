using System;

namespace UnityEngine
{
	// Token: 0x020001DC RID: 476
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public sealed class MinAttribute : PropertyAttribute
	{
		// Token: 0x060015E2 RID: 5602 RVA: 0x00023207 File Offset: 0x00021407
		public MinAttribute(float min)
		{
			this.min = min;
		}

		// Token: 0x040007B7 RID: 1975
		public readonly float min;
	}
}
