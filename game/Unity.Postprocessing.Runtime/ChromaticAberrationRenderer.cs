using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x0200001A RID: 26
	[Preserve]
	internal sealed class ChromaticAberrationRenderer : PostProcessEffectRenderer<ChromaticAberration>
	{
		// Token: 0x06000030 RID: 48 RVA: 0x000032FC File Offset: 0x000014FC
		public override void Render(PostProcessRenderContext context)
		{
			Texture texture = base.settings.spectralLut.value;
			if (texture == null)
			{
				if (this.m_InternalSpectralLut == null)
				{
					this.m_InternalSpectralLut = new Texture2D(3, 1, TextureFormat.RGB24, false)
					{
						name = "Chromatic Aberration Spectrum Lookup",
						filterMode = FilterMode.Bilinear,
						wrapMode = TextureWrapMode.Clamp,
						anisoLevel = 0,
						hideFlags = HideFlags.DontSave
					};
					this.m_InternalSpectralLut.SetPixels(new Color[]
					{
						new Color(1f, 0f, 0f),
						new Color(0f, 1f, 0f),
						new Color(0f, 0f, 1f)
					});
					this.m_InternalSpectralLut.Apply();
				}
				texture = this.m_InternalSpectralLut;
			}
			PropertySheet uberSheet = context.uberSheet;
			uberSheet.EnableKeyword((base.settings.fastMode || SystemInfo.graphicsDeviceType == GraphicsDeviceType.OpenGLES2) ? "CHROMATIC_ABERRATION_LOW" : "CHROMATIC_ABERRATION");
			uberSheet.properties.SetFloat(ShaderIDs.ChromaticAberration_Amount, base.settings.intensity * 0.05f);
			uberSheet.properties.SetTexture(ShaderIDs.ChromaticAberration_SpectralLut, texture);
		}

		// Token: 0x06000031 RID: 49 RVA: 0x00003451 File Offset: 0x00001651
		public override void Release()
		{
			RuntimeUtilities.Destroy(this.m_InternalSpectralLut);
			this.m_InternalSpectralLut = null;
		}

		// Token: 0x06000032 RID: 50 RVA: 0x00003465 File Offset: 0x00001665
		public ChromaticAberrationRenderer()
		{
		}

		// Token: 0x04000056 RID: 86
		private Texture2D m_InternalSpectralLut;
	}
}
