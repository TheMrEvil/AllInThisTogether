using System;
using System.Diagnostics;
using UnityEngine.Scripting;

namespace UnityEngine.TextCore.LowLevel
{
	// Token: 0x02000013 RID: 19
	[UsedByNativeCode]
	[DebuggerDisplay("{(OTL_LookupType)lookupType}")]
	internal struct OTL_Lookup
	{
		// Token: 0x04000089 RID: 137
		public uint lookupType;

		// Token: 0x0400008A RID: 138
		public uint lookupFlag;

		// Token: 0x0400008B RID: 139
		public uint markFilteringSet;
	}
}
