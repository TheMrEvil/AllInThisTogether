using System;

namespace UnityEngine.Rendering
{
	// Token: 0x020000B2 RID: 178
	[Serializable]
	public class TextureCurveParameter : VolumeParameter<TextureCurve>
	{
		// Token: 0x06000608 RID: 1544 RVA: 0x0001C371 File Offset: 0x0001A571
		public TextureCurveParameter(TextureCurve value, bool overrideState = false) : base(value, overrideState)
		{
		}

		// Token: 0x06000609 RID: 1545 RVA: 0x0001C37B File Offset: 0x0001A57B
		public override void Release()
		{
			this.m_Value.Release();
		}
	}
}
