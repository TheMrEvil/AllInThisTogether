using System;
using UnityEngine;

namespace Boxophobic.StyledGUI
{
	// Token: 0x02000004 RID: 4
	public class StyledCategory : PropertyAttribute
	{
		// Token: 0x06000009 RID: 9 RVA: 0x00002199 File Offset: 0x00000399
		public StyledCategory(string category)
		{
			this.category = category;
			this.top = 10f;
			this.down = 10f;
			this.colapsable = false;
		}

		// Token: 0x0600000A RID: 10 RVA: 0x000021C5 File Offset: 0x000003C5
		public StyledCategory(string category, bool colapsable)
		{
			this.category = category;
			this.top = 10f;
			this.down = 10f;
			this.colapsable = colapsable;
		}

		// Token: 0x0600000B RID: 11 RVA: 0x000021F1 File Offset: 0x000003F1
		public StyledCategory(string category, float top, float down)
		{
			this.category = category;
			this.top = top;
			this.down = down;
			this.colapsable = false;
		}

		// Token: 0x0600000C RID: 12 RVA: 0x00002215 File Offset: 0x00000415
		public StyledCategory(string category, int top, int down, bool colapsable)
		{
			this.category = category;
			this.top = (float)top;
			this.down = (float)down;
			this.colapsable = colapsable;
		}

		// Token: 0x04000009 RID: 9
		public string category;

		// Token: 0x0400000A RID: 10
		public float top;

		// Token: 0x0400000B RID: 11
		public float down;

		// Token: 0x0400000C RID: 12
		public bool colapsable;
	}
}
