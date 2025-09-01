using System;
using UnityEngine;

namespace MK.Toon
{
	// Token: 0x0200002E RID: 46
	public class Vector4Property : Property<Vector4>
	{
		// Token: 0x06000028 RID: 40 RVA: 0x00003397 File Offset: 0x00001597
		public Vector4Property(Uniform uniform) : base(uniform, Array.Empty<string>())
		{
		}

		// Token: 0x06000029 RID: 41 RVA: 0x000033A5 File Offset: 0x000015A5
		public override Vector4 GetValue(Material material)
		{
			return material.GetVector(this._uniform.id);
		}

		// Token: 0x0600002A RID: 42 RVA: 0x000033B8 File Offset: 0x000015B8
		public override void SetValue(Material material, Vector4 value)
		{
			material.SetVector(this._uniform.id, value);
		}
	}
}
