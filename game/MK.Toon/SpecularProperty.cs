using System;
using UnityEngine;

namespace MK.Toon
{
	// Token: 0x02000035 RID: 53
	public class SpecularProperty : Property<Specular>
	{
		// Token: 0x0600003F RID: 63 RVA: 0x000035DA File Offset: 0x000017DA
		public SpecularProperty(Uniform uniform, params string[] keywords) : base(uniform, keywords)
		{
		}

		// Token: 0x06000040 RID: 64 RVA: 0x000035E4 File Offset: 0x000017E4
		public override Specular GetValue(Material material)
		{
			return (Specular)material.GetInt(this._uniform.id);
		}

		// Token: 0x06000041 RID: 65 RVA: 0x000035F8 File Offset: 0x000017F8
		public override void SetValue(Material material, Specular specular)
		{
			if (material.shader.name.Contains(Properties.shaderVariantSimpleName))
			{
				specular = ((specular >= Specular.Isotropic) ? Specular.Isotropic : Specular.Off);
			}
			material.SetInt(this._uniform.id, (int)specular);
			base.SetKeyword(material, specular > Specular.Off, (int)specular);
		}
	}
}
