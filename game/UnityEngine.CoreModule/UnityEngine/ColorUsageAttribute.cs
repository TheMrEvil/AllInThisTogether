using System;

namespace UnityEngine
{
	// Token: 0x020001DF RID: 479
	[AttributeUsage(AttributeTargets.Field, Inherited = true, AllowMultiple = false)]
	public sealed class ColorUsageAttribute : PropertyAttribute
	{
		// Token: 0x060015E7 RID: 5607 RVA: 0x0002326C File Offset: 0x0002146C
		public ColorUsageAttribute(bool showAlpha)
		{
			this.showAlpha = showAlpha;
		}

		// Token: 0x060015E8 RID: 5608 RVA: 0x000232C4 File Offset: 0x000214C4
		public ColorUsageAttribute(bool showAlpha, bool hdr)
		{
			this.showAlpha = showAlpha;
			this.hdr = hdr;
		}

		// Token: 0x060015E9 RID: 5609 RVA: 0x00023324 File Offset: 0x00021524
		[Obsolete("Brightness and exposure parameters are no longer used for anything. Use ColorUsageAttribute(bool showAlpha, bool hdr)")]
		public ColorUsageAttribute(bool showAlpha, bool hdr, float minBrightness, float maxBrightness, float minExposureValue, float maxExposureValue)
		{
			this.showAlpha = showAlpha;
			this.hdr = hdr;
			this.minBrightness = minBrightness;
			this.maxBrightness = maxBrightness;
			this.minExposureValue = minExposureValue;
			this.maxExposureValue = maxExposureValue;
		}

		// Token: 0x040007BB RID: 1979
		public readonly bool showAlpha = true;

		// Token: 0x040007BC RID: 1980
		public readonly bool hdr = false;

		// Token: 0x040007BD RID: 1981
		[Obsolete("This field is no longer used for anything.")]
		public readonly float minBrightness = 0f;

		// Token: 0x040007BE RID: 1982
		[Obsolete("This field is no longer used for anything.")]
		public readonly float maxBrightness = 8f;

		// Token: 0x040007BF RID: 1983
		[Obsolete("This field is no longer used for anything.")]
		public readonly float minExposureValue = 0.125f;

		// Token: 0x040007C0 RID: 1984
		[Obsolete("This field is no longer used for anything.")]
		public readonly float maxExposureValue = 3f;
	}
}
