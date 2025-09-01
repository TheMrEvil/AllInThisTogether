using System;

namespace Parse.Infrastructure.Utilities
{
	// Token: 0x0200005D RID: 93
	[AttributeUsage(AttributeTargets.All)]
	internal class PreserveAttribute : Attribute
	{
		// Token: 0x0600045B RID: 1115 RVA: 0x0000DE1C File Offset: 0x0000C01C
		public PreserveAttribute()
		{
		}

		// Token: 0x040000DE RID: 222
		public bool AllMembers;

		// Token: 0x040000DF RID: 223
		public bool Conditional;
	}
}
