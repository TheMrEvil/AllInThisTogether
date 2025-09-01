using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x0200002C RID: 44
	[Preserve]
	internal sealed class GrainRenderer : PostProcessEffectRenderer<Grain>
	{
		// Token: 0x06000082 RID: 130 RVA: 0x00006FD8 File Offset: 0x000051D8
		public override void Render(PostProcessRenderContext context)
		{
			float realtimeSinceStartup = Time.realtimeSinceStartup;
			float z = HaltonSeq.Get(this.m_SampleIndex & 1023, 2);
			float w = HaltonSeq.Get(this.m_SampleIndex & 1023, 3);
			int num = this.m_SampleIndex + 1;
			this.m_SampleIndex = num;
			if (num >= 1024)
			{
				this.m_SampleIndex = 0;
			}
			if (this.m_GrainLookupRT == null || !this.m_GrainLookupRT.IsCreated())
			{
				RuntimeUtilities.Destroy(this.m_GrainLookupRT);
				this.m_GrainLookupRT = new RenderTexture(128, 128, 0, this.GetLookupFormat())
				{
					filterMode = FilterMode.Bilinear,
					wrapMode = TextureWrapMode.Repeat,
					anisoLevel = 0,
					name = "Grain Lookup Texture"
				};
				this.m_GrainLookupRT.Create();
			}
			PropertySheet propertySheet = context.propertySheets.Get(context.resources.shaders.grainBaker);
			propertySheet.properties.Clear();
			propertySheet.properties.SetFloat(ShaderIDs.Phase, realtimeSinceStartup % 10f);
			propertySheet.properties.SetVector(ShaderIDs.GrainNoiseParameters, new Vector3(12.9898f, 78.233f, 43758.547f));
			context.command.BeginSample("GrainLookup");
			context.command.BlitFullscreenTriangle(BuiltinRenderTextureType.None, this.m_GrainLookupRT, propertySheet, base.settings.colored.value ? 1 : 0, false, null, false);
			context.command.EndSample("GrainLookup");
			PropertySheet uberSheet = context.uberSheet;
			uberSheet.EnableKeyword("GRAIN");
			uberSheet.properties.SetTexture(ShaderIDs.GrainTex, this.m_GrainLookupRT);
			uberSheet.properties.SetVector(ShaderIDs.Grain_Params1, new Vector2(base.settings.lumContrib.value, base.settings.intensity.value * 20f));
			uberSheet.properties.SetVector(ShaderIDs.Grain_Params2, new Vector4((float)context.width / (float)this.m_GrainLookupRT.width / base.settings.size.value, (float)context.height / (float)this.m_GrainLookupRT.height / base.settings.size.value, z, w));
		}

		// Token: 0x06000083 RID: 131 RVA: 0x0000722E File Offset: 0x0000542E
		private RenderTextureFormat GetLookupFormat()
		{
			if (RenderTextureFormat.ARGBHalf.IsSupported())
			{
				return RenderTextureFormat.ARGBHalf;
			}
			return RenderTextureFormat.ARGB32;
		}

		// Token: 0x06000084 RID: 132 RVA: 0x0000723B File Offset: 0x0000543B
		public override void Release()
		{
			RuntimeUtilities.Destroy(this.m_GrainLookupRT);
			this.m_GrainLookupRT = null;
			this.m_SampleIndex = 0;
		}

		// Token: 0x06000085 RID: 133 RVA: 0x00007256 File Offset: 0x00005456
		public GrainRenderer()
		{
		}

		// Token: 0x040000F4 RID: 244
		private RenderTexture m_GrainLookupRT;

		// Token: 0x040000F5 RID: 245
		private const int k_SampleCount = 1024;

		// Token: 0x040000F6 RID: 246
		private int m_SampleIndex;
	}
}
