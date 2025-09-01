using System;
using UnityEngine;

namespace MK.Toon
{
	// Token: 0x02000037 RID: 55
	public class StencilModeProperty : Property<Stencil>
	{
		// Token: 0x06000045 RID: 69 RVA: 0x000036B0 File Offset: 0x000018B0
		public StencilModeProperty(Uniform uniform) : base(uniform, Array.Empty<string>())
		{
		}

		// Token: 0x06000046 RID: 70 RVA: 0x000036BE File Offset: 0x000018BE
		public override Stencil GetValue(Material material)
		{
			return (Stencil)material.GetInt(this._uniform.id);
		}

		// Token: 0x06000047 RID: 71 RVA: 0x000036D4 File Offset: 0x000018D4
		public override void SetValue(Material material, Stencil stencil)
		{
			if (stencil == Stencil.Builtin)
			{
				Properties.stencilRef.SetValue(material, 0);
				Properties.stencilReadMask.SetValue(material, 255);
				Properties.stencilWriteMask.SetValue(material, 255);
				Properties.stencilComp.SetValue(material, StencilComparison.Always);
				Properties.stencilPass.SetValue(material, StencilOperation.Keep);
				Properties.stencilFail.SetValue(material, StencilOperation.Keep);
				Properties.stencilZFail.SetValue(material, StencilOperation.Keep);
			}
			material.SetInt(this._uniform.id, (int)stencil);
		}
	}
}
