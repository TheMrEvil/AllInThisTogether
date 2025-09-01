using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000D3 RID: 211
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class ColorParameter : VolumeParameter<Color>
	{
		// Token: 0x0600069A RID: 1690 RVA: 0x0001DBBE File Offset: 0x0001BDBE
		public ColorParameter(Color value, bool overrideState = false) : base(value, overrideState)
		{
		}

		// Token: 0x0600069B RID: 1691 RVA: 0x0001DBD6 File Offset: 0x0001BDD6
		public ColorParameter(Color value, bool hdr, bool showAlpha, bool showEyeDropper, bool overrideState = false) : base(value, overrideState)
		{
			this.hdr = hdr;
			this.showAlpha = showAlpha;
			this.showEyeDropper = showEyeDropper;
			this.overrideState = overrideState;
		}

		// Token: 0x0600069C RID: 1692 RVA: 0x0001DC10 File Offset: 0x0001BE10
		public override void Interp(Color from, Color to, float t)
		{
			this.m_Value.r = from.r + (to.r - from.r) * t;
			this.m_Value.g = from.g + (to.g - from.g) * t;
			this.m_Value.b = from.b + (to.b - from.b) * t;
			this.m_Value.a = from.a + (to.a - from.a) * t;
		}

		// Token: 0x040003BF RID: 959
		[NonSerialized]
		public bool hdr;

		// Token: 0x040003C0 RID: 960
		[NonSerialized]
		public bool showAlpha = true;

		// Token: 0x040003C1 RID: 961
		[NonSerialized]
		public bool showEyeDropper = true;
	}
}
