using System;

namespace UnityEngine
{
	// Token: 0x020001E0 RID: 480
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public sealed class GradientUsageAttribute : PropertyAttribute
	{
		// Token: 0x060015EA RID: 5610 RVA: 0x000233A0 File Offset: 0x000215A0
		public GradientUsageAttribute(bool hdr)
		{
			this.hdr = hdr;
			this.colorSpace = ColorSpace.Gamma;
		}

		// Token: 0x060015EB RID: 5611 RVA: 0x000233C6 File Offset: 0x000215C6
		public GradientUsageAttribute(bool hdr, ColorSpace colorSpace)
		{
			this.hdr = hdr;
			this.colorSpace = colorSpace;
		}

		// Token: 0x040007C1 RID: 1985
		public readonly bool hdr = false;

		// Token: 0x040007C2 RID: 1986
		public readonly ColorSpace colorSpace = ColorSpace.Gamma;
	}
}
