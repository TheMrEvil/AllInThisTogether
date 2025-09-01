using System;
using UnityEngine;

namespace Boxophobic.StyledGUI
{
	// Token: 0x0200000B RID: 11
	public class StyledPopupArray : PropertyAttribute
	{
		// Token: 0x06000017 RID: 23 RVA: 0x0000242E File Offset: 0x0000062E
		public StyledPopupArray(string array)
		{
			this.array = array;
		}

		// Token: 0x0400001D RID: 29
		public string array;
	}
}
