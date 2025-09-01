using System;
using System.Collections.Generic;

namespace System.Linq.Parallel
{
	// Token: 0x02000104 RID: 260
	internal interface IMergeHelper<TInputOutput>
	{
		// Token: 0x060008A3 RID: 2211
		void Execute();

		// Token: 0x060008A4 RID: 2212
		IEnumerator<TInputOutput> GetEnumerator();

		// Token: 0x060008A5 RID: 2213
		TInputOutput[] GetResultsAsArray();
	}
}
