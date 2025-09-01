using System;
using UnityEngine;

namespace MK.Toon
{
	// Token: 0x02000039 RID: 57
	public class SurfaceProperty : Property<Surface, bool>
	{
		// Token: 0x0600004C RID: 76 RVA: 0x000037ED File Offset: 0x000019ED
		public SurfaceProperty(Uniform uniform, params string[] keywords) : base(uniform, keywords)
		{
		}

		// Token: 0x0600004D RID: 77 RVA: 0x000037F7 File Offset: 0x000019F7
		public override Surface GetValue(Material material)
		{
			return (Surface)material.GetInt(base.uniform.id);
		}

		// Token: 0x0600004E RID: 78 RVA: 0x0000380A File Offset: 0x00001A0A
		public override void SetValue(Material material, Surface surface)
		{
			this.SetValue(material, surface, false);
		}

		// Token: 0x0600004F RID: 79 RVA: 0x00003818 File Offset: 0x00001A18
		public override void SetValue(Material material, Surface surface, bool alphaClipping)
		{
			if (material.shader.name.Contains(Properties.shaderComponentOutlineName))
			{
				surface = Surface.Opaque;
			}
			if (material.shader.name.Contains(Properties.shaderComponentRefractionName))
			{
				surface = Surface.Transparent;
			}
			bool flag = Properties.blend.GetValue(material) == Blend.Custom;
			if (surface != Surface.Transparent)
			{
				if (!flag)
				{
					Properties.zWrite.SetValue(material, ZWrite.On);
					Properties.zTest.SetValue(material, ZTest.LessEqual);
				}
				if (alphaClipping)
				{
					material.SetOverrideTag("RenderType", "TransparentCutout");
					material.SetOverrideTag("IgnoreProjector", "true");
				}
				else
				{
					material.SetOverrideTag("RenderType", "Opaque");
					material.SetOverrideTag("IgnoreProjector", "false");
				}
				material.SetShaderPassEnabled("ShadowCaster", true);
			}
			else
			{
				if (!flag)
				{
					Properties.zWrite.SetValue(material, ZWrite.Off);
					Properties.zTest.SetValue(material, ZTest.LessEqual);
				}
				material.SetOverrideTag("RenderType", "Transparent");
				material.SetOverrideTag("IgnoreProjector", "true");
				material.SetShaderPassEnabled("ShadowCaster", false);
			}
			Properties.blend.SetValue(material, Properties.blend.GetValue(material));
			Properties.renderPriority.SetValue(material, Properties.renderPriority.GetValue(material), Properties.alphaClipping.GetValue(material));
			material.SetInt(base.uniform.id, (int)surface);
			base.SetKeyword(material, surface > Surface.Opaque, (int)surface);
		}
	}
}
