using System;
using System.Security;

namespace System.Globalization
{
	// Token: 0x020009A4 RID: 2468
	internal struct InternalCodePageDataItem
	{
		// Token: 0x04003713 RID: 14099
		internal ushort codePage;

		// Token: 0x04003714 RID: 14100
		internal ushort uiFamilyCodePage;

		// Token: 0x04003715 RID: 14101
		internal uint flags;

		// Token: 0x04003716 RID: 14102
		[SecurityCritical]
		internal string Names;
	}
}
