using System;
using System.ComponentModel;

namespace UnityEngine.Networking.Types
{
	// Token: 0x02000012 RID: 18
	[DefaultValue(NetworkAccessLevel.Invalid)]
	public enum NetworkAccessLevel : ulong
	{
		// Token: 0x0400006A RID: 106
		Invalid,
		// Token: 0x0400006B RID: 107
		User,
		// Token: 0x0400006C RID: 108
		Owner,
		// Token: 0x0400006D RID: 109
		Admin = 4UL
	}
}
