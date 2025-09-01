using System;

namespace Microsoft.Internal
{
	// Token: 0x02000014 RID: 20
	internal static class StringComparers
	{
		// Token: 0x17000005 RID: 5
		// (get) Token: 0x06000062 RID: 98 RVA: 0x00002EF9 File Offset: 0x000010F9
		public static StringComparer ContractName
		{
			get
			{
				return StringComparer.Ordinal;
			}
		}

		// Token: 0x17000006 RID: 6
		// (get) Token: 0x06000063 RID: 99 RVA: 0x00002EF9 File Offset: 0x000010F9
		public static StringComparer MetadataKeyNames
		{
			get
			{
				return StringComparer.Ordinal;
			}
		}
	}
}
