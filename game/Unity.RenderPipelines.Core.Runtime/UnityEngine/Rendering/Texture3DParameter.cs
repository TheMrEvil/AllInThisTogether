using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000DE RID: 222
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class Texture3DParameter : VolumeParameter<Texture>
	{
		// Token: 0x060006AF RID: 1711 RVA: 0x0001DF46 File Offset: 0x0001C146
		public Texture3DParameter(Texture value, bool overrideState = false) : base(value, overrideState)
		{
		}

		// Token: 0x060006B0 RID: 1712 RVA: 0x0001DF50 File Offset: 0x0001C150
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
