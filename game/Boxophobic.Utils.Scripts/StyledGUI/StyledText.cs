using System;
using UnityEngine;

namespace Boxophobic.StyledGUI
{
	// Token: 0x0200000F RID: 15
	public class StyledText : PropertyAttribute
	{
		// Token: 0x0600001B RID: 27 RVA: 0x00002479 File Offset: 0x00000679
		public StyledText()
		{
		}

		// Token: 0x0600001C RID: 28 RVA: 0x00002493 File Offset: 0x00000693
		public StyledText(TextAnchor alignment)
		{
			this.alignment = alignment;
		}

		// Token: 0x0600001D RID: 29 RVA: 0x000024B4 File Offset: 0x000006B4
		public StyledText(TextAnchor alignment, float top, float down)
		{
			this.alignment = alignment;
			this.top = top;
			this.down = down;
		}

		// Token: 0x04000023 RID: 35
		public string text = "";

		// Token: 0x04000024 RID: 36
		public TextAnchor alignment = TextAnchor.MiddleCenter;

		// Token: 0x04000025 RID: 37
		public float top;

		// Token: 0x04000026 RID: 38
		public float down;
	}
}
