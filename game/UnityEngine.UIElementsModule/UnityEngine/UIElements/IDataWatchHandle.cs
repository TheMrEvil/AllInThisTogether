using System;

namespace UnityEngine.UIElements
{
	// Token: 0x02000034 RID: 52
	[Obsolete("IDataWatchHandle is no longer supported and will be removed soon", true)]
	internal interface IDataWatchHandle : IDisposable
	{
		// Token: 0x1700003A RID: 58
		// (get) Token: 0x06000156 RID: 342
		Object watched { get; }

		// Token: 0x1700003B RID: 59
		// (get) Token: 0x06000157 RID: 343
		bool disposed { get; }
	}
}
