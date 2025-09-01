using System;
using UnityEngine;

namespace Boxophobic.StyledGUI
{
	// Token: 0x02000008 RID: 8
	public class StyledLayers : PropertyAttribute
	{
		// Token: 0x06000011 RID: 17 RVA: 0x00002300 File Offset: 0x00000500
		public StyledLayers()
		{
		}

		// Token: 0x06000012 RID: 18 RVA: 0x00002313 File Offset: 0x00000513
		public StyledLayers(string display)
		{
			this.display = display;
		}

		// Token: 0x04000013 RID: 19
		public string display = "";
	}
}
