using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000DC RID: 220
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpTextureParameter : VolumeParameter<Texture>
	{
		// Token: 0x060006AB RID: 1707 RVA: 0x0001DECE File Offset: 0x0001C0CE
		public NoInterpTextureParameter(Texture value, bool overrideState = false) : base(value, overrideState)
		{
		}

		// Token: 0x060006AC RID: 1708 RVA: 0x0001DED8 File Offset: 0x0001C0D8
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
