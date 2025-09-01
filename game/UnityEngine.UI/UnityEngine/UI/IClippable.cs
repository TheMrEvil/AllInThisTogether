using System;

namespace UnityEngine.UI
{
	// Token: 0x0200000B RID: 11
	public interface IClippable
	{
		// Token: 0x17000011 RID: 17
		// (get) Token: 0x0600004A RID: 74
		GameObject gameObject { get; }

		// Token: 0x0600004B RID: 75
		void RecalculateClipping();

		// Token: 0x17000012 RID: 18
		// (get) Token: 0x0600004C RID: 76
		RectTransform rectTransform { get; }

		// Token: 0x0600004D RID: 77
		void Cull(Rect clipRect, bool validRect);

		// Token: 0x0600004E RID: 78
		void SetClipRect(Rect value, bool validRect);

		// Token: 0x0600004F RID: 79
		void SetClipSoftness(Vector2 clipSoftness);
	}
}
