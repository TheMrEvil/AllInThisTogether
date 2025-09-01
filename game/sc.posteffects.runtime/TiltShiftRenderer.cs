using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x0200004A RID: 74
	public sealed class TiltShiftRenderer : PostProcessEffectRenderer<TiltShift>
	{
		// Token: 0x060000DC RID: 220 RVA: 0x00008035 File Offset: 0x00006235
		public override void Init()
		{
			this.shader = Shader.Find("Hidden/SC Post Effects/Tilt Shift");
			this.screenCopyID = Shader.PropertyToID("_ScreenCopyTexture");
		}

		// Token: 0x060000DD RID: 221 RVA: 0x00008058 File Offset: 0x00006258
		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(this.shader);
			CommandBuffer command = context.command;
			propertySheet.properties.SetVector("_Params", new Vector4(base.settings.areaSize.value, base.settings.areaFalloff.value, base.settings.amount.value, (float)base.settings.mode.value));
			propertySheet.properties.SetFloat("_Offset", base.settings.offset.value);
			propertySheet.properties.SetFloat("_Angle", base.settings.angle.value * 0.017453292f);
			context.command.GetTemporaryRT(this.screenCopyID, context.width, context.height, 0, FilterMode.Bilinear, context.sourceFormat);
			int pass = (int)(base.settings.mode.value + (int)base.settings.quality.value);
			int value = (int)base.settings.mode.value;
			if (value != 0)
			{
				if (value == 1)
				{
					pass = (int)(2 + base.settings.quality.value);
				}
			}
			else
			{
				pass = (int)base.settings.quality.value;
			}
			command.BlitFullscreenTriangle(context.source, this.screenCopyID, propertySheet, pass, false, null, false);
			command.SetGlobalTexture("_BlurredTex", this.screenCopyID);
			command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, TiltShift.debug ? 5 : 4, false, null, false);
			command.ReleaseTemporaryRT(this.screenCopyID);
		}

		// Token: 0x060000DE RID: 222 RVA: 0x00008212 File Offset: 0x00006412
		public TiltShiftRenderer()
		{
		}

		// Token: 0x0400015D RID: 349
		private Shader shader;

		// Token: 0x0400015E RID: 350
		private int screenCopyID;

		// Token: 0x02000088 RID: 136
		private enum Pass
		{
			// Token: 0x040001E7 RID: 487
			FragHorizontal,
			// Token: 0x040001E8 RID: 488
			FragHorizontalHQ,
			// Token: 0x040001E9 RID: 489
			FragRadial,
			// Token: 0x040001EA RID: 490
			FragRadialHQ,
			// Token: 0x040001EB RID: 491
			FragBlend,
			// Token: 0x040001EC RID: 492
			FragDebug
		}
	}
}
