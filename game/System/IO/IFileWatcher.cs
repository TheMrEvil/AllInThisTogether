using System;

namespace System.IO
{
	// Token: 0x02000516 RID: 1302
	internal interface IFileWatcher
	{
		// Token: 0x06002A47 RID: 10823
		void StartDispatching(object fsw);

		// Token: 0x06002A48 RID: 10824
		void StopDispatching(object fsw);

		// Token: 0x06002A49 RID: 10825
		void Dispose(object fsw);
	}
}
