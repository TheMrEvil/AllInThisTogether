using System;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x02000052 RID: 82
	[Serializable]
	public sealed class TextureParameter : ParameterOverride<Texture>
	{
		// Token: 0x06000116 RID: 278 RVA: 0x0000B388 File Offset: 0x00009588
		public override void Interp(Texture from, Texture to, float t)
		{
			if (from == null && to == null)
			{
				this.value = null;
				return;
			}
			if (from != null && to != null)
			{
				this.value = TextureLerper.instance.Lerp(from, to, t);
				return;
			}
			if (this.defaultState == TextureParameterDefault.Lut2D)
			{
				Texture lutStrip = RuntimeUtilities.GetLutStrip((from != null) ? from.height : to.height);
				if (from == null)
				{
					from = lutStrip;
				}
				if (to == null)
				{
					to = lutStrip;
				}
			}
			Color to2;
			switch (this.defaultState)
			{
			case TextureParameterDefault.Black:
				to2 = Color.black;
				break;
			case TextureParameterDefault.White:
				to2 = Color.white;
				break;
			case TextureParameterDefault.Transparent:
				to2 = Color.clear;
				break;
			case TextureParameterDefault.Lut2D:
			{
				Texture lutStrip2 = RuntimeUtilities.GetLutStrip((from != null) ? from.height : to.height);
				if (from == null)
				{
					from = lutStrip2;
				}
				if (to == null)
				{
					to = lutStrip2;
				}
				if (from.width != to.width || from.height != to.height)
				{
					this.value = null;
					return;
				}
				this.value = TextureLerper.instance.Lerp(from, to, t);
				return;
			}
			default:
				base.Interp(from, to, t);
				return;
			}
			if (from == null)
			{
				this.value = TextureLerper.instance.Lerp(to, to2, 1f - t);
				return;
			}
			this.value = TextureLerper.instance.Lerp(from, to2, t);
		}

		// Token: 0x06000117 RID: 279 RVA: 0x0000B4FF File Offset: 0x000096FF
		public TextureParameter()
		{
		}

		// Token: 0x0400016C RID: 364
		public TextureParameterDefault defaultState = TextureParameterDefault.Black;
	}
}
