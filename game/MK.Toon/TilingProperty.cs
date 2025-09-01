using System;
using UnityEngine;

namespace MK.Toon
{
	// Token: 0x02000033 RID: 51
	public class TilingProperty : Property<Vector2>
	{
		// Token: 0x06000039 RID: 57 RVA: 0x00003570 File Offset: 0x00001770
		public TilingProperty(Uniform uniform) : base(uniform, Array.Empty<string>())
		{
		}

		// Token: 0x0600003A RID: 58 RVA: 0x0000357E File Offset: 0x0000177E
		public override Vector2 GetValue(Material material)
		{
			return material.GetTextureScale(base.uniform.id);
		}

		// Token: 0x0600003B RID: 59 RVA: 0x00003591 File Offset: 0x00001791
		public override void SetValue(Material material, Vector2 value)
		{
			material.SetTextureScale(base.uniform.id, value);
		}
	}
}
