using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000006 RID: 6
	internal static class AlignmentUtils
	{
		// Token: 0x0600000B RID: 11 RVA: 0x000020DC File Offset: 0x000002DC
		internal static float RoundToPixelGrid(float v, float pixelsPerPoint, float offset = 0.02f)
		{
			return Mathf.Floor(v * pixelsPerPoint + 0.5f + offset) / pixelsPerPoint;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002100 File Offset: 0x00000300
		internal static float CeilToPixelGrid(float v, float pixelsPerPoint, float offset = -0.02f)
		{
			return Mathf.Ceil(v * pixelsPerPoint + offset) / pixelsPerPoint;
		}

		// Token: 0x0600000D RID: 13 RVA: 0x00002120 File Offset: 0x00000320
		internal static float FloorToPixelGrid(float v, float pixelsPerPoint, float offset = 0.02f)
		{
			return Mathf.Floor(v * pixelsPerPoint + offset) / pixelsPerPoint;
		}
	}
}
