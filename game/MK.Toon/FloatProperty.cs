using System;
using UnityEngine;

namespace MK.Toon
{
	// Token: 0x0200002A RID: 42
	public class FloatProperty : Property<float>
	{
		// Token: 0x06000018 RID: 24 RVA: 0x000031D2 File Offset: 0x000013D2
		public FloatProperty(Uniform uniform, string keyword, float keywordDisabled = 0f) : base(uniform, new string[]
		{
			keyword
		})
		{
			this._keywordDisabled = keywordDisabled;
		}

		// Token: 0x06000019 RID: 25 RVA: 0x000031EC File Offset: 0x000013EC
		public FloatProperty(Uniform uniform) : base(uniform, Array.Empty<string>())
		{
		}

		// Token: 0x0600001A RID: 26 RVA: 0x000031FA File Offset: 0x000013FA
		public override float GetValue(Material material)
		{
			return material.GetFloat(this._uniform.id);
		}

		// Token: 0x0600001B RID: 27 RVA: 0x0000320D File Offset: 0x0000140D
		public override void SetValue(Material material, float value)
		{
			material.SetFloat(this._uniform.id, value);
			base.SetKeyword(material, value != this._keywordDisabled, (int)value);
		}

		// Token: 0x04000189 RID: 393
		private float _keywordDisabled;
	}
}
