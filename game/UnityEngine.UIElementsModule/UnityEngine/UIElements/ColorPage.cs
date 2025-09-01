using System;
using UnityEngine.UIElements.UIR;

namespace UnityEngine.UIElements
{
	// Token: 0x0200025E RID: 606
	internal struct ColorPage
	{
		// Token: 0x0600127C RID: 4732 RVA: 0x000492B4 File Offset: 0x000474B4
		public static ColorPage Init(RenderChain renderChain, BMPAlloc alloc)
		{
			bool flag = alloc.IsValid();
			return new ColorPage
			{
				isValid = flag,
				pageAndID = (flag ? renderChain.shaderInfoAllocator.ColorAllocToVertexData(alloc) : default(Color32))
			};
		}

		// Token: 0x04000863 RID: 2147
		public bool isValid;

		// Token: 0x04000864 RID: 2148
		public Color32 pageAndID;
	}
}
