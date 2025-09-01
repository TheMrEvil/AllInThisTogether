using System;
using System.Runtime.InteropServices;

namespace System.Threading.Tasks
{
	// Token: 0x0200032F RID: 815
	[StructLayout(LayoutKind.Auto)]
	internal struct IndexRange
	{
		// Token: 0x04001C42 RID: 7234
		internal long _nFromInclusive;

		// Token: 0x04001C43 RID: 7235
		internal long _nToExclusive;

		// Token: 0x04001C44 RID: 7236
		internal volatile Box<long> _nSharedCurrentIndexOffset;

		// Token: 0x04001C45 RID: 7237
		internal int _bRangeFinished;
	}
}
