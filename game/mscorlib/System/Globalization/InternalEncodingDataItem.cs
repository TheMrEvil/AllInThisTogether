using System;
using System.Security;

namespace System.Globalization
{
	// Token: 0x020009A3 RID: 2467
	internal struct InternalEncodingDataItem
	{
		// Token: 0x04003711 RID: 14097
		[SecurityCritical]
		internal string webName;

		// Token: 0x04003712 RID: 14098
		internal ushort codePage;
	}
}
