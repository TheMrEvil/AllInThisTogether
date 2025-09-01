using System;
using UnityEngine;

namespace MK.Toon
{
	// Token: 0x0200002D RID: 45
	public class Vector3Property : Property<Vector3>
	{
		// Token: 0x06000025 RID: 37 RVA: 0x00003358 File Offset: 0x00001558
		public Vector3Property(Uniform uniform) : base(uniform, Array.Empty<string>())
		{
		}

		// Token: 0x06000026 RID: 38 RVA: 0x00003366 File Offset: 0x00001566
		public override Vector3 GetValue(Material material)
		{
			return material.GetVector(this._uniform.id);
		}

		// Token: 0x06000027 RID: 39 RVA: 0x0000337E File Offset: 0x0000157E
		public override void SetValue(Material material, Vector3 value)
		{
			material.SetVector(this._uniform.id, value);
		}
	}
}
