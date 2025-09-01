using System;
using System.Collections.Generic;

namespace UnityEngine.UIElements.UIR
{
	// Token: 0x0200034A RID: 842
	internal class DrawParams
	{
		// Token: 0x06001B03 RID: 6915 RVA: 0x00077E20 File Offset: 0x00076020
		public void Reset()
		{
			this.view.Clear();
			this.view.Push(Matrix4x4.identity);
			this.scissor.Clear();
			this.scissor.Push(DrawParams.k_UnlimitedRect);
			this.renderTexture.Clear();
			this.defaultMaterial.Clear();
		}

		// Token: 0x06001B04 RID: 6916 RVA: 0x00077E80 File Offset: 0x00076080
		public DrawParams()
		{
		}

		// Token: 0x06001B05 RID: 6917 RVA: 0x00077EB9 File Offset: 0x000760B9
		// Note: this type is marked as 'beforefieldinit'.
		static DrawParams()
		{
		}

		// Token: 0x04000D01 RID: 3329
		internal static readonly Rect k_UnlimitedRect = new Rect(-100000f, -100000f, 200000f, 200000f);

		// Token: 0x04000D02 RID: 3330
		internal static readonly Rect k_FullNormalizedRect = new Rect(-1f, -1f, 2f, 2f);

		// Token: 0x04000D03 RID: 3331
		internal readonly Stack<Matrix4x4> view = new Stack<Matrix4x4>(8);

		// Token: 0x04000D04 RID: 3332
		internal readonly Stack<Rect> scissor = new Stack<Rect>(8);

		// Token: 0x04000D05 RID: 3333
		internal readonly List<RenderTexture> renderTexture = new List<RenderTexture>(8);

		// Token: 0x04000D06 RID: 3334
		internal readonly List<Material> defaultMaterial = new List<Material>(8);
	}
}
