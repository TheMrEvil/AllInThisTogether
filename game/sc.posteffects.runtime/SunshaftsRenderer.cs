using System;
using UnityEngine;
using UnityEngine.Rendering;
using UnityEngine.Rendering.PostProcessing;

namespace SCPE
{
	// Token: 0x02000048 RID: 72
	public sealed class SunshaftsRenderer : PostProcessEffectRenderer<Sunshafts>
	{
		// Token: 0x060000D6 RID: 214 RVA: 0x00007B0C File Offset: 0x00005D0C
		public override void Init()
		{
			this.shader = Shader.Find("Hidden/SC Post Effects/Sun Shafts");
			this.skyboxBufferID = Shader.PropertyToID("_SkyboxBuffer");
		}

		// Token: 0x060000D7 RID: 215 RVA: 0x00007B2E File Offset: 0x00005D2E
		public override void Release()
		{
			base.Release();
		}

		// Token: 0x060000D8 RID: 216 RVA: 0x00007B38 File Offset: 0x00005D38
		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet propertySheet = context.propertySheets.Get(this.shader);
			CommandBuffer command = context.command;
			float z = base.settings.useCasterIntensity ? SunshaftCaster.intensity : base.settings.sunShaftIntensity.value;
			Vector3 vector = Vector3.one * 0.5f;
			if (Sunshafts.sunPosition != Vector3.zero)
			{
				vector = context.camera.WorldToViewportPoint(Sunshafts.sunPosition);
			}
			else
			{
				vector = new Vector3(0.5f, 0.5f, 0f);
			}
			propertySheet.properties.SetVector("_SunPosition", new Vector4(vector.x, vector.y, z, base.settings.falloff));
			Color color = base.settings.useCasterColor ? SunshaftCaster.color : base.settings.sunColor.value;
			propertySheet.properties.SetFloat("_BlendMode", (float)base.settings.blendMode.value);
			propertySheet.properties.SetColor("_SunColor", (vector.z >= 0f) ? color : new Color(0f, 0f, 0f, 0f));
			propertySheet.properties.SetColor("_SunThreshold", base.settings.sunThreshold);
			int value = (int)base.settings.resolution.value;
			context.command.GetTemporaryRT(this.skyboxBufferID, context.width / 2, context.height / 2, 0, FilterMode.Bilinear, context.sourceFormat);
			context.command.BlitFullscreenTriangle(context.source, this.skyboxBufferID, propertySheet, 0, false, null, false);
			command.SetGlobalTexture("_SunshaftBuffer", this.skyboxBufferID);
			command.BeginSample("Sunshafts blur");
			int nameID = Shader.PropertyToID("_Temp1");
			int nameID2 = Shader.PropertyToID("_Temp2");
			command.GetTemporaryRT(nameID, context.width / value, context.height / value, 0, FilterMode.Bilinear);
			command.GetTemporaryRT(nameID2, context.width / value, context.height / value, 0, FilterMode.Bilinear);
			command.Blit(this.skyboxBufferID, nameID);
			float value2 = base.settings.length * 0.0013020834f;
			int num = base.settings.highQuality ? 2 : 1;
			float num2 = base.settings.highQuality ? (base.settings.length / 2.5f) : base.settings.length;
			for (int i = 0; i < num; i++)
			{
				context.command.BlitFullscreenTriangle(nameID, nameID2, propertySheet, 1, false, null, false);
				value2 = num2 * (((float)i * 2f + 1f) * 6f) / (float)context.screenWidth;
				propertySheet.properties.SetFloat("_BlurRadius", value2);
				context.command.BlitFullscreenTriangle(nameID2, nameID, propertySheet, 1, false, null, false);
				value2 = num2 * (((float)i * 2f + 2f) * 6f) / (float)context.screenWidth;
				propertySheet.properties.SetFloat("_BlurRadius", value2);
			}
			command.EndSample("Sunshafts blur");
			command.SetGlobalTexture("_SunshaftBuffer", nameID);
			context.command.BlitFullscreenTriangle(context.source, context.destination, propertySheet, 2, false, null, false);
			command.ReleaseTemporaryRT(nameID);
			command.ReleaseTemporaryRT(nameID2);
			command.ReleaseTemporaryRT(this.skyboxBufferID);
		}

		// Token: 0x060000D9 RID: 217 RVA: 0x00007F2F File Offset: 0x0000612F
		public SunshaftsRenderer()
		{
		}

		// Token: 0x04000153 RID: 339
		private Shader shader;

		// Token: 0x04000154 RID: 340
		private int skyboxBufferID;

		// Token: 0x02000083 RID: 131
		private enum Pass
		{
			// Token: 0x040001DD RID: 477
			SkySource,
			// Token: 0x040001DE RID: 478
			RadialBlur,
			// Token: 0x040001DF RID: 479
			Blend
		}
	}
}
