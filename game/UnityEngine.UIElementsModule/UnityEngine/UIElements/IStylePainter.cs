using System;

namespace UnityEngine.UIElements
{
	// Token: 0x0200007E RID: 126
	internal interface IStylePainter
	{
		// Token: 0x06000322 RID: 802
		MeshWriteData DrawMesh(int vertexCount, int indexCount, Texture texture, Material material, MeshGenerationContext.MeshFlags flags);

		// Token: 0x06000323 RID: 803
		void DrawText(MeshGenerationContextUtils.TextParams textParams, ITextHandle handle, float pixelsPerPoint);

		// Token: 0x06000324 RID: 804
		void DrawRectangle(MeshGenerationContextUtils.RectangleParams rectParams);

		// Token: 0x06000325 RID: 805
		void DrawBorder(MeshGenerationContextUtils.BorderParams borderParams);

		// Token: 0x06000326 RID: 806
		void DrawImmediate(Action callback, bool cullingEnabled);

		// Token: 0x170000A3 RID: 163
		// (get) Token: 0x06000327 RID: 807
		VisualElement visualElement { get; }
	}
}
