using System;
using UnityEngine;

namespace MK.Toon
{
	// Token: 0x0200002C RID: 44
	public class Vector2Property : Property<Vector2>
	{
		// Token: 0x06000022 RID: 34 RVA: 0x00003319 File Offset: 0x00001519
		public Vector2Property(Uniform uniform) : base(uniform, Array.Empty<string>())
		{
		}

		// Token: 0x06000023 RID: 35 RVA: 0x00003327 File Offset: 0x00001527
		public override Vector2 GetValue(Material material)
		{
			return material.GetVector(this._uniform.id);
		}

		// Token: 0x06000024 RID: 36 RVA: 0x0000333F File Offset: 0x0000153F
		public override void SetValue(Material material, Vector2 value)
		{
			material.SetVector(this._uniform.id, value);
		}
	}
}
