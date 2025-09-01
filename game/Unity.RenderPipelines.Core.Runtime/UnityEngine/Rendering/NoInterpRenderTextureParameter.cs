using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000E0 RID: 224
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class NoInterpRenderTextureParameter : VolumeParameter<RenderTexture>
	{
		// Token: 0x060006B3 RID: 1715 RVA: 0x0001DFBE File Offset: 0x0001C1BE
		public NoInterpRenderTextureParameter(RenderTexture value, bool overrideState = false) : base(value, overrideState)
		{
		}

		// Token: 0x060006B4 RID: 1716 RVA: 0x0001DFC8 File Offset: 0x0001C1C8
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
