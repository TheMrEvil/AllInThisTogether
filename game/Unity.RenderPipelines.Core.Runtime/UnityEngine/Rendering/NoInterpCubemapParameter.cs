using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000E2 RID: 226
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpCubemapParameter : VolumeParameter<Cubemap>
	{
		// Token: 0x060006B7 RID: 1719 RVA: 0x0001E036 File Offset: 0x0001C236
		public NoInterpCubemapParameter(Cubemap value, bool overrideState = false) : base(value, overrideState)
		{
		}

		// Token: 0x060006B8 RID: 1720 RVA: 0x0001E040 File Offset: 0x0001C240
		public override int GetHashCode()
		{
			int result = base.GetHashCode();
			if (this.value != null)
			{
				result = 23 * CoreUtils.GetTextureHash(this.value);
			}
			return result;
		}
	}
}
