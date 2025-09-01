using System;
using UnityEngine;

namespace Boxophobic.StyledGUI
{
	// Token: 0x02000009 RID: 9
	public class StyledMask : PropertyAttribute
	{
		// Token: 0x06000013 RID: 19 RVA: 0x00002330 File Offset: 0x00000530
		public StyledMask(string file, string options, int top, int down)
		{
			this.file = file;
			this.options = options;
			this.top = top;
			this.down = down;
		}

		// Token: 0x06000014 RID: 20 RVA: 0x00002384 File Offset: 0x00000584
		public StyledMask(string display, string file, string options, int top, int down)
		{
			this.display = display;
			this.file = file;
			this.options = options;
			this.top = top;
			this.down = down;
		}

		// Token: 0x04000014 RID: 20
		public string display = "";

		// Token: 0x04000015 RID: 21
		public string file = "";

		// Token: 0x04000016 RID: 22
		public string options = "";

		// Token: 0x04000017 RID: 23
		public int top;

		// Token: 0x04000018 RID: 24
		public int down;
	}
}
