using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000035 RID: 53
	[Obsolete("IDataWatchService is no longer supported and will be removed soon", true)]
	internal interface IDataWatchService
	{
		// Token: 0x06000158 RID: 344
		IDataWatchHandle AddWatch(Object watched, Action<Object> onDataChanged);

		// Token: 0x06000159 RID: 345
		void RemoveWatch(IDataWatchHandle handle);

		// Token: 0x0600015A RID: 346
		void ForceDirtyNextPoll(Object obj);
	}
}
