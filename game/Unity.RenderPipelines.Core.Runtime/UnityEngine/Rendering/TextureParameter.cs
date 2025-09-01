using System;
using System.Diagnostics;

namespace UnityEngine.Rendering
{
	// Token: 0x020000DB RID: 219
	[DebuggerDisplay("{m_Value} ({m_OverrideState})")]
	[Serializable]
	public class TextureParameter : VolumeParameter<Texture>
	{
		// Token: 0x060006A8 RID: 1704 RVA: 0x0001DE7F File Offset: 0x0001C07F
		public TextureParameter(Texture value, bool overrideState = false) : this(value, TextureDimension.Any, overrideState)
		{
		}

		// Token: 0x060006A9 RID: 1705 RVA: 0x0001DE8A File Offset: 0x0001C08A
		public TextureParameter(Texture value, TextureDimension dimension, bool overrideState = false) : base(value, overrideState)
		{
			this.dimension = dimension;
		}

		// Token: 0x060006AA RID: 1706 RVA: 0x0001DE9C File Offset: 0x0001C09C
		public override int GetHashCode()
		{
			int result = base.GetHashCode();
			if (this.value != null)
			{
				result = 23 * CoreUtils.GetTextureHash(this.value);
			}
			return result;
		}

		// Token: 0x040003C5 RID: 965
		public TextureDimension dimension;
	}
}
