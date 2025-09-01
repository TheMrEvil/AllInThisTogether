using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000009 RID: 9
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	public sealed class MinMaxAttribute : Attribute
	{
		// Token: 0x0600000C RID: 12 RVA: 0x000021BD File Offset: 0x000003BD
		public MinMaxAttribute(float min, float max)
		{
			this.min = min;
			this.max = max;
		}

		// Token: 0x0400001A RID: 26
		public readonly float min;

		// Token: 0x0400001B RID: 27
		public readonly float max;
	}
}
