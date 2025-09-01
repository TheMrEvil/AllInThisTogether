using System;
using UnityEngine;

namespace Boxophobic.StyledGUI
{
	// Token: 0x02000002 RID: 2
	public class StyledBanner : PropertyAttribute
	{
		// Token: 0x06000001 RID: 1 RVA: 0x00002050 File Offset: 0x00000250
		public StyledBanner(string title)
		{
			this.colorR = -1f;
			this.title = title;
			this.helpURL = "";
		}

		// Token: 0x06000002 RID: 2 RVA: 0x00002075 File Offset: 0x00000275
		public StyledBanner(float colorR, float colorG, float colorB, string title)
		{
			this.colorR = colorR;
			this.colorG = colorG;
			this.colorB = colorB;
			this.title = title;
			this.helpURL = "";
		}

		// Token: 0x06000003 RID: 3 RVA: 0x000020A5 File Offset: 0x000002A5
		public StyledBanner(string title, string helpURL)
		{
			this.colorR = -1f;
			this.title = title;
			this.helpURL = helpURL;
		}

		// Token: 0x06000004 RID: 4 RVA: 0x000020C6 File Offset: 0x000002C6
		public StyledBanner(float colorR, float colorG, float colorB, string title, string helpURL)
		{
			this.colorR = colorR;
			this.colorG = colorG;
			this.colorB = colorB;
			this.title = title;
			this.helpURL = helpURL;
		}

		// Token: 0x06000005 RID: 5 RVA: 0x000020F3 File Offset: 0x000002F3
		public StyledBanner(string title, string subtitle, string helpURL)
		{
			this.colorR = -1f;
			this.title = title;
			this.helpURL = helpURL;
		}

		// Token: 0x06000006 RID: 6 RVA: 0x00002114 File Offset: 0x00000314
		public StyledBanner(float colorR, float colorG, float colorB, string title, string subtitle, string helpURL)
		{
			this.colorR = colorR;
			this.colorG = colorG;
			this.colorB = colorB;
			this.title = title;
			this.helpURL = helpURL;
		}

		// Token: 0x04000001 RID: 1
		public float colorR;

		// Token: 0x04000002 RID: 2
		public float colorG;

		// Token: 0x04000003 RID: 3
		public float colorB;

		// Token: 0x04000004 RID: 4
		public string title;

		// Token: 0x04000005 RID: 5
		public string helpURL;
	}
}
