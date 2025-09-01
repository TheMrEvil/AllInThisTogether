using System;
using System.Security.Permissions;

namespace System.Reflection.Emit
{
	// Token: 0x0200090F RID: 2319
	internal struct RefEmitPermissionSet
	{
		// Token: 0x06004E92 RID: 20114 RVA: 0x000F65DE File Offset: 0x000F47DE
		public RefEmitPermissionSet(SecurityAction action, string pset)
		{
			this.action = action;
			this.pset = pset;
		}

		// Token: 0x040030D6 RID: 12502
		public SecurityAction action;

		// Token: 0x040030D7 RID: 12503
		public string pset;
	}
}
