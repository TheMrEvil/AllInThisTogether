using System;
using System.Collections;
using System.Runtime.CompilerServices;
using System.Text;

namespace System
{
	// Token: 0x02000196 RID: 406
	internal interface ITupleInternal : ITuple
	{
		// Token: 0x06001033 RID: 4147
		string ToString(StringBuilder sb);

		// Token: 0x06001034 RID: 4148
		int GetHashCode(IEqualityComparer comparer);
	}
}
