using System;
using UnityEngine;

namespace MK.Toon
{
	// Token: 0x02000030 RID: 48
	public class TextureProperty : Property<Texture>
	{
		// Token: 0x0600002F RID: 47 RVA: 0x0000342A File Offset: 0x0000162A
		public TextureProperty(Uniform uniform, string keyword) : base(uniform, new string[]
		{
			keyword
		})
		{
		}

		// Token: 0x06000030 RID: 48 RVA: 0x0000343D File Offset: 0x0000163D
		public TextureProperty(Uniform uniform) : base(uniform, Array.Empty<string>())
		{
		}

		// Token: 0x06000031 RID: 49 RVA: 0x0000344B File Offset: 0x0000164B
		public override Texture GetValue(Material material)
		{
			return material.GetTexture(this._uniform.id);
		}

		// Token: 0x06000032 RID: 50 RVA: 0x0000345E File Offset: 0x0000165E
		public override void SetValue(Material material, Texture texture)
		{
			material.SetTexture(this._uniform.id, texture);
			base.SetKeyword(material, texture != null, 0);
		}
	}
}
