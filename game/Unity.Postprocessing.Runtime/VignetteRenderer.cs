using System;
using UnityEngine.Scripting;

namespace UnityEngine.Rendering.PostProcessing
{
	// Token: 0x0200003F RID: 63
	[Preserve]
	internal sealed class VignetteRenderer : PostProcessEffectRenderer<Vignette>
	{
		// Token: 0x060000CF RID: 207 RVA: 0x0000A390 File Offset: 0x00008590
		public override void Render(PostProcessRenderContext context)
		{
			PropertySheet uberSheet = context.uberSheet;
			uberSheet.EnableKeyword("VIGNETTE");
			uberSheet.properties.SetColor(ShaderIDs.Vignette_Color, base.settings.color.value);
			if (base.settings.mode == VignetteMode.Classic)
			{
				uberSheet.properties.SetFloat(ShaderIDs.Vignette_Mode, 0f);
				uberSheet.properties.SetVector(ShaderIDs.Vignette_Center, base.settings.center.value);
				float z = (1f - base.settings.roundness.value) * 6f + base.settings.roundness.value;
				uberSheet.properties.SetVector(ShaderIDs.Vignette_Settings, new Vector4(base.settings.intensity.value * 3f, base.settings.smoothness.value * 5f, z, base.settings.rounded.value ? 1f : 0f));
				return;
			}
			uberSheet.properties.SetFloat(ShaderIDs.Vignette_Mode, 1f);
			uberSheet.properties.SetTexture(ShaderIDs.Vignette_Mask, base.settings.mask.value);
			uberSheet.properties.SetFloat(ShaderIDs.Vignette_Opacity, Mathf.Clamp01(base.settings.opacity.value));
		}

		// Token: 0x060000D0 RID: 208 RVA: 0x0000A507 File Offset: 0x00008707
		public VignetteRenderer()
		{
		}
	}
}
