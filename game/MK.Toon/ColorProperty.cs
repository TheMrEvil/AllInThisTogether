using System;
using UnityEngine;

namespace MK.Toon
{
	// Token: 0x0200002F RID: 47
	public class ColorProperty : Property<Color>
	{
		// Token: 0x0600002B RID: 43 RVA: 0x000033CC File Offset: 0x000015CC
		public ColorProperty(Uniform uniform, string keyword) : base(uniform, new string[]
		{
			keyword
		})
		{
		}

		// Token: 0x0600002C RID: 44 RVA: 0x000033DF File Offset: 0x000015DF
		public ColorProperty(Uniform uniform) : base(uniform, Array.Empty<string>())
		{
		}

		// Token: 0x0600002D RID: 45 RVA: 0x000033ED File Offset: 0x000015ED
		public override Color GetValue(Material material)
		{
			return material.GetColor(this._uniform.id);
		}

		// Token: 0x0600002E RID: 46 RVA: 0x00003400 File Offset: 0x00001600
		public override void SetValue(Material material, Color color)
		{
			material.SetColor(this._uniform.id, color);
			base.SetKeyword(material, color.maxColorComponent > 0f, 0);
		}
	}
}
