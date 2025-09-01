using System;
using UnityEngine;

namespace MK.Toon
{
	// Token: 0x0200003A RID: 58
	public class BlendProperty : Property<Blend>
	{
		// Token: 0x06000050 RID: 80 RVA: 0x0000397A File Offset: 0x00001B7A
		public BlendProperty(Uniform uniform, params string[] keywords) : base(uniform, keywords)
		{
		}

		// Token: 0x06000051 RID: 81 RVA: 0x00003984 File Offset: 0x00001B84
		public override Blend GetValue(Material material)
		{
			return (Blend)material.GetInt(base.uniform.id);
		}

		// Token: 0x06000052 RID: 82 RVA: 0x00003998 File Offset: 0x00001B98
		public override void SetValue(Material material, Blend blend)
		{
			if (Properties.blend.GetValue(material) != Blend.Custom)
			{
				Properties.zTest.SetValue(material, ZTest.LessEqual);
				if (Properties.surface.GetValue(material) == Surface.Opaque)
				{
					Properties.blendSrc.SetValue(material, BlendFactor.One);
					Properties.blendDst.SetValue(material, BlendFactor.Zero);
					Properties.blendSrcAlpha.SetValue(material, BlendFactor.One);
					Properties.blendDstAlpha.SetValue(material, BlendFactor.Zero);
				}
				else
				{
					switch (blend)
					{
					case Blend.Alpha:
						Properties.blendSrc.SetValue(material, BlendFactor.SrcAlpha);
						Properties.blendDst.SetValue(material, BlendFactor.OneMinusSrcAlpha);
						Properties.blendSrcAlpha.SetValue(material, BlendFactor.One);
						Properties.blendDstAlpha.SetValue(material, BlendFactor.OneMinusSrcAlpha);
						break;
					case Blend.Premultiply:
						Properties.blendSrc.SetValue(material, BlendFactor.One);
						Properties.blendDst.SetValue(material, BlendFactor.OneMinusSrcAlpha);
						Properties.blendSrcAlpha.SetValue(material, BlendFactor.One);
						Properties.blendDstAlpha.SetValue(material, BlendFactor.OneMinusSrcAlpha);
						break;
					case Blend.Additive:
						Properties.blendSrc.SetValue(material, BlendFactor.SrcAlpha);
						Properties.blendDst.SetValue(material, BlendFactor.One);
						Properties.blendSrcAlpha.SetValue(material, BlendFactor.One);
						Properties.blendDstAlpha.SetValue(material, BlendFactor.One);
						break;
					case Blend.Multiply:
						Properties.blendSrc.SetValue(material, BlendFactor.DstColor);
						Properties.blendDst.SetValue(material, BlendFactor.Zero);
						Properties.blendSrcAlpha.SetValue(material, BlendFactor.Zero);
						Properties.blendDstAlpha.SetValue(material, BlendFactor.One);
						break;
					default:
						Properties.blendSrc.SetValue(material, BlendFactor.SrcAlpha);
						Properties.blendDst.SetValue(material, BlendFactor.OneMinusSrcAlpha);
						Properties.blendSrcAlpha.SetValue(material, BlendFactor.One);
						Properties.blendDstAlpha.SetValue(material, BlendFactor.OneMinusSrcAlpha);
						break;
					}
				}
				material.SetInt(Properties.blend.uniform.id, (int)blend);
				base.SetKeyword(material, blend > Blend.Alpha, (int)blend);
				return;
			}
			material.SetInt(Properties.blend.uniform.id, (int)blend);
			base.SetKeyword(material, blend > Blend.Alpha, (int)blend);
		}
	}
}
