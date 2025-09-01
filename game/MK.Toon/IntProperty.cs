using System;
using UnityEngine;

namespace MK.Toon
{
	// Token: 0x02000028 RID: 40
	public class IntProperty : Property<int>
	{
		// Token: 0x06000010 RID: 16 RVA: 0x000030E1 File Offset: 0x000012E1
		public IntProperty(Uniform uniform, string keyword, int keywordDisabled = 0) : base(uniform, new string[]
		{
			keyword
		})
		{
		}

		// Token: 0x06000011 RID: 17 RVA: 0x000030F4 File Offset: 0x000012F4
		public IntProperty(Uniform uniform) : base(uniform, Array.Empty<string>())
		{
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00003102 File Offset: 0x00001302
		public override int GetValue(Material material)
		{
			return material.GetInt(this._uniform.id);
		}

		// Token: 0x06000013 RID: 19 RVA: 0x00003115 File Offset: 0x00001315
		public override void SetValue(Material material, int value)
		{
			material.SetInt(this._uniform.id, value);
			base.SetKeyword(material, value != this._keywordDisabled, value);
		}

		// Token: 0x04000185 RID: 389
		private int _keywordDisabled;
	}
}
