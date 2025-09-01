using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000D4 RID: 212
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpColorParameter : VolumeParameter<Color>
	{
		// Token: 0x0600069D RID: 1693 RVA: 0x0001DCA1 File Offset: 0x0001BEA1
		public NoInterpColorParameter(Color value, bool overrideState = false) : base(value, overrideState)
		{
		}

		// Token: 0x0600069E RID: 1694 RVA: 0x0001DCB9 File Offset: 0x0001BEB9
		public NoInterpColorParameter(Color value, bool hdr, bool showAlpha, bool showEyeDropper, bool overrideState = false) : base(value, overrideState)
		{
			this.hdr = hdr;
			this.showAlpha = showAlpha;
			this.showEyeDropper = showEyeDropper;
			this.overrideState = overrideState;
		}

		// Token: 0x040003C2 RID: 962
		public bool hdr;

		// Token: 0x040003C3 RID: 963
		[NonSerialized]
		public bool showAlpha = true;

		// Token: 0x040003C4 RID: 964
		[NonSerialized]
		public bool showEyeDropper = true;
	}
}
