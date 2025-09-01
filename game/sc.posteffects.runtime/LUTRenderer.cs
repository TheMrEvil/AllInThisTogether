using System;
using UnityEngine;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000029 RID: 41
	public sealed class LUTRenderer : PostProcessEffectRenderer<LUT>
	{
		// Token: 0x0600007F RID: 127 RVA: 0x00005F89 File Offset: 0x00004189
		public override void Init()
		{
			this.shader = Shader.Find("Hidden/SC Post Effects/LUT");
		}

		// Token: 0x06000080 RID: 128 RVA: 0x00005F9B File Offset: 0x0000419B
		public override void Release()
		{
			base.Release();
		}

		// Token: 0x06000081 RID: 129 RVA: 0x00005FA4 File Offset: 0x000041A4
		public override void Render(PostProcessRenderContext context)
		{
			if (LUT.Bypass)
			{
				return;
			}
			PropertySheet propertySheet = context.propertySheets.Get(this.shader);
			if (base.settings.lutNear.value)
			{
				propertySheet.properties.SetTexture("_LUT_Near", base.settings.lutNear);
				propertySheet.properties.SetVector("_LUT_Params", new Vector4(1f / (float)base.settings.lutNear.value.width, 1f / (float)base.settings.lutNear.value.height, (float)base.settings.lutNear.value.height - 1f, base.settings.intensity));
			}
			if (base.settings.mode.value == LUT.Mode.DistanceBased)
			{
				context.command.SetGlobalVector("_FadeParams", new Vector4(base.settings.startFadeDistance.value, base.settings.endFadeDistance.value, 0f, 0f));
				if (base.settings.lutFar.value)
				{
					propertySheet.properties.SetTexture("_LUT_Far", base.settings.lutFar);
				}
			}
			propertySheet.properties.SetFloat("_Invert", base.settings.invert);
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, (int)base.settings.mode.value, false, null, false);
		}

		// Token: 0x06000082 RID: 130 RVA: 0x0000615A File Offset: 0x0000435A
		public LUTRenderer()
		{
		}

		// Token: 0x040000CB RID: 203
		private Shader shader;
	}
}
