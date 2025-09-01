using System;
using UnityEngine;

namespace Boxophobic.StyledGUI
{
	// Token: 0x0200000E RID: 14
	public class StyledSpace : PropertyAttribute
	{
		// Token: 0x0600001A RID: 26 RVA: 0x0000246A File Offset: 0x0000066A
		public StyledSpace(int space)
		{
			this.space = space;
		}

		// Token: 0x04000022 RID: 34
		public int space;
	}
}
