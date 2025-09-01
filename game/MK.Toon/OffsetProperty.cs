using System;
using UnityEngine;

namespace MK.Toon
{
	// Token: 0x02000034 RID: 52
	public class OffsetProperty : Property<Vector2>
	{
		// Token: 0x0600003C RID: 60 RVA: 0x000035A5 File Offset: 0x000017A5
		public OffsetProperty(Uniform uniform) : base(uniform, Array.Empty<string>())
		{
		}

		// Token: 0x0600003D RID: 61 RVA: 0x000035B3 File Offset: 0x000017B3
		public override Vector2 GetValue(Material material)
		{
			return material.GetTextureOffset(base.uniform.id);
		}

		// Token: 0x0600003E RID: 62 RVA: 0x000035C6 File Offset: 0x000017C6
		public override void SetValue(Material material, Vector2 value)
		{
			material.SetTextureOffset(base.uniform.id, value);
		}
	}
}
