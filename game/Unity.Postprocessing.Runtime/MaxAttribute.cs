using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000007 RID: 7
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	public sealed class MaxAttribute : Attribute
	{
		// Token: 0x0600000A RID: 10 RVA: 0x0000219F File Offset: 0x0000039F
		public MaxAttribute(float max)
		{
			this.max = max;
		}

		// Token: 0x04000018 RID: 24
		public readonly float max;
	}
}
