using System;
using UnityEngine;

namespace Boxophobic.StyledGUI
{
	// Token: 0x02000006 RID: 6
	public class StyledIndent : PropertyAttribute
	{
		// Token: 0x0600000F RID: 15 RVA: 0x000022E9 File Offset: 0x000004E9
		public StyledIndent(int indent)
		{
			this.indent = indent;
		}

		// Token: 0x04000012 RID: 18
		public int indent;
	}
}
