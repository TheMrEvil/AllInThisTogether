using System;

namespace UnityEngine.UI
{
	// Token: 0x02000043 RID: 67
	[AddComponentMenu("UI/Effects/Position As UV1", 82)]
	public class PositionAsUV1 : BaseMeshEffect
	{
		// Token: 0x060004AF RID: 1199 RVA: 0x000168BD File Offset: 0x00014ABD
		protected PositionAsUV1()
		{
		}

		// Token: 0x060004B0 RID: 1200 RVA: 0x000168C8 File Offset: 0x00014AC8
		public override void ModifyMesh(VertexHelper vh)
		{
			UIVertex uivertex = default(UIVertex);
			for (int i = 0; i < vh.currentVertCount; i++)
			{
				vh.PopulateUIVertex(ref uivertex, i);
				uivertex.uv1 = new Vector2(uivertex.position.x, uivertex.position.y);
				vh.SetUIVertex(uivertex, i);
			}
		}
	}
}
