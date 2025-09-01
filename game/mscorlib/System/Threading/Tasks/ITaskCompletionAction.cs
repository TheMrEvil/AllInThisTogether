using System;

namespace System.Threading.Tasks
{
	// Token: 0x02000366 RID: 870
	internal interface ITaskCompletionAction
	{
		// Token: 0x06002467 RID: 9319
		void Invoke(Task completingTask);

		// Token: 0x1700046A RID: 1130
		// (get) Token: 0x06002468 RID: 9320
		bool InvokeMayRunArbitraryCode { get; }
	}
}
