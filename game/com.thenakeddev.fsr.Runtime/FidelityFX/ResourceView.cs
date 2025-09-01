using System;
using UnityEngine.Rendering;

namespace FidelityFX
{
	// Token: 0x02000003 RID: 3
	public readonly struct ResourceView
	{
		// Token: 0x06000009 RID: 9 RVA: 0x000021FE File Offset: 0x000003FE
		public ResourceView(in RenderTargetIdentifier renderTarget, RenderTextureSubElement subElement = RenderTextureSubElement.Default, int mipLevel = 0)
		{
			this.RenderTarget = renderTarget;
			this.SubElement = subElement;
			this.MipLevel = mipLevel;
		}

		// Token: 0x17000001 RID: 1
		// (get) Token: 0x0600000A RID: 10 RVA: 0x0000221C File Offset: 0x0000041C
		public bool IsValid
		{
			get
			{
				return !this.RenderTarget.Equals(default(RenderTargetIdentifier));
			}
		}

		// Token: 0x0600000B RID: 11 RVA: 0x00002244 File Offset: 0x00000444
		// Note: this type is marked as 'beforefieldinit'.
		static ResourceView()
		{
			RenderTargetIdentifier renderTargetIdentifier = default(RenderTargetIdentifier);
			ResourceView.Unassigned = new ResourceView(ref renderTargetIdentifier, RenderTextureSubElement.Default, 0);
			renderTargetIdentifier = BuiltinRenderTextureType.None;
			ResourceView.None = new ResourceView(ref renderTargetIdentifier, RenderTextureSubElement.Default, 0);
		}

		// Token: 0x04000001 RID: 1
		public static readonly ResourceView Unassigned;

		// Token: 0x04000002 RID: 2
		public static readonly ResourceView None;

		// Token: 0x04000003 RID: 3
		public readonly RenderTargetIdentifier RenderTarget;

		// Token: 0x04000004 RID: 4
		public readonly RenderTextureSubElement SubElement;

		// Token: 0x04000005 RID: 5
		public readonly int MipLevel;
	}
}
