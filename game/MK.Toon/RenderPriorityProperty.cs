using System;
using UnityEngine;

namespace MK.Toon
{
	// Token: 0x02000038 RID: 56
	public class RenderPriorityProperty : Property<int, bool>
	{
		// Token: 0x06000048 RID: 72 RVA: 0x00003753 File Offset: 0x00001953
		public RenderPriorityProperty(Uniform uniform) : base(uniform, Array.Empty<string>())
		{
		}

		// Token: 0x06000049 RID: 73 RVA: 0x00003761 File Offset: 0x00001961
		public override int GetValue(Material material)
		{
			return material.GetInt(base.uniform.id);
		}

		// Token: 0x0600004A RID: 74 RVA: 0x00003774 File Offset: 0x00001974
		public override void SetValue(Material material, int priority)
		{
			this.SetValue(material, priority, false);
		}

		// Token: 0x0600004B RID: 75 RVA: 0x00003780 File Offset: 0x00001980
		public override void SetValue(Material material, int priority, bool alphaClipping)
		{
			if (Properties.surface.GetValue(material) != Surface.Transparent)
			{
				if (alphaClipping)
				{
					material.renderQueue = 2450;
				}
				else
				{
					material.renderQueue = 2000;
				}
			}
			else
			{
				material.renderQueue = 3000;
			}
			material.SetInt(base.uniform.id, priority);
			material.renderQueue -= Properties.renderPriority.GetValue(material);
		}
	}
}
