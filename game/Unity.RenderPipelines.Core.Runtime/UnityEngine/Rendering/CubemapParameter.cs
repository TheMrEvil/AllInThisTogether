using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000E1 RID: 225
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class CubemapParameter : VolumeParameter<Texture>
	{
		// Token: 0x060006B5 RID: 1717 RVA: 0x0001DFFA File Offset: 0x0001C1FA
		public CubemapParameter(Texture value, bool overrideState = false) : base(value, overrideState)
		{
		}

		// Token: 0x060006B6 RID: 1718 RVA: 0x0001E004 File Offset: 0x0001C204
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
