using System;
using UnityEngine.TextCore.Text;

namespace UnityEngine.UIElements
{
	// Token: 0x020002BB RID: 699
	internal interface ITextHandle
	{
		// Token: 0x060017A3 RID: 6051
		Vector2 GetCursorPosition(CursorPositionStylePainterParameters parms, float scaling);

		// Token: 0x060017A4 RID: 6052
		float ComputeTextWidth(MeshGenerationContextUtils.TextParams parms, float scaling);

		// Token: 0x060017A5 RID: 6053
		float ComputeTextHeight(MeshGenerationContextUtils.TextParams parms, float scaling);

		// Token: 0x060017A6 RID: 6054
		float GetLineHeight(int characterIndex, MeshGenerationContextUtils.TextParams textParams, float textScaling, float pixelPerPoint);

		// Token: 0x060017A7 RID: 6055
		TextInfo Update(MeshGenerationContextUtils.TextParams parms, float pixelsPerPoint);

		// Token: 0x060017A8 RID: 6056
		int VerticesCount(MeshGenerationContextUtils.TextParams parms, float pixelPerPoint);

		// Token: 0x060017A9 RID: 6057
		ITextHandle New();

		// Token: 0x060017AA RID: 6058
		bool IsLegacy();

		// Token: 0x060017AB RID: 6059
		void SetDirty();

		// Token: 0x060017AC RID: 6060
		bool IsElided();

		// Token: 0x170005B9 RID: 1465
		// (get) Token: 0x060017AD RID: 6061
		// (set) Token: 0x060017AE RID: 6062
		Vector2 MeasuredSizes { get; set; }

		// Token: 0x170005BA RID: 1466
		// (get) Token: 0x060017AF RID: 6063
		// (set) Token: 0x060017B0 RID: 6064
		Vector2 RoundedSizes { get; set; }
	}
}
