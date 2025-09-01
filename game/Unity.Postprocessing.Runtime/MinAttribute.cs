using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000008 RID: 8
	[AttributeUsage(AttributeTargets.Field, AllowMultiple = false)]
	public sealed class MinAttribute : Attribute
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000021AE File Offset: 0x000003AE
		public MinAttribute(float min)
		{
			this.min = min;
		}

		// Token: 0x04000019 RID: 25
		public readonly float min;
	}
}
