using System;
using System.Security;

namespace System.Threading
{
	// Token: 0x020002D9 RID: 729
	internal interface IThreadPoolWorkItem
	{
		// Token: 0x06001FFC RID: 8188
		[SecurityCritical]
		void ExecuteWorkItem();

		// Token: 0x06001FFD RID: 8189
		[SecurityCritical]
		void MarkAborted(ThreadAbortException tae);
	}
}
