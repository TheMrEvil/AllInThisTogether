using System;
using System.Collections;
using System.Runtime.CompilerServices;

namespace System
{
	// Token: 0x020001AC RID: 428
	internal interface IValueTupleInternal : ITuple
	{
		// Token: 0x0600126A RID: 4714
		int GetHashCode(IEqualityComparer comparer);

		// Token: 0x0600126B RID: 4715
		string ToStringEnd();
	}
}
