using System;
using UnityEngine;

namespace Boxophobic.StyledGUI
{
	// Token: 0x02000010 RID: 16
	public class StyledTexturePreview : PropertyAttribute
	{
		// Token: 0x0600001E RID: 30 RVA: 0x000024E3 File Offset: 0x000006E3
		public StyledTexturePreview()
		{
			this.displayName = "";
		}

		// Token: 0x0600001F RID: 31 RVA: 0x00002501 File Offset: 0x00000701
		public StyledTexturePreview(string displayName)
		{
			this.displayName = displayName;
		}

		// Token: 0x04000027 RID: 39
		public string displayName = "";
	}
}
