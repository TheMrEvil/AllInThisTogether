using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000DD RID: 221
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class Texture2DParameter : VolumeParameter<Texture>
	{
		// Token: 0x060006AD RID: 1709 RVA: 0x0001DF0A File Offset: 0x0001C10A
		public Texture2DParameter(Texture value, bool overrideState = false) : base(value, overrideState)
		{
		}

		// Token: 0x060006AE RID: 1710 RVA: 0x0001DF14 File Offset: 0x0001C114
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
