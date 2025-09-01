using System;
using UnityEngine;

namespace MK.Toon
{
	// Token: 0x02000027 RID: 39
	public class BoolProperty : Property<bool>
	{
		// Token: 0x0600000C RID: 12 RVA: 0x00003084 File Offset: 0x00001284
		public BoolProperty(Uniform uniform, string keyword) : base(uniform, new string[]
		{
			keyword
		})
		{
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00003097 File Offset: 0x00001297
		public BoolProperty(Uniform uniform) : base(uniform, Array.Empty<string>())
		{
		}

		// Token: 0x0600000E RID: 14 RVA: 0x000030A5 File Offset: 0x000012A5
		public override bool GetValue(Material material)
		{
			return material.GetInt(this._uniform.id) > 0;
		}

		// Token: 0x0600000F RID: 15 RVA: 0x000030BE File Offset: 0x000012BE
		public override void SetValue(Material material, bool value)
		{
			material.SetInt(this._uniform.id, value ? 1 : 0);
			base.SetKeyword(material, value, 0);
		}
	}
}
