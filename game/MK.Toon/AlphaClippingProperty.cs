using System;
using UnityEngine;

namespace MK.Toon
{
	// Token: 0x02000032 RID: 50
	public class AlphaClippingProperty : Property<bool>
	{
		// Token: 0x06000036 RID: 54 RVA: 0x000034E6 File Offset: 0x000016E6
		public AlphaClippingProperty(Uniform uniform, string keyword) : base(uniform, new string[]
		{
			keyword
		})
		{
		}

		// Token: 0x06000037 RID: 55 RVA: 0x000034F9 File Offset: 0x000016F9
		public override bool GetValue(Material material)
		{
			return material.GetInt(this._uniform.id) > 0;
		}

		// Token: 0x06000038 RID: 56 RVA: 0x00003514 File Offset: 0x00001714
		public override void SetValue(Material material, bool value)
		{
			material.SetInt(this._uniform.id, value ? 1 : 0);
			base.SetKeyword(material, value, 0);
			Properties.surface.SetValue(material, Properties.surface.GetValue(material), value);
			Properties.renderPriority.SetValue(material, Properties.renderPriority.GetValue(material), value);
		}
	}
}
