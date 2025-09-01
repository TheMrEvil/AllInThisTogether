using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000DF RID: 223
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class RenderTextureParameter : VolumeParameter<RenderTexture>
	{
		// Token: 0x060006B1 RID: 1713 RVA: 0x0001DF82 File Offset: 0x0001C182
		public RenderTextureParameter(RenderTexture value, bool overrideState = false) : base(value, overrideState)
		{
		}

		// Token: 0x060006B2 RID: 1714 RVA: 0x0001DF8C File Offset: 0x0001C18C
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
