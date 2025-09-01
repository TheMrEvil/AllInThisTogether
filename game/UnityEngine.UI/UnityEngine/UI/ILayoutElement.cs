using System;

namespace UnityEngine.UI
{
	// Token: 0x0200001F RID: 31
	public interface ILayoutElement
	{
		// Token: 0x06000269 RID: 617
		void CalculateLayoutInputHorizontal();

		// Token: 0x0600026A RID: 618
		void CalculateLayoutInputVertical();

		// Token: 0x170000A7 RID: 167
		// (get) Token: 0x0600026B RID: 619
		float minWidth { get; }

		// Token: 0x170000A8 RID: 168
		// (get) Token: 0x0600026C RID: 620
		float preferredWidth { get; }

		// Token: 0x170000A9 RID: 169
		// (get) Token: 0x0600026D RID: 621
		float flexibleWidth { get; }

		// Token: 0x170000AA RID: 170
		// (get) Token: 0x0600026E RID: 622
		float minHeight { get; }

		// Token: 0x170000AB RID: 171
		// (get) Token: 0x0600026F RID: 623
		float preferredHeight { get; }

		// Token: 0x170000AC RID: 172
		// (get) Token: 0x06000270 RID: 624
		float flexibleHeight { get; }

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x06000271 RID: 625
		int layoutPriority { get; }
	}
}
