using System;
using UnityEngine;

namespace Boxophobic.StyledGUI
{
	// Token: 0x02000005 RID: 5
	public class StyledEnum : PropertyAttribute
	{
		// Token: 0x0600000D RID: 13 RVA: 0x0000223C File Offset: 0x0000043C
		public StyledEnum(string file, string options, int top, int down)
		{
			this.file = file;
			this.options = options;
			this.top = top;
			this.down = down;
		}

		// Token: 0x0600000E RID: 14 RVA: 0x00002290 File Offset: 0x00000490
		public StyledEnum(string display, string file, string options, int top, int down)
		{
			this.display = display;
			this.file = file;
			this.options = options;
			this.top = top;
			this.down = down;
		}

		// Token: 0x0400000D RID: 13
		public string display = "";

		// Token: 0x0400000E RID: 14
		public string file = "";

		// Token: 0x0400000F RID: 15
		public string options = "";

		// Token: 0x04000010 RID: 16
		public int top;

		// Token: 0x04000011 RID: 17
		public int down;
	}
}
